using System;
using System.Threading.Tasks;

namespace FootTrack.Shared
{
    public static class ResultFailureExtensions
    {
        public static async Task<Result> OnFailureAsync(this Task<Result> resultTask, Func<Task<Result>> func)
        {
            Result result = await resultTask;

            if (result.IsSuccess)
            {
                return result;
            }

            return await func();
        }
    }

    public static class ResultSuccessExtensions
    {
        public static Result<TK> OnSuccess<T, TK>(this Result<T> result, Func<T, TK> func)
        {
            return result.IsFailure
                ? Result.Fail<TK>(result.Error)
                : Result.Ok(func(result.Value));
        }
        
        public static async Task<Result<TK>> OnSuccessAsync<T, TK>(this Task<Result<T>> resultTask,
            Func<T, Result<TK>> func)
        {
            Result<T> result = await resultTask;

            return result.IsFailure
                ? Result.Fail<TK>(result.Error)
                : func(result.Value);
        }

        public static async Task<Result<TK>> OnSuccessAsync<T, TK>(this Task<Result<T>> resultTask,
            Func<Task<Result<TK>>> func)
        {
            Result<T> result = await resultTask;

            return result.IsFailure
                ? Result.Fail<TK>(result.Error)
                : await func();
        }

        public static async Task<Result> OnSuccessAsync<T>(this Task<Result<T>> resultTask, Func<T, Task<Result>> func)
        {
            Result<T> result = await resultTask;

            return result.IsFailure
                ? Result.Fail(result.Error)
                : await func(result.Value);
        }

        public static async Task<Result> OnSuccessAsync(this Task<Result> resultTask, Func<Result> func)
        {
            Result result = await resultTask;

            return result.IsFailure
                ? Result.Fail(result.Error)
                : func();
        }
    }
}