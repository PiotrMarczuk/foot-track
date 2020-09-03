using System;
using System.Threading.Tasks;

namespace FootTrack.Shared.Common
{
    public static class ResultExtensions
    {
        public static Result<T> ToResult<T>(this Maybe<T> maybe, string errorMessage) where T : class
        {
            return maybe.HasNoValue
                ? Result.Fail<T>(errorMessage)
                : Result.Ok(maybe.Value);
        }

        public static async Task<Result<T>> ToResultAsync<T>(this Task<Maybe<T>> maybeTask, string errorMessage)
            where T : class
        {
            var maybe = await maybeTask;

            return ToResult(maybe, errorMessage);
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

        public static async Task<Result<TK>> OnSuccessAsync<T, TK>(this Task<Result<T>> resultTask, Func<TK> func)
        {
            var result = await resultTask;

            return OnSuccess(result, func);
        }

        private static Result<TK> OnSuccess<T, TK>(this Result<T> result, Func<TK> func)
        {
            return result.IsFailure
                ? Result.Fail<TK>(result.Error)
                : Result.Ok(func());
        }

        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return !predicate(result.Value)
                ? Result.Fail<T>(errorMessage)
                : result;
        }

        public static async Task<Result<T>> EnsureAsync<T>(this Task<Result<T>> resultTask, Func<T, bool> predicate,
            string errorMessage)
        {
            var result = await resultTask;

            return Ensure(result, predicate, errorMessage);
        }

        public static Result<TK> Map<T, TK>(this Result<T> result, Func<T, TK> func)
        {
            return result.IsFailure
                ? Result.Fail<TK>(result.Error)
                : Result.Ok(func(result.Value));
        }

        public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result;
        }

        public static Result OnFailure(this Result result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public static T OnBoth<T>(this Result result, Func<Result, T> func)
        {
            return func(result);
        }

        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }
    }
}