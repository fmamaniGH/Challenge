using Challenge.Ecommerce.Application.Interface;
using Challenge.Ecommerce.Services.WebApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Challenge.Ecommerce.Application.Test
{
    [TestClass]
    public class UsuarioApplicationTest
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
        public void Authenticate_CuandoNoSeEnvianParametros_RetornarMensajeErrorValidacion()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUsuarioApplication>();

            // Arrange
            var username = string.Empty;
            var password = string.Empty;
            var expected = "Errores de validación";

            // Act            
            var result = context.Authenticate(username, password).Result;
            var actual = result.Message;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Authenticate_CuandoSeEnvianParametrosCorrectos_RetornarMensajeExito()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUsuarioApplication>();

            // Arrange
            var userName = "fmamani";
            var password = "1234";
            var expected = "Exito";

            // Act
            var result = context.Authenticate(userName, password).Result;
            var actual = result.Message;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Authenticate_CuandoSeEnvianParametrosIncorrectos_RetornarMensajeUsuarioNoExiste()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUsuarioApplication>();

            // Arrange
            var userName = "fmamani";
            var password = "123456789";
            var expected = "Usuario no existe";

            // Act
            var result = context.Authenticate(userName, password).Result;
            var actual = result.Message;

            // Assert
            Assert.AreEqual(expected, actual);
        }

    }
}
