using System;
using FootTrack.Database.Factories;
using FootTrack.Settings.MongoDb;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace FootTrack.TestUtils
{
    public class DatabaseFixture : IDisposable
    {
        private readonly string _originalDbName;
        private readonly IMongoDbSettings _dbSettings;
        private readonly IMongoClient _mongoClient;

        public DatabaseFixture()
        {
            IConfigurationRoot configRoot = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();

            _originalDbName = configRoot.GetSection(nameof(MongoDbSettings))["DatabaseName"];

            _dbSettings = new MongoDbSettings
            {
                ConnectionString = configRoot.GetSection(nameof(MongoDbSettings))["ConnectionString"],
            };

            _mongoClient = new MongoClient(_dbSettings.ConnectionString);
        }

        public IMongoDatabase CreateMongoDatabase()
        {
            _dbSettings.DatabaseName = $"test_db_{_originalDbName}_{Guid.NewGuid()}";

            return new MongoDatabaseFactory(_dbSettings).Create();
        }

        public void Dispose()
        {
            _mongoClient.DropDatabase(_dbSettings.DatabaseName);
        }
    }
}