using MongoDB.Driver;

namespace FootTrack.Database.Factories
{
    public interface IMongoDatabaseFactory
    {
        public IMongoDatabase Create();
    }
}