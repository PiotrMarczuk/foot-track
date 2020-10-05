using System;
using System.Threading;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.BusinessLogic.Services;
using FootTrack.Settings.JwtToken;
using MongoDB.Bson;
using NSubstitute;
using NUnit.Framework;

namespace FootTrack.BusinessLogic.UnitTests.ServiceTests
{
    [TestFixture]
    public class JwtTokenServiceTests
    {
        private IJwtTokenService _sut;

        private readonly TimeSpan _tokenLifeTime = TimeSpan.FromSeconds(1);

        [SetUp]
        public void SetUp()
        {
            IJwtTokenSettings jwtTokenSettings = MockJwtTokenSettings();

            _sut = new JwtTokenService(jwtTokenSettings);
        }

        [Test]
        public void Should_generate_correct_token()
        {
            // ARRANGE
            Id firstId = Id.Create(ObjectId.GenerateNewId().ToString()).Value;
            Id secondId = Id.Create(ObjectId.GenerateNewId().ToString()).Value;

            // ACT
            string firstToken = _sut.GenerateToken(firstId);
            string secondToken = _sut.GenerateToken(secondId);
            Thread.Sleep(_tokenLifeTime);
            string tokenAfterTimeSpanEnded = _sut.GenerateToken(firstId);

            // ASSERT
            Assert.That(firstToken, Is.Not.EqualTo(secondToken));
            Assert.That(firstToken, Is.Not.EqualTo(tokenAfterTimeSpanEnded));
        }

        private IJwtTokenSettings MockJwtTokenSettings()
        {
            var jwtTokenSettings = Substitute.For<IJwtTokenSettings>();
            jwtTokenSettings.Secret.Returns("supersecretblablablablablablalblablal");
            jwtTokenSettings.TokenLifetime.Returns(_tokenLifeTime);

            return jwtTokenSettings;
        }
    }
}
