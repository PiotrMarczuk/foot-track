using System.Collections.Generic;
using FootTrack.Shared;
using MongoDB.Bson;

namespace FootTrack.BusinessLogic.Models.ValueObjects
{
    public class Id : ValueObject
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
                : Result.Fail<Id>(Errors.General.Invalid(nameof(Id)));
        }

        public static implicit operator string(Id id)
        {
            return id.Value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}