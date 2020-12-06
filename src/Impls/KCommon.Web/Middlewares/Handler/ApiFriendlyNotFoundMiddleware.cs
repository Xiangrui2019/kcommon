using System.Net;
using System.Text;
using System.Threading.Tasks;
using KCommon.Core.Abstract.Serializing;
using KCommon.Web.ErrorCode;
using KCommon.Web.Models.Message;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace KCommon.Web.Middlewares.Handler
{
    public class ApiFriendlyNotFoundMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJsonSerializer _jsonSerializer;

        public ApiFriendlyNotFoundMiddleware(
            RequestDelegate next,
            IJsonSerializer jsonSerializer)
        {
            _next = next;
            _jsonSerializer = jsonSerializer;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next.Invoke(context);
            if (context.Response.StatusCode == StatusCodes.Status404NotFound
                && !context.Response.HasStarted)
            {
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json; charset=utf-8";

                var message = JsonConvert.SerializeObject(new MessageModel
                {
                    Code = ErrorType.NotFound,
                    Message = "对不起, 我们找不到这个资源"
                });

                await context.Response.WriteAsync(message, Encoding.UTF8);
            }
        }
    }
}