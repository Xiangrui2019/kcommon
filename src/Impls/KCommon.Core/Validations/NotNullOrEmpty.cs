using System.ComponentModel.DataAnnotations;

namespace KCommon.Core.Validations
{
    public class NotNullOrEmpty : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string val)
            {
                if (string.IsNullOrEmpty(val))
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
                return new ValidationResult($"{validationContext.DisplayName} 不能为Null或者空!");
            }
        }
    }
}