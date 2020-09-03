using FootTrack.Shared.Common;

namespace FootTrack.BusinessLogic.Models.ValueObjects
{
    public class Password : ValueObject<Password>
    {
        public string Value { get; }

        private Password(string value)
        {
            Value = value;
        }

        public static Result<Password> Create(Maybe<string> passwordOrNothing)
        {
            return passwordOrNothing.ToResult("Password should not be empty.")
                .OnSuccess(password => password.Trim())
                .Ensure(password => password != string.Empty, "Password should not be empty.")
                .Ensure(password => password.Length <= 256, "Password is too long.")
                .Ensure(password => password.Length >= 6, "Password is too short.")
                .Map(password => new Password(password));
        }
        
        protected override bool EqualsCore(Password other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static explicit operator Password(string password)
        {
            return Create(password).Value;
        }

        public static implicit operator string(Password password)
        {
            return password.Value;
        }
    }
}