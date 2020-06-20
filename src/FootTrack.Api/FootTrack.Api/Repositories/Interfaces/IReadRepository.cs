using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using FootTrack.Api.Models.Document;

namespace FootTrack.Api.Repositories.Interfaces
{
    public interface IReadRepository<TDocument> where TDocument : IDocument
    {
        IEnumerable<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression);

        TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression);

        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        TDocument FindById(string id);

        Task<TDocument> FindByIdAsync(string id);
    }
}