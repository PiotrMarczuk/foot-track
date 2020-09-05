using System;
using System.Collections.Generic;

namespace FootTrack.Shared
{
    public sealed class Error : ValueObject
    {
        private const string Separator = "||";
        
        public string Code { get; }
        public string Message { get; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
        
        public string Serialize()
        {
            return $"{Code}{Separator}{Message}";
        }
        
        public static Error Deserialize(string serialized)
        {
            var data = serialized.Split(
                new[] { Separator },
                StringSplitOptions.RemoveEmptyEntries);

            if (data.Length > 2)
            {
                throw new InvalidOperationException($"Invalid error serialization: '{serialized}'");
            }
            
            return new Error(data[0], data[1]);
        }
    }
}