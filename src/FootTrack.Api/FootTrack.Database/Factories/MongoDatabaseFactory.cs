using FootTrack.Settings.MongoDb;

using MongoDB.Driver;

namespace FootTrack.Database.Factories
{
    public class MongoDatabaseFactory : IMongoDatabaseFactory
    {
        private readonly IMongoDbSettings _mongoDbSettings;
        
        public MongoDatabaseFactory(IMongoDbSettings mongoDbSettings)
        {
            _mongoDbSettings = mongoDbSettings;
        }

        public IMongoDatabase Create()
        {
            var mongoClient = new MongoClient(_mongoDbSettings.ConnectionString);
            return mongoClient.GetDatabase(_mongoDbSettings.DatabaseName);
        }
    }
}