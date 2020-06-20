using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using FootTrack.Api.Models.Document;

namespace FootTrack.Api.Repositories.Interfaces
{
    public interface IDeleteRepository<TDocument> where TDocument : IDocument
    {
        void DeleteOne(Expression<Func<TDocument, bool>> filterExpression);

        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        void DeleteById(string id);

        Task DeleteByIdAsync(string id);

        void DeleteMany(Expression<Func<TDocument, bool>> filterExpression);

        Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);
    }
}