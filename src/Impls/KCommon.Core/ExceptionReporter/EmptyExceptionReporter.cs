using KCommon.Core.Abstract.ExceptionReporter;
using System;
using System.Threading.Tasks;

namespace KCommon.Core.ExceptionReporter
{
    public class EmptyExceptionReporter : IExceptionReporter
    {
        public Task ReportAsync(Exception e, string path = "Inline")
        {
            throw new NotImplementedException();
        }
    }
}
