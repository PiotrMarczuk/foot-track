using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace FootTrack.Shared.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class ValueObjectTests
    {
        [Test]
        public void Should_correctly_compare_with_another_ValueObjects()
        {
            var valueObject = new ValueObjectExposed();
            var valueObject2 = new ValueObjectExposed();

            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.That(valueObject.Equals(string.Empty), Is.False);
            Assert.That(valueObject.Equals(null), Is.False);
            Assert.That(valueObject, Is.EqualTo(valueObject2));
            Assert.IsTrue(valueObject == valueObject2);
        }

        [Test]
        public void Should_return_correct_HashCode()
        {
            var valueObject = new ValueObjectExposed();
            
            Assert.That(valueObject.GetHashCode(), Is.Not.Zero);
        }
    }

    internal class ValueObjectExposed : ValueObject
    {
        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new List<object>
            {
                string.Empty,
                "",
            };
        }
    }
}
