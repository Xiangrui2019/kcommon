using KCommon.Core.Abstract.Logging;
using KCommon.Core.Abstract.Serializing;
using KCommon.Web.ErrorCode;
using KCommon.Web.Models.Message;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KCommon.Web.Middlewares.Failing
{
    public class FailingMiddleware
    {
        private readonly RequestDelegate _next;
        private bool _mustFail;
        private readonly FailingOptions _options;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ILogger _logger;

        public FailingMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory,
            FailingOptions options,
            IJsonSerializer jsonSerializer)
        {
            _next = next;
            _options = options;
            _jsonSerializer = jsonSerializer;
            _mustFail = false;
            _logger = loggerFactory.Create(GetType().FullName);
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;
            if (path.Equals(_options.ConfigPath, StringComparison.OrdinalIgnoreCase))
            {
                await ProcessConfigRequest(context);
                return;
            }

            if (MustFail(context))
            {
                _logger.Info($"Response for path {path} will fail.");
                await SendNoResponse(context, new MessageModel
                {
                    Code = ErrorType.Breaker,
                    Message = "对不起, 服务器无法处理您的请求."
                });
            }
            else
            {
                await _next.Invoke(context);
            }
        }

        private async Task ProcessConfigRequest(HttpContext context)
        {
            var enable = context.Request.Query.Keys.Any(k => k == "enable");
            var disable = context.Request.Query.Keys.Any(k => k == "disable");

            if (enable && disable) throw new ArgumentException("必须只传入一个输入, 两个不行");

            if (disable)
            {
                _mustFail = false;
                await SendOkResponse(context, new MessageModel
                {
                    Code = ErrorType.Success,
                    Message = "熔断器关闭, 请求可以正常处理."
                });

                return;
            }

            if (enable)
            {
                _mustFail = true;
                await SendOkResponse(context, new MessageModel
                {
                    Code = ErrorType.Success,
                    Message = "熔断器打开, 请求会被全部拦截."
                });
                return;
            }

            await SendOkResponse(context, new MessageModel
            {
                Code = ErrorType.Success,
                Message = "熔断器关闭, 请求可以正常处理."
            });
            return;
        }

        private async Task SendOkResponse(HttpContext context, MessageModel message)
        {
            context.Response.StatusCode = (int) System.Net.HttpStatusCode.OK;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(
                _jsonSerializer.Serialize(message));
        }

        private async Task SendNoResponse(HttpContext context, MessageModel message)
        {
            context.Response.StatusCode = (int) System.Net.HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(
                _jsonSerializer.Serialize(message));
        }

        private bool MustFail(HttpContext context)
        {
            var rpath = context.Request.Path.Value;

            if (_options.NotFilteredPaths.Any(p => p.Equals(rpath, StringComparison.InvariantCultureIgnoreCase)))
                return false;

            return _mustFail &&
                   (_options.EndpointPaths.Any(x => x == rpath)
                    || _options.EndpointPaths.Count == 0);
        }
    }
}