using FootTrack.Communication.Factories;

using NUnit.Framework;

namespace FootTrack.Communication.UnitTests.Factories
{
    [TestFixture]
    public class CloudToDeviceMethodFactoryTests
    {
        [Test]
        public void Should_successfully_create()
        {
            var sut = new CloudToDeviceMethodFactory();

            var result = sut.Create();

            Assert.That(result, Is.Not.Null);
        }
    }
}
