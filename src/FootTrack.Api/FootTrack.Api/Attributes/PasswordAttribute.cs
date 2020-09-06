using System;
using System.ComponentModel.DataAnnotations;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;

namespace FootTrack.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var password = value as string;

            if (password == null)
            {
                return new ValidationResult(Errors.General.Invalid(nameof(password)).Serialize());
            }

            var passwordResult = Password.Create(password);

            return passwordResult.IsFailure
                ? new ValidationResult(passwordResult.Error.Serialize())
                : ValidationResult.Success;
        }
    }
}