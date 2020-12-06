using KCommon.Web.Middlewares.Failing;
using KCommon.Web.Middlewares.Handler;
using Microsoft.AspNetCore.Builder;

namespace KCommon.Web.Configurations
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseFailing(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<FailingMiddleware>();

            return builder;
        }

        public static IApplicationBuilder UseApiFriendlyHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ApiFriendlyExceptionMiddleware>();
            builder.UseMiddleware<ApiFriendlyNotFoundMiddleware>();

            return builder;
        }
    }
}