using System;
using System.Threading.Tasks;

namespace FootTrack.Shared
{
    public static class ResultEnsureExtensions
    {
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

        public static async Task<Result<T>> EnsureAsync<T>(
            this Task<Result<T>> resultTask, 
            Func<T, bool> predicate,
            Error error)
        {
            Result<T> result = await resultTask;

            return Ensure(result, predicate, error);
        }
    }
}