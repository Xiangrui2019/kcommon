using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using KCommon.Core.Abstract.Logging;
using KCommon.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace KCommon.Web.Middlewares.Handler
{
    public class ApiFriendlyExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _environment;

        public ApiFriendlyExceptionMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory, 
            IHostingEnvironment environment)
        {
            _next = next;
            _logger = loggerFactory.Create(GetType().FullName);
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.Clear();
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json; charset=utf-8";
                    var message = JsonConvert.SerializeObject(
                        ReusltGenerator.GetServerExceptionResponse(
                            e.StackTrace, _environment.IsDevelopment()));
                    await context.Response.WriteAsync(message, Encoding.UTF8);
                    _logger.Error(e.Message, e);

                    return;
                }
                throw;
            }
        }
    }
}