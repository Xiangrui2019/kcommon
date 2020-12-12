using System;
using System.Threading.Tasks;
using KCommon.Core.Abstract.Logging;
using KCommon.Core.Components;

namespace KCommon.Core.Utilities
{
    public class Cannon
    {
        private readonly ILogger _logger;

        public Cannon(ILoggerFactory loggerFactory)
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

        public void Fire(Action bullet, Action<Exception> handler = null)
        {
            _logger.Info("Fired a new action.");
            Task.Run(() =>
            {
                try
                {
                    bullet();
                }
                catch (Exception e)
                {
                    _logger.Error($"Cannon crashed {e.Message}!");
                    handler?.Invoke(e);
                }
            });
        }
        
        public void FireAsync(Func<Task> bullet, Action<Exception> handler = null)
        {
            _logger.Info("Fired a new async action.");
            Task.Run(async () =>
            {
                try
                {
                    await bullet();
                }
                catch (Exception e)
                {
                    _logger.Error($"Cannon crashed {e.Message}!");
                    handler?.Invoke(e);
                }
            });
        }
    }
}