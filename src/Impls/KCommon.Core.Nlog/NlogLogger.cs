using NLog;
using System;

namespace KCommon.Core.Nlog
{
    public class NlogLogger : KCommon.Core.Abstract.Logging.ILogger
    {
        private readonly Logger _log;

        public NlogLogger(Logger log)
        {
            _log = log;
        }

        public bool IsDebugEnabled => _log.IsDebugEnabled;

        public void Debug(string message)
        {
            _log.Debug(message);
        }

        public void Debug(string message, Exception exception)
        {
            _log.Debug($"{message} - {exception.Message}");
        }

        public void DebugFormat(string format, params object[] args)
        {
            _log.Debug(string.Format(format, args));
        }

        public void Error(string message)
        {
            _log.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            _log.Error($"{message} - {exception.Message}");
        }

        public void ErrorFormat(string format, params object[] args)
        {
            _log.Error(string.Format(format, args));
        }

        public void Fatal(string message)
        {
            _log.Fatal(message);
        }

        public void Fatal(string message, Exception exception)
        {
            _log.Fatal($"{message} - {exception.Message}");
        }

        public void FatalFormat(string format, params object[] args)
        {
            _log.Fatal(string.Format(format, args));
        }

        public void Info(string message)
        {
            _log.Info(message);
        }

        public void Info(string message, Exception exception)
        {
            _log.Info($"{message} - {exception.Message}");
        }

        public void InfoFormat(string format, params object[] args)
        {
            _log.Info(string.Format(format, args));
        }

        public void Warn(string message)
        {
            _log.Warn(message);
        }

        public void Warn(string message, Exception exception)
        {
            _log.Warn($"{message} - {exception.Message}");
        }

        public void WarnFormat(string format, params object[] args)
        {
            _log.Warn(string.Format(format, args));
        }
    }
}
