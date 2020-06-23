using System.Collections.Generic;
using System.Threading.Tasks;

using FootTrack.Api.Models.Document;

namespace FootTrack.Api.Repositories.Interfaces
{
    public interface ICreateRepository<TDocument> where TDocument : IDocument
    {
        void InsertOne(TDocument document);

        Task InsertOneAsync(TDocument document);

        void InsertMany(ICollection<TDocument> documents);

        Task InsertManyAsync(ICollection<TDocument> documents);
    }
}