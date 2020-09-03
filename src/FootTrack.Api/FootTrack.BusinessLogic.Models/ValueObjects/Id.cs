using FootTrack.Shared.Common;

using MongoDB.Bson;

namespace FootTrack.BusinessLogic.Models.ValueObjects
{
    public class Id : ValueObject<Id>
    {
        public string Value { get; }

        private Id(string value)
        {
            Value = value;
        }

        public static Result<Id> Create(Maybe<string> idOrNothing)
        {
            return ObjectId.TryParse(idOrNothing.Value, out _)
                ? Result.Ok(new Id(idOrNothing.Value))
                : Result.Fail<Id>("Provided id was not correct.");
        }

        protected override bool EqualsCore(Id other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static explicit operator Id(string id)
        {
            return Create(id).Value;
        }

        public static implicit operator string(Id id)
        {
            return id.Value;
        }
    }
}