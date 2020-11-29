using KCommon.Core.Abstract.Logging;
using KCommon.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.Core.Async
{
    public class CannonService
    {
        private readonly ILogger _logger;

        public CannonService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.Create(GetType().FullName);
        }

        public void Fire<T>(Action<T> bullet, Action<Exception> handler = null)
        {
            _logger.Info("Fired a new action.");
            Task.Run(() =>
            {
                var dependency = ObjectContainer.Resolve(typeof(T));
                try
                {
                    bullet((T) dependency);
                }
                catch (Exception e)
                {
                    _logger.Error($"Cannon crashed {e.Message}!");
                    handler?.Invoke(e);
                }
                finally
                {
                    dependency = default;
                }
            });
        }

        public void FireAsync<T>(Func<T, Task> bullet, Action<Exception> handler = null)
        {
            _logger.Info("Fired a new async action.");
            Task.Run(async () =>
            {
                var dependency = ObjectContainer.Resolve(typeof(T));
                try
                {
                    await bullet((T) dependency);
                }
                catch (Exception e)
                {
                    _logger.Error($"Cannon crashed {e.Message}!");
                    handler?.Invoke(e);
                }
                finally
                {
                    dependency = default;
                }
            });
        }
    }
}
