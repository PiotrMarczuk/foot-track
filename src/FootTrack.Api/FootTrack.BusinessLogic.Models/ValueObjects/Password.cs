using System.Collections.Generic;
using FootTrack.Shared;

namespace FootTrack.BusinessLogic.Models.ValueObjects
{
    public class Password : ValueObject
    {
        private const int MaxLength = 256;
        private const int MinLength = 6;

        public string Value { get; }

        private Password(string value)
        {
            Value = value;
        }

        public static Result<Password> Create(Maybe<string> passwordOrNothing)
        {
            return passwordOrNothing.ToResult(Errors.General.Empty(nameof(Password)))
                .OnSuccess(password => password.Trim())
                .Ensure(password => password != string.Empty, Errors.General.Empty(nameof(Password)))
                .Ensure(password => password.Length <= MaxLength, Errors.General.TooLong(MaxLength, nameof(Password)))
                .Ensure(password => password.Length >= MinLength, Errors.Password.TooShort(MinLength, nameof(Password)))
                .Map(password => new Password(password));
        }

        public static implicit operator string(Password password)
        {
            return password.Value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}