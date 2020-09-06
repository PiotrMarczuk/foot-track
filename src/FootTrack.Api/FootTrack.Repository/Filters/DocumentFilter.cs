using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Database.Models.Document;

using MongoDB.Bson;
using MongoDB.Driver;

namespace FootTrack.Repository.Filters
{
    public static class DocumentFilter<T> where T : IDocument
    {
        public static FilterDefinition<T> FilterById(Id id)
        {
            return Builders<T>.Filter.Eq(x => x.Id, ObjectId.Parse(id));
        }
    }
}