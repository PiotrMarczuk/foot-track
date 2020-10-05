using System;
using System.Threading.Tasks;

namespace FootTrack.Shared.ExtensionMethods
{
    public static class ResultMappingExtensions
    {
        public static Result<T> ToResult<T>(this Maybe<T> maybe, Error error) where T : class
        {
            return maybe.HasNoValue
                ? Result.Fail<T>(error)
                : Result.Ok(maybe.Value);
        }
        
        public static async Task<Result<T>> ToResultAsync<T>(this Task<Maybe<T>> maybeTask, Error error)
            where T : class
        {
            Maybe<T> maybe = await maybeTask;

            return ToResult(maybe, error);
        }

        public static Result<TK> Map<T, TK>(this Result<T> result, Func<T, TK> func)
        {
            return result.IsFailure
                ? Result.Fail<TK>(result.Error)
                : Result.Ok(func(result.Value));
        }
    }
}