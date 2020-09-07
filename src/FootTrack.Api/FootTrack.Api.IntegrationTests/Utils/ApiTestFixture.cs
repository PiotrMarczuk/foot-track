using System;
using System.Linq;
using FootTrack.Database.Factories;
using FootTrack.Settings.MongoDb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FootTrack.Api.IntegrationTests.Utils
{
    internal class ApiTestFixture : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddJsonFile("appsettings.test.json");
            });

            builder.ConfigureServices(services =>
            {
                ServiceDescriptor descriptor = services.Single(serviceDescriptor => serviceDescriptor.ServiceType == (typeof(IMongoDatabaseFactory)));
                services.Remove(descriptor);
                ServiceProvider serviceProvider = services.BuildServiceProvider();
                using IServiceScope serviceScope = serviceProvider.CreateScope();
                var dbSettings = serviceScope.ServiceProvider.GetService<IMongoDbSettings>();
                dbSettings.DatabaseName = $"integration_{dbSettings.DatabaseName}_{Guid.NewGuid()}";
                services.AddSingleton<IMongoDatabaseFactory>(s => new IntegrationMongoDatabaseFactory(dbSettings));
            });
        }
    }
}
