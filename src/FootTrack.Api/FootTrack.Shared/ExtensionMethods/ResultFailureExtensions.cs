using System;
using System.Threading.Tasks;

namespace FootTrack.Shared.ExtensionMethods
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
}