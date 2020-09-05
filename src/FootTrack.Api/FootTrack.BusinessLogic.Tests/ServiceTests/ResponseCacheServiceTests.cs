using System;
using System.Text;
using System.Threading.Tasks;
using FootTrack.BusinessLogic.Services;
using Microsoft.Extensions.Caching.Distributed;
using NSubstitute;
using NUnit.Framework;

namespace FootTrack.BusinessLogic.Tests.ServiceTests
{
    [TestFixture]
    public class ResponseCacheServiceTests
    {
        private readonly TimeSpan _cacheLifeTime = TimeSpan.FromSeconds(1);
        private IResponseCacheService _sut;
        private IDistributedCache _distributedCache;

        [SetUp]
        public void SetUp()
        {
            _distributedCache = Substitute.For<IDistributedCache>();
            _sut = new ResponseCacheService(_distributedCache);
        }

        [Test]
        public async Task When_creating_cache_should_add_it_to_distributed_cache()
        {
            // ARRANGE
            const string cacheKey = "value";
            const string response = "responseString";

            // ACT
            await _sut.CacheResponseAsync(cacheKey, response, _cacheLifeTime);

            // ASSERT
            await _distributedCache
                .ReceivedWithAnyArgs()
                .SetAsync(default, default, default);
        }

        [Test]
        public async Task Should_return_existing_value_from_cache()
        {
            // ARRANGE
            const string cacheKey = "value";
            const string cachedResponse = "response";
            _distributedCache.GetAsync(cacheKey).Returns(Encoding.ASCII.GetBytes(cachedResponse));

            // ACT
            string result = await _sut.GetCachedResponseAsync(cacheKey);

            // ASSERT
            Assert.That(result, Is.EqualTo(cachedResponse));
        }
    }
}
