using System;
using FootTrack.Database.Factories;
using FootTrack.Settings.MongoDb;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace FootTrack.Repository.IntegrationTests
{
    public class DatabaseFixture : IDisposable
    {
        private readonly IMongoDbSettings _dbSettings;
        private readonly IMongoClient _mongoClient;

        public IMongoDatabase MongoDb { get; }

        public DatabaseFixture()
        {
            IConfigurationRoot configRoot = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();

            string originalDbName = configRoot.GetSection(nameof(MongoDbSettings))["DatabaseName"];
            var dbName = $"test_db_{originalDbName}_{Guid.NewGuid()}";

            _dbSettings = new MongoDbSettings
            {
                ConnectionString = configRoot.GetSection(nameof(MongoDbSettings))["ConnectionString"],
                DatabaseName = dbName,
            };

            _mongoClient = new MongoClient(_dbSettings.ConnectionString);
            MongoDb = new MongoDatabaseFactory(_dbSettings).Create();
        }

        public void Dispose()
        {
            _mongoClient.DropDatabase(_dbSettings.DatabaseName);
        }
    }
}