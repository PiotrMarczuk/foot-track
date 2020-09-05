using System.Linq;
using System.Reflection;

using FootTrack.Database.Attributes;
using FootTrack.Database.Models.Document;

using MongoDB.Driver;

namespace FootTrack.Database.Providers
{
    public class CollectionProvider<T> : ICollectionProvider<T> where T : IDocument
    {
        private readonly IMongoDatabase _mongoDatabase;

        public CollectionProvider(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public IMongoCollection<T> GetCollection()
        {
            return _mongoDatabase.GetCollection<T>(GetCollectionName(typeof(T)));
        }

        private static string GetCollectionName(ICustomAttributeProvider documentType)
        {
            return ((BsonCollectionAttribute) documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .First()).CollectionName;
        }
    }
}