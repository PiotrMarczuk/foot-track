using System;
using System.Threading.Tasks;
using FootTrack.Shared;
using FootTrack.Shared.ExtensionMethods;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace FootTrack.BusinessLogic.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;

        public ResponseCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            string serializedResponse = JsonConvert
                .SerializeObject(response);

            await _distributedCache.SetStringAsync(
                cacheKey,
                serializedResponse,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = timeToLive,
                });
        }

        public async Task<Result<string>> GetCachedResponseAsync(string cacheKey)
        {
            Maybe<string> cacheOrNothing =  await _distributedCache
                .GetStringAsync(cacheKey);

            return cacheOrNothing.ToResult(Errors.General.NotFound());
        }
    }
}
