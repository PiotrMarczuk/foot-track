using System;
using FootTrack.Database.Factories;
using FootTrack.Database.Models.Document;
using FootTrack.Database.Providers;
using FootTrack.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace FootTrack.Api.Installers
{
    // ReSharper disable once UnusedType.Global
    // ReSharper disable once UnusedMember.Global
    public class RepositoryInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSingleton<IMongoDatabaseFactory, MongoSingletonDatabaseFactory>();
            services
                .AddSingleton(typeof(IMongoDatabase), MongoDatabaseImplementationFactory);
            services
                .AddScoped(typeof(ICollectionProvider<>), typeof(CollectionProvider<>));
            services
                .AddTransient<IUserRepository, UserRepository>();
        }

        private static IMongoDatabase MongoDatabaseImplementationFactory(IServiceProvider serviceProvider)
        {
            var mongoClientFactory = serviceProvider.GetService<IMongoDatabaseFactory>();
            return mongoClientFactory.Create();
        }
    }
}