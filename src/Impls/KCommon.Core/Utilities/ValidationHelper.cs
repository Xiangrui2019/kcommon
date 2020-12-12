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
            var validationContext = new ValidationContext(value);
            var results = new List<ValidationResult>();
            var modelState = new ModelStateDictionary();
            var (isValid, ex) =
                ExceptionHelper.CatchException<bool>(
                    () => Validator.TryValidateObject(value, validationContext, results, true));

            if (ex != null)
            {
                modelState.AddModelError("验证器内部错误", ex.Message);
                return modelState;
            }
            
            if (!isValid)
                foreach (var item in results)
                    modelState.AddModelError(item.MemberNames.First(), item.ErrorMessage);
            
            return modelState;
        }

        public static void EnsureAndThrow(object value)
        {
            var results = new List<ValidationResult>();
            var validationContext = new ValidationContext(value);
            
            var (isValid, ex) =
                ExceptionHelper.CatchException<bool>(
                    () => Validator.TryValidateObject(value, validationContext, results, true));

            if (ex != null)
            {
                throw new ArgumentException($"验证器发生严重内部错误, 请检查 {ex.Message}");
            }

            if (!isValid) throw new ArgumentException(results.First().ErrorMessage);
        }
    }
}