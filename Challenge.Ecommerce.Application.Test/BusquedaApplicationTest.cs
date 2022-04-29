using Challenge.Ecommerce.Application.Interface;
using Challenge.Ecommerce.Services.WebApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Challenge.Ecommerce.Application.Test
{
    [TestClass]
    public class BusquedaApplicationTest
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
            var context = scope.ServiceProvider.GetService<IBusquedaApplication>();

            // Arrange
            var item = "iphone";
            var expected = "Exito";

            // Act
            var result = context.GetItems(item).Result;
            var actual = result.Message;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPais__CuandoNoSeEnvianParametros_RetornarMensajeErrorValidacion()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IBusquedaApplication>();

            // Arrange
            var item = string.Empty;           
            var expected = "Errores de validación"; 

            // Act
            var result = context.GetItems(item).Result;
            var actual = result.Message;

            // Assert
            Assert.AreEqual(expected, actual);
        }

    }
}
