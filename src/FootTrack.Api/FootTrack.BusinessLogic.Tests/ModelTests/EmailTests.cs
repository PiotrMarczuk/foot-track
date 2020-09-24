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
            const string emailWithSpaces = " veryWeirdEmail@gmail.com ";
            const string notSoValidMail = "email@.com";

            // ACT
            Result<Email> resultOfEmptyEmail = Email.Create(emptyEmail);
            Result<Email> resultOfTooLongEmail = Email.Create(tooLongEmail);
            Result<Email> resultOfEmailWithTrailingSpaces = Email.Create(emailWithSpaces);
            Result<Email> resultOfNotSoValidMail = Email.Create(notSoValidMail);

            // ASSERT
            Assert.That(resultOfEmptyEmail.IsFailure, Is.True);
            Assert.That(resultOfEmptyEmail.Error, Is.EqualTo(Errors.General.Empty()));
            Assert.That(resultOfTooLongEmail.IsFailure, Is.True);
            Assert.That(resultOfTooLongEmail.Error, Is.EqualTo(Errors.General.TooLong(default)));
            Assert.That(resultOfEmailWithTrailingSpaces.IsSuccess, Is.True);
            Assert.That(resultOfEmailWithTrailingSpaces.Value.Value, Is.EqualTo(emailWithSpaces.Trim()));
            Assert.That(resultOfNotSoValidMail.IsFailure, Is.True);
            Assert.That(resultOfNotSoValidMail.Error, Is.EqualTo(Errors.General.Invalid()));
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
