using System;
using FootTrack.Shared;

namespace FootTrack.Api.Utils
{
    public class Envelope<T>
    {
        public T Result { get; }

        public string ErrorMessage { get; }

        public DateTime TimeGenerated { get; }

        public string FieldName { get; }

        protected internal Envelope(T result, Error error, string fieldName) : this(result, error)
        {
            FieldName = fieldName;
        }

        protected internal Envelope(T result, Error error) : this(result)
        {
            ErrorMessage = error.Message;
        }

        protected internal Envelope(T result)
        {
            Result = result;
            TimeGenerated = DateTime.UtcNow;
        }
    }

    public sealed class Envelope : Envelope<string>
    {
        private Envelope() : base(null)
        {
        }

        private Envelope(Error error)
            : base(null, error)
        {
        }

        private Envelope(Error error, string fieldName) : base(null, error, fieldName)
        {
        }

        public static Envelope<T> Ok<T>(T result)
        {
            return new Envelope<T>(result);
        }

        public static Envelope Ok()
        {
            return new Envelope();
        }

        public static Envelope Error(Error error)
        {
            return new Envelope(error);
        }

        public static Envelope Error(Error error, string fieldName)
        {
            return new Envelope(error, fieldName);
        }
    }
}