using FootTrack.Api.Attributes;
using MongoDB.Bson;
using NUnit.Framework;

namespace FootTrack.Api.Tests.AttributesTests
{
    [TestFixture]
    public class IdAttributeTests
    {
        private IdAttribute _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new IdAttribute();
        }

        [Test]
        public void When_given_empty_id_should_success()
        {
            bool result = _sut.IsValid(null);

            Assert.That(result, Is.True);
        }

        [Test]
        public void When_given_not_literal_id_should_fail()
        {
            bool result = _sut.IsValid(1);

            Assert.That(result, Is.False);
        }

        [Test]
        public void When_given_wrong_id_should_fail()
        {
            bool result = _sut.IsValid("test");

            Assert.That(result, Is.False);
        }

        [Test]
        public void When_correct_id_should_success()
        {
            string correctId = ObjectId.GenerateNewId().ToString();

            bool result = _sut.IsValid(correctId);

            Assert.That(result, Is.True);
        }
    }
}
