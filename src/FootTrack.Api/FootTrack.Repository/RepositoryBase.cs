using System.Linq;
using System.Reflection;
using FootTrack.Database.Attributes;
using FootTrack.Database.Models.Document;
using FootTrack.Settings.MongoDb;
using MongoDB.Driver;

namespace FootTrack.Repository
{
    public class RepositoryBase<TDocument> where TDocument : IDocument
    {
        protected readonly IMongoCollection<TDocument> Collection;

        protected RepositoryBase(IMongoDbSettings settings)
        {
            IMongoDatabase database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            Collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private static string GetCollectionName(ICustomAttributeProvider documentType)
        {
            return ((BsonCollectionAttribute) documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }
    }
}