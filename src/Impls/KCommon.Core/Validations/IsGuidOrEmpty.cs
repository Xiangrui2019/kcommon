using System;
using System.ComponentModel.DataAnnotations;

namespace KCommon.Core.Validations
{
    public class IsGuidOrEmpty : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string val)
            {
                if (string.IsNullOrWhiteSpace(val))
                {
                    return true;
                }
                return Guid.TryParse(val, out _);
            }
            return true;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (IsValid(value))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"{validationContext.DisplayName} 不是一个GUID的值!");
            }
        }
    }
}
