using Handler.Exceptions;
using KCommon.Web.Models.Message;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KCommon.Web.AOP
{
    public class ApiExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            switch (context.Exception)
            {
                case UnexpectedException exp:
                    context.ExceptionHandled = true;
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Result = new JsonResult(new MessageModel
                    {
                        Code = exp.Code,
                        Message = exp.Message
                    });
                    break;
                case ApiModelException exp:
                    context.ExceptionHandled = true;
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Result = new JsonResult(new MessageModel
                    {
                        Code = exp.Code,
                        Message = exp.Message
                    });
                    break;
            }
        }
    }
}