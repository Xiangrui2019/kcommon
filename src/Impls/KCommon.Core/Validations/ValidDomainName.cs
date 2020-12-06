using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace KCommon.Core.Validations
{
    public class ValidDomainName : ValidationAttribute
    {
        private readonly string _domainRegex = @"^[-a-z0-9_.]+$";

        public override bool IsValid(object value)
        {
            var regex = new Regex(_domainRegex, RegexOptions.Compiled);
            if (value is string val) return regex.IsMatch(val);
            return true;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (IsValid(value))
                return ValidationResult.Success;
            else
                return new ValidationResult($"{validationContext.DisplayName} 只能包含 字母下划线和数字.");
        }
    }
}