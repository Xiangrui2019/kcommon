using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace KCommon.Web.Extensions
{
    public static class HostExtensions
    {
        public static bool IsInKubernetes(this IHost webHost)
        {
            var cfg = (IConfiguration) webHost.Services.GetService(typeof(IConfiguration));

            var orchestratorType = cfg.GetValue<string>("OrchestratorType");
            return orchestratorType?.ToUpper() == "K8S";
        }
    }
}
