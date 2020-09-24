using System;
using System.ComponentModel.DataAnnotations;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;

namespace FootTrack.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var id = value as string;
            
            if (id == null)
            {
                return new ValidationResult(Errors.General.Invalid(nameof(id)).Serialize());
            }

            Result<Id> idResult = Id.Create(id);
            
            return idResult.IsFailure 
                ? new ValidationResult(idResult.Error.Serialize()) 
                : ValidationResult.Success;
        }
    }
}