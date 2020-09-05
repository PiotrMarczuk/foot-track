using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace FootTrack.Shared.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class ErrorTests
    {
        [Test]
        public void Error_codes_must_be_unique()
        {

            var methods = typeof(Error)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(x => x.ReturnType == typeof(Error))
                .ToList();

            int numberOfUniqueCodes = methods.Select(GetErrorCode)
                .Distinct()
                .Count();

            Assert.That(numberOfUniqueCodes, Is.EqualTo(methods.Count));
        }

        [Test]
        public void Error_codes_must_serialize_correctly()
        {
            Error error = Errors.General.NotFound();
            string serializedError = error.Serialize();
            Error deserializedError = Error.Deserialize(serializedError);

            Assert.That(error, Is.EqualTo(deserializedError));
        }

        [Test]
        public void Error_deserialization_should_fail_when_given_wrong_input()
        {
            const string wrongError = "test||test||test";

            Assert.Throws<InvalidOperationException>(() => Error.Deserialize(wrongError));
        }

        private static string GetErrorCode(MethodInfo method)
        {
            var parameters = method.GetParameters()
                .Select<ParameterInfo, object>(x =>
                {
                    if (x.ParameterType == typeof(string))
                    {
                        return "error.code||test";
                    }

                    throw new Exception();
                })
                .ToArray();

            var error = (Error)method.Invoke(null, parameters);
            return error?.Code;
        }
    }
}