using FootTrack.Database.Models.Document;
using MongoDB.Driver;

namespace FootTrack.Database.Providers
{
    public interface ICollectionProvider<T> where T : IDocument
    {
        public IMongoCollection<T> GetCollection();
    }
}