using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;
using NUnit.Framework;

namespace FootTrack.BusinessLogic.Tests.ModelTests
{
    [TestFixture]
    public class PasswordTests
    {
        [Test]
        public void When_password_does_not_meet_restrictions_should_not_be_created()
        {
            // ARRANGE
            var emptyPassword = string.Empty;
            var tooLongPassword = new string('*', 257);
            var tooShortPassword = new string('*', 5);
            const string passwordWithSpaces = " very weird password ";

            // ACT
            var resultOfEmptyPassword = Password.Create(emptyPassword);
            var resultOfTooLongPassword = Password.Create(tooLongPassword);
            var resultOfTooShortPassword = Password.Create(tooShortPassword);
            var resultOfPasswordWithTrailingSpaces = Password.Create(passwordWithSpaces);

            // ASSERT
            Assert.That(resultOfEmptyPassword.IsFailure, Is.True);
            Assert.That(resultOfEmptyPassword.Error, Is.EqualTo(Errors.General.Empty()));
            Assert.That(resultOfTooLongPassword.IsFailure, Is.True);
            Assert.That(resultOfTooLongPassword.Error, Is.EqualTo(Errors.General.TooLong(default)));
            Assert.That(resultOfTooShortPassword.IsFailure, Is.True);
            Assert.That(resultOfTooShortPassword.Error, Is.EqualTo(Errors.Password.TooShort(default)));
            Assert.That(resultOfPasswordWithTrailingSpaces.IsSuccess, Is.True);
            Assert.That(resultOfPasswordWithTrailingSpaces.Value.Value, Is.EqualTo(passwordWithSpaces.Trim()));
        }

        [Test]
        public void Two_password_with_same_value_should_be_considered_equal()
        {
            // ARRANGE
            const string examplePassword = "password";
            Password password1 = Password.Create(examplePassword).Value;
            Password password2 = Password.Create(examplePassword).Value;

            // ACT & ASSERT
            Assert.That(password1, Is.EqualTo(password2));
        }
    }
}
