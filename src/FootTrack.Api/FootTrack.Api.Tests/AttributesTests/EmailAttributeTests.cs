using FootTrack.Api.Attributes;
using NUnit.Framework;

namespace FootTrack.Api.UnitTests.AttributesTests
{
    [TestFixture]
    public class EmailAttributeTests
    {
        private EmailAttribute _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new EmailAttribute();
        }

        [Test]
        public void When_given_empty_email_should_success()
        {
            bool result = _sut.IsValid(null);

            Assert.That(result, Is.True);
        }

        [Test]
        public void When_given_not_literal_email_should_fail()
        {
            bool result = _sut.IsValid(1);

            Assert.That(result, Is.False);
        }

        [Test]
        public void When_given_wrong_email_should_fail()
        {
            bool result = _sut.IsValid("test");

            Assert.That(result, Is.False);
        }

        [Test]
        public void When_correct_email_should_success()
        {
            bool result = _sut.IsValid("test@gmail.com");

            Assert.That(result, Is.True);
        }
    }
}
