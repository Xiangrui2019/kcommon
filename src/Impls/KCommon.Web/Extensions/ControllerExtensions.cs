using KCommon.Web.ErrorCode;
using KCommon.Web.Models.Message;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.Web.Extensions
{
    public static class ControllerExtensions
    {
        public static string TrimController(this string controllerName)
        {
            return controllerName
                .Replace("Controller", "")
                .Replace("Api", "API");
        }

        public static IActionResult Message(this ControllerBase controller, ErrorType errorType, string errorMessage)
        {
            return controller.Ok(new MessageModel
            {
                Code = errorType,
                Message = errorMessage
            });
        }
    }
}
