using System;
using System.Threading.Tasks;

namespace FootTrack.Shared
{
    public static class ResultExtensions
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
            var maybe = await maybeTask;

            return ToResult(maybe, error);
        }

        public static Result<TK> OnSuccess<T, TK>(this Result<T> result, Func<T, TK> func)
        {
            return result.IsFailure
                ? Result.Fail<TK>(result.Error)
                : Result.Ok(func(result.Value));
        }
        
        public static async Task<Result<TK>> OnSuccessAsync<T, TK>(this Task<Result<T>> resultTask, Func<T, TK> func)
        {
            var result = await resultTask;

            return OnSuccess(result, func);
        }

        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, Error error)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return !predicate(result.Value)
                ? Result.Fail<T>(error)
                : result;
        }

        public static async Task<Result<T>> EnsureAsync<T>(this Task<Result<T>> resultTask, Func<T, bool> predicate,
            Error error)
        {
            var result = await resultTask;

            return Ensure(result, predicate, error);
        }

        public static Result<TK> Map<T, TK>(this Result<T> result, Func<T, TK> func)
        {
            return result.IsFailure
                ? Result.Fail<TK>(result.Error)
                : Result.Ok(func(result.Value));
        }
    }
}