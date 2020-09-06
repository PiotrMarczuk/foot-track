using System.Collections.Generic;
using System.Text.RegularExpressions;
using FootTrack.Shared;

namespace FootTrack.BusinessLogic.Models.ValueObjects
{
    public class Email : ValueObject
    {
        private const int MaxEmailLength = 256;

        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Create(Maybe<string> emailOrNothing)
        {
            return emailOrNothing.ToResult(Errors.General.Empty(nameof(Email)))
                .OnSuccess(email => email.Trim())
                .Ensure(email => email != string.Empty, Errors.General.Empty(nameof(Email)))
                .Ensure(email => email.Length <= MaxEmailLength, Errors.General.TooLong(MaxEmailLength, nameof(Email)))
                .Ensure(email => Regex.IsMatch(email, @"^(.+)@(.+)$"), Errors.General.Invalid(nameof(Email)))
                .Map(email => new Email(email));
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}