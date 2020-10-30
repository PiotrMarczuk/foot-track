using FootTrack.Communication.Factories;

using NUnit.Framework;

namespace FootTrack.Communication.UnitTests.Factories
{
    [TestFixture]
    public class CloudToDeviceMethodFactoryTests
    {
        [Test]
        public void Should_successfully_create_start_method()
        {
            var sut = new CloudToDeviceMethodFactory();

            var result = sut.CreateStartMethod();

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Should_successfully_create_end_method()
        {
            var sut = new CloudToDeviceMethodFactory();

            var result = sut.CreateEndMethod();

            Assert.That(result, Is.Not.Null);
        }
    }
}
