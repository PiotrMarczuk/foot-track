using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace FootTrack.Api.Installers
{
    // ReSharper disable once UnusedMember.Global
    public class HangfireInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var mongoUrlBuilder = new MongoUrlBuilder("mongodb://mongodb/jobs");
            var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());

            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseMongoStorage(mongoClient, mongoUrlBuilder.DatabaseName, new MongoStorageOptions
                    {
                        MigrationOptions = new MongoMigrationOptions
                        {
                            MigrationStrategy = new MigrateMongoMigrationStrategy(),
                            BackupStrategy = new CollectionMongoBackupStrategy(),
                        },
                        Prefix = "hangfire.mongo",
                        CheckConnection = true,
                    });
            });

            services.AddHangfireServer(serverOptions =>
            {
                serverOptions.ServerName = "Hangfire.Mongo server 1";
            });
        }
    }
}