using Newtonsoft.Json;

using NUnit.Framework;

namespace FootTrack.BusinessLogic.UnitTests
{
    internal static class TestUtils
    {
        public static void AssertAreEqualByJson(object actual, object expected)
        {
            string actualString = JsonConvert.SerializeObject(actual);
            string expectedString = JsonConvert.SerializeObject(expected);

            Assert.That(actualString, Is.EqualTo(expectedString));
        }
    }
}