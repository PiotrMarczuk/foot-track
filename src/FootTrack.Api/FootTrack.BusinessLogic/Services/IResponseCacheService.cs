using System;
using System.Threading.Tasks;
using FootTrack.Shared;

namespace FootTrack.BusinessLogic.Services
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);

        Task<Result<string>> GetCachedResponseAsync(string cacheKey);
    }
}
