using System.Linq;

using FootTrack.Api.Models.Document;

namespace FootTrack.Api.Repositories.Interfaces
{
    public interface IMongoRepository<TDocument>
        : ICreateRepository<TDocument>,
            IReadRepository<TDocument>,
            IUpdateRepository<TDocument>,
            IDeleteRepository<TDocument>
        where TDocument : IDocument
    {
        IQueryable<TDocument> AsQueryable();
    }
}