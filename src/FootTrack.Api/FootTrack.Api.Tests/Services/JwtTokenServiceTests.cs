using System;
using NSubstitute;
using NUnit.Framework;

using FootTrack.Api.Models;
using FootTrack.Api.Services.Implementations;
using FootTrack.Api.Services.Interfaces;
using FootTrack.Api.Settings.JwtToken;

namespace FootTrack.Api.Tests.Services
{

    public class JwtTokenServiceTests
    {
        private IJwtTokenService _sut;

        [SetUp]
        public void SetUp()
        {
            var settings = Substitute.For<IJwtTokenSettings>();
            settings.Secret.Returns("testesttesttesttestesttesttesttestesttesttesttestesttesttest");

            _sut = new JwtTokenService(settings);
        }

        [Test]
        public void When_given_null_parameter_should_throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.GenerateToken(null));
        }

        [Test]
        public void When_given_valid_parameter_should_return_not_empty_string()
        {
            var user = new User();

            var result = _sut
                .GenerateToken(user);

            Assert.That(result, Is.Not.Empty);
        }
    }
}