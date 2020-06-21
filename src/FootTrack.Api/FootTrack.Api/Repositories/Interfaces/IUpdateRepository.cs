using System.Threading.Tasks;

using FootTrack.Api.Models.Document;

namespace FootTrack.Api.Repositories.Interfaces
{
    public interface IUpdateRepository<TDocument> where TDocument : IDocument
    {
        TDocument ReplaceOne(TDocument document);

        Task<TDocument> ReplaceOneAsync(TDocument document);
    }
}