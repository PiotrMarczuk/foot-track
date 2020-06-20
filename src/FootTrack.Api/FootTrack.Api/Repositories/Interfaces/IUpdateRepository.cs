using System.Threading.Tasks;

using FootTrack.Api.Models.Document;

namespace FootTrack.Api.Repositories.Interfaces
{
    public interface IUpdateRepository<in TDocument> where TDocument : IDocument
    {
        void ReplaceOne(TDocument document);

        Task ReplaceOneAsync(TDocument document);
    }
}