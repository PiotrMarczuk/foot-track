using FootTrack.Communication.Factories;
using FootTrack.Settings.AzureServiceClient;
using NSubstitute;
using NUnit.Framework;

namespace FootTrack.Communication.UnitTests.Factories
{
    [TestFixture]
    public class ServiceClientFactoryTests
    {
        [Test]
        public void Should_successfully_create()
        {
            var settings = Substitute.For<IAzureServiceClientSettings>();
            settings.AzureServiceClientConnectionString.Returns("HostName=blabla.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=/PjcQM8z/EEEbnnyaZWSHQ==");
            var sut = new ServiceClientFactory(settings);

            Assert.That(sut.Create, Is.Not.Null);
        }
    }
}
