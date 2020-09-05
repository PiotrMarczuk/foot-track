using FootTrack.Settings.MongoDb;

using MongoDB.Driver;

namespace FootTrack.Database.Factories
{
    public class MongoSingletonDatabaseFactory : IMongoDatabaseFactory
    {
        private readonly IMongoDbSettings _mongoDbSettings;
        private IMongoClient _mongoClient;
        
        public MongoSingletonDatabaseFactory(IMongoDbSettings mongoDbSettings)
        {
            _mongoDbSettings = mongoDbSettings;
        }

        public IMongoDatabase Create()
        {
            if (_mongoClient != null)
            {
                return _mongoClient.GetDatabase(_mongoDbSettings.DatabaseName);
            }
            
            _mongoClient = new MongoClient(_mongoDbSettings.ConnectionString);
            return _mongoClient.GetDatabase(_mongoDbSettings.DatabaseName);
        }
    }
}