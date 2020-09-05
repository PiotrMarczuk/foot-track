using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;
using MongoDB.Bson;
using NUnit.Framework;

namespace FootTrack.BusinessLogic.Tests.ModelTests
{
    [TestFixture]
    public class IdTests
    {
        [Test]
        public void When_id_does_not_meet_restrictions_should_not_be_created()
        {
            // ARRANGE
            const string stupidId = "dsadfasfasf";
            var generatedId = ObjectId.GenerateNewId().ToString();

            // ACT
            var stupidIdResult = Id.Create(stupidId);
            var generatedIdResult = Id.Create(generatedId);

            // ASSERT
            Assert.That(stupidIdResult.IsFailure, Is.True);
            Assert.That(stupidIdResult.Error, Is.EqualTo(Errors.General.Invalid()));
            Assert.That(generatedIdResult.IsSuccess, Is.True);
            Assert.That(generatedIdResult.Value.Value, Is.EqualTo(generatedId));
        }

        [Test]
        public void Two_Ids_with_same_value_should_be_considered_equal()
        {
            // ARRANGE
            var generatedId = ObjectId.GenerateNewId().ToString();
            Id id1 = Id.Create(generatedId).Value;
            Id id2 = Id.Create(generatedId).Value;

            // ACT & ASSERT
            Assert.That(id1, Is.EqualTo(id2));
        }
    }
}
