using System;
using FootTrack.Shared;

namespace FootTrack.Api.Utils
{
    public class Envelope<T>
    {
        public T Result { get; }

        public ErrorDetails Errors { get; }

        public DateTime TimeGenerated { get; } = DateTime.UtcNow;


        protected internal Envelope(T result)
        {
            Result = result;
        }

        protected internal Envelope(ErrorDetails details)
        {
            Errors = details;
        }
    }

    public sealed class Envelope : Envelope<string>
    {
        private Envelope(Error error, string fieldName = default) : base(new ErrorDetails(error.Message, fieldName))
        {
        }

        public static Envelope<T> Ok<T>(T result)
        {
            return new Envelope<T>(result);
        }

        public static Envelope Error(Error error, string fieldName = default)
        {
            return new Envelope(error, fieldName);
        }
    }
}