using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace FootTrack.Shared.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class MaybeTests
    {
        [Test]
        public void When_getting_empty_maybe_should_fail()
        {
            Maybe<string> maybe = null;

            Assert.Throws<InvalidOperationException>(() =>
            {
                string unused = maybe.Value;
            });
        }

        [Test]
        public void Should_correctly_compare_not_maybe_with_maybe()
        {
            const string test = "test";
            const string differentValue = "test2";
            Maybe<string> maybe = test;

            Assert.IsTrue(test == maybe);
            Assert.IsTrue(maybe == test);
            Assert.IsTrue(differentValue != maybe);
            Assert.IsTrue(maybe != differentValue);
        }

        [Test]
        public void Should_correctly_compare_with_another_maybe()
        {
            const string test = "test";

            Maybe<string> maybe = test;
            Maybe<string> anotherMaybe = test;
            var emptyMaybe = new Maybe<string>();
            var anotherEmptyMaybe = new Maybe<string>();

            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsFalse(maybe.Equals((object)test));
            Assert.IsTrue(maybe.Equals(anotherMaybe));
            Assert.IsTrue(maybe.Equals((object)anotherMaybe));
            Assert.IsTrue(emptyMaybe.Equals(anotherEmptyMaybe));
            Assert.IsFalse(maybe.Equals(emptyMaybe));
        }

        [Test]
        public void Should_return_correct_string()
        {
            Maybe<string> maybe = "test";
            var emptyMaybe = new Maybe<string>();

            Assert.That(maybe.ToString(), Is.Not.Empty);
            Assert.That(maybe.ToString(), Is.EqualTo("test"));
            Assert.That(emptyMaybe.ToString(), Is.Not.Empty);
            Assert.That(emptyMaybe.ToString(), Is.EqualTo("No value"));
        }

        [Test]
        public void Should_return_correct_hashcode()
        {
            Maybe<string> maybe = "test";

            Assert.That(maybe.GetHashCode(), Is.Not.Zero);
        }
    }
}
