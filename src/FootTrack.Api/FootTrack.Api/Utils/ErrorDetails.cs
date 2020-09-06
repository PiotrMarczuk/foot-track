using NullGuard;

namespace FootTrack.Api.Utils
{
    public sealed class ErrorDetails
    {
        [AllowNull]
        public string FieldName { get; }

        public string Message { get; }

        public ErrorDetails(string message, string fieldName = default)
        {
            Message = message;

            FieldName = fieldName;
        }
    }
}