using System;
using System.Diagnostics.CodeAnalysis;
using FootTrack.BusinessLogic.Models.ValueObjects;
using NUnit.Framework;

namespace FootTrack.Shared.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class ResultTests
    {
        [Test]
        public void When_trying_to_create_wrong_state_should_fail()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new ResultExposed(true, Errors.General.NotFound());
            });

            Assert.Throws<InvalidOperationException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new ResultExposed(false, null);
            });
        }

        [Test]
        public void When_trying_to_extract_value_with_set_error_should_fail()
        {
            Result<Email> emailResult = Email.Create(null);

            Assert.Throws<InvalidOperationException>(() =>
            {
                Email unused = emailResult.Value;
            });
        }

        [Test]
        public void When_combining_multiple_results_with_at_least_one_error_should_return_error()
        {
            Result<Email> emailResult = Email.Create(null);
            Result<Email> emailResult2 = Email.Create("test@gmail.com");

            Result combineResult = Result.Combine(emailResult, emailResult2);

            Assert.That(combineResult.IsFailure, Is.True);
        }
    }

    [ExcludeFromCodeCoverage]
    internal class ResultExposed : Result
    {
        public ResultExposed(bool isSuccess, Error error) : base(isSuccess, error)
        {
        }
    }
}
