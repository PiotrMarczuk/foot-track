using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FootTrack.BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using FootTrack.Settings;
using Microsoft.Extensions.Primitives;

namespace FootTrack.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;
        private readonly HttpStatusCode _statusCode;

        public CachedAttribute(int timeToLiveSeconds, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            _timeToLiveSeconds = timeToLiveSeconds;
            _statusCode = statusCode;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheSettings = context.HttpContext.RequestServices
                .GetRequiredService<RedisCacheSettings>();

            if (!cacheSettings.Enabled)
            {
                await next();
                return;
            }

            var cacheService = context.HttpContext.RequestServices
                .GetRequiredService<IResponseCacheService>();

            string cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var cachedResponseResult = await cacheService
                .GetCachedResponseAsync(cacheKey);

            if (cachedResponseResult.IsSuccess)
            {
                var contentResult = new ContentResult
                {
                    Content = cachedResponseResult.Value,
                    ContentType = "application/json",
                    StatusCode = (int) _statusCode,
                };

                context.Result = contentResult;
                return;
            }

            ActionExecutedContext executedContext = await next();

            if (executedContext.Result is OkObjectResult okObjectResult)
            {
                await cacheService
                    .CacheResponseAsync(
                        cacheKey,
                        okObjectResult.Value,
                        TimeSpan.FromSeconds(_timeToLiveSeconds));
            }
        }

        private static string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder
                .Append($"{request.Path}");

            foreach ((string key, StringValues value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder
                    .Append($"|{key}-{value}");
            }

            return keyBuilder
                .ToString();
        }
    }
}