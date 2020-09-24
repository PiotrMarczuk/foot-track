using System;
using System.ComponentModel.DataAnnotations;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;

namespace FootTrack.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var email = value as string;
            
            if (email == null)
            {
                return new ValidationResult(Errors.General.Invalid(nameof(email)).Serialize());
            }

            Result<Email> emailResult = Email.Create(email);
            
            return emailResult.IsFailure 
                ? new ValidationResult(emailResult.Error.Serialize()) 
                : ValidationResult.Success;
        }
    }
}