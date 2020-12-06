using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.Web.Middlewares.Failing
{
    public class FailingOptions
    {
        public string ConfigPath = "/failing";

        public List<string> EndpointPaths { get; set; } = new List<string>();
        public List<string> NotFilteredPaths { get; set; } = new List<string>();
    }
}