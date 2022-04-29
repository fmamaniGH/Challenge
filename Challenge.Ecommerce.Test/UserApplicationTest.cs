using Challenge.Ecommerce.Services.WebApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;

namespace Challenge.Ecommerce.Test
{
    [TestClass]
    public class PaisTest
    {
        private static IConfiguration _configuration;
        private static Microsoft.Extensions.DependencyInjection.IServiceScopeFactory _scopeFactory;
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
            _scopeFactory = services.AddLogging().BuildServiceProvider().GetService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
