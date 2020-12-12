using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KCommon.Core.Utilities
{
    public static class ValidationHelper
    {
        public static ModelStateDictionary Ensure(object value)
        {
            var modelState = new ModelStateDictionary();

            try
            {
                var validationContext = new ValidationContext(value);
                var results = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(value, validationContext, results, true);

                if (!isValid)
                    foreach (var item in results)
                        modelState.AddModelError(item.MemberNames.First(), item.ErrorMessage);
            }
            catch (Exception ex)
            {
                modelState.AddModelError("验证器内部错误", ex.Message);
            }

            return modelState;
        }

        public static void EnsureAndThrow(object value)
        {
            try
            {
                var validationContext = new ValidationContext(value);
                var results = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(value, validationContext, results, true);

                if (!isValid) throw new ArgumentException(results.First().ErrorMessage);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"验证器发生严重内部错误, 请检查 {ex.Message}");
            }
        }
    }
}