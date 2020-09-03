// using System;
// using NSubstitute;
// using NUnit.Framework;
//
// using FootTrack.Api.Models;
// using FootTrack.Api.Services.Interfaces;
// using FootTrack.BusinessLogic.Models;
// using FootTrack.BusinessLogic.Services.Implementations;
// using FootTrack.BusinessLogic.Services.Interfaces;
// using FootTrack.Database.Models;
// using FootTrack.Settings.JwtToken;
//
// namespace FootTrack.Api.Tests.Services
// {
//
//     public class JwtTokenServiceTests
//     {
//         private IJwtTokenService _sut;
//
//         [SetUp]
//         public void SetUp()
//         {
//             var settings = Substitute.For<IJwtTokenSettings>();
//             settings.Secret.Returns("testesttesttesttestesttesttesttestesttesttesttestesttesttest");
//             settings.TokenLifetime.Returns(new TimeSpan(0, 5, 0, 0));
//
//             _sut = new JwtTokenService(settings);
//         }
//
//         [Test]
//         public void When_given_null_parameter_should_throw_ArgumentNullException()
//         {
//             Assert.Throws<ArgumentNullException>(() => _sut.GenerateToken(null));
//         }
//
//         [Test]
//         public void When_given_valid_parameter_should_return_not_empty_string()
//         {
//             var user = new UserLogin();
//
//             var result = _sut
//                 .GenerateToken(user);
//
//             Assert.That(result, Is.Not.Empty);
//         }
//     }
// }