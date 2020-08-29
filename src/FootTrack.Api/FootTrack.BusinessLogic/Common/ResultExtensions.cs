using System;

namespace FootTrack.BusinessLogic.Common
{
    public static class ResultExtensions
    {
        public static Result<T> ToResult<T>(this Maybe<T> maybe, string errorMessage) where T : class
        {
            return maybe.HasNoValue
                ? Result.Fail<T>(errorMessage)
                : Result.Ok(maybe.Value);
        }

        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<T, K> func)
        {
            return result.IsFailure
                ? Result.Fail<K>(result.Error)
                : Result.Ok(func(result.Value));
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