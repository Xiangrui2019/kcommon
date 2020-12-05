using KCommon.Web.Middlewares.Failing;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.Web.Configurations
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseFailingMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<FailingMiddleware>();
            return builder;
        }
    }
}
