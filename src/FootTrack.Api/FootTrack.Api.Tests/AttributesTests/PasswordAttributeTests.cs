using FootTrack.Api.Attributes;
using NUnit.Framework;

namespace FootTrack.Api.Tests.AttributesTests
{
    [TestFixture]
    public class PasswordAttributeTests
    {
        private PasswordAttribute _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new PasswordAttribute();
        }

        [Test]
        public void When_given_empty_password_should_success()
        {
            bool result = _sut.IsValid(null);

            Assert.That(result, Is.True);
        }

        [Test]
        public void When_given_not_literal_password_should_fail()
        {
            bool result = _sut.IsValid(1);

            Assert.That(result, Is.False);
        }

        [Test]
        public void When_given_too_short_password_should_fail()
        {
            bool result = _sut.IsValid("test");

            Assert.That(result, Is.False);
        }

        [Test]
        public void When_given_correct_password_should_success()
        {
            bool result = _sut.IsValid("testpassword");

            Assert.That(result, Is.True);
        }
    }
}
