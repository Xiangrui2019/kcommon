using System.ComponentModel.DataAnnotations;

namespace KCommon.Core.Validations
{
    public class Equal : ValidationAttribute
    {
        public object Excepted { get; set; }
        
        public override bool IsValid(object actual)
        {
            if (!actual.Equals(Excepted))
            {
                return false;
            }
            return true;
        }

        protected override ValidationResult IsValid(object actual, ValidationContext validationContext)
        {
            if (IsValid(actual))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"The {validationContext.DisplayName} isn't same as {Excepted.GetType()}!");
            }
        }
    }
}