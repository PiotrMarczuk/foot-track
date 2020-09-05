using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;
using NUnit.Framework;

namespace FootTrack.BusinessLogic.Tests.ModelTests
{
    [TestFixture]
    public class EmailTests
    {
        [Test]
        public void When_Email_does_not_meet_restrictions_should_not_be_created()
        {
            // ARRANGE
            var emptyEmail = string.Empty;
            var tooLongEmail = new string('*', 257);
            const string emailWithSpaces = " veryweirdEmail@gmail.com ";

            // ACT
            var resultOfEmptyEmail = Email.Create(emptyEmail);
            var resultOfTooLongEmail = Email.Create(tooLongEmail);
            var resultOfEmailWithTrailingSpaces = Email.Create(emailWithSpaces);

            // ASSERT
            Assert.That(resultOfEmptyEmail.IsFailure, Is.True);
            Assert.That(resultOfEmptyEmail.Error, Is.EqualTo(Errors.General.Empty()));
            Assert.That(resultOfTooLongEmail.IsFailure, Is.True);
            Assert.That(resultOfTooLongEmail.Error, Is.EqualTo(Errors.General.TooLong(default)));
            Assert.That(resultOfEmailWithTrailingSpaces.IsSuccess, Is.True);
            Assert.That(resultOfEmailWithTrailingSpaces.Value.Value, Is.EqualTo(emailWithSpaces.Trim()));
        }

        [Test]
        public void Two_emails_with_same_value_should_be_considered_equal()
        {
            // ARRANGE
            const string exampleEmail = "email@gmail.com";
            Email email1 = Email.Create(exampleEmail).Value;
            Email email2 = Email.Create(exampleEmail).Value;

            // ACT & ASSERT
            Assert.That(email1, Is.EqualTo(email2));
        }
    }
}
