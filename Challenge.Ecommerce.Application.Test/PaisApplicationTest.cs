using Challenge.Ecommerce.Application.Interface;
using Challenge.Ecommerce.Services.WebApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Challenge.Ecommerce.Application.Test
{
    [TestClass]
    public class PaisApplicationTest
    {
        private static IConfiguration _configuration;
        private static IServiceScopeFactory _scopeFactory;

        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();
            _configuration = builder.Build();

            var startup = new Startup(_configuration);
            var services = new ServiceCollection();
            startup.ConfigureServices(services);
            _scopeFactory = services.AddLogging().BuildServiceProvider().GetService<IServiceScopeFactory>();
        }

        [TestMethod]
        public void GetPais_CuandoSeEnvianParametrosCorrectos_RetornarMensajeExito()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IPaisApplication>();

            // Arrange
            var pais = "AR";
            var expected = "Exito";

            // Act
            var result = context.GetPais(pais).Result;
            var actual = result.Message;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPais_CuandoSeEnvianParametrosIncorrectos_RetornarMensajePaisNoExiste()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IPaisApplication>();

            // Arrange
            var pais = "ARRR";
            var expected = "No se encuentra el pais";

            // Act
            var result = context.GetPais(pais).Result;
            var actual = result.Message;

            // Assert
            Assert.AreEqual(expected, actual);
        }

    }
}
