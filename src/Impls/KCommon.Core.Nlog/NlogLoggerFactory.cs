using KCommon.Core.Abstract.Logging;
using System;

namespace KCommon.Core.Nlog
{
    public class NlogLoggerFactory : ILoggerFactory
    {
        public ILogger Create(string name)
        {
            return new NlogLogger(NLog.LogManager.GetLogger(name));
        }

        public ILogger Create(Type type)
        {
            return new NlogLogger(NLog.LogManager.GetLogger(type.FullName));
        }
    }
}