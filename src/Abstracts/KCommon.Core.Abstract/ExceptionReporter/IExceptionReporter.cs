using System;
using System.Threading.Tasks;

namespace KCommon.Core.Abstract.ExceptionReporter
{
    public interface IExceptionReporter
    {
        Task ReportAsync(Exception e, string path = "Inline");
    }
}
