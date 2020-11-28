using System.ComponentModel.DataAnnotations;

namespace KCommon.Core.Validations
{
    public class NonnegativeInt : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is int val)
            {
                if (val >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
                return new ValidationResult($"The {validationContext.DisplayName} should not be null or empty!");
            }
        }
    }
    
    public class NonnegativeLong : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is long val)
            {
                if (val >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
                return new ValidationResult($"The {validationContext.DisplayName} should not be null or empty!");
            }
        }
    }
}