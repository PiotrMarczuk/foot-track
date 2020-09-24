using System;
using FootTrack.Database.Factories;
using FootTrack.Settings.MongoDb;
using MongoDB.Driver;

namespace FootTrack.Api.IntegrationTests.Utils
{
    internal class IntegrationMongoDatabaseFactory : IMongoDatabaseFactory, IDisposable
    {
        private readonly IMongoDbSettings _mongoDbSettings;
        private IMongoClient _mongoClient;

        public IntegrationMongoDatabaseFactory(IMongoDbSettings mongoDbSettings)
        {
            _mongoDbSettings = mongoDbSettings;
        }

        public IMongoDatabase Create()
        {
            _mongoClient = new MongoClient(_mongoDbSettings.ConnectionString);
            return _mongoClient.GetDatabase(_mongoDbSettings.DatabaseName);
        }

        public void Dispose()
        {
            _mongoClient.DropDatabase(_mongoDbSettings.DatabaseName);
        }
    }
}
