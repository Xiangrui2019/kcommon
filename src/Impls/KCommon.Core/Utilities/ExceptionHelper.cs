using System;
using System.Threading.Tasks;

namespace KCommon.Core.Utilities
{
    public static class ExceptionHelper
    {
        public static void EatException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static async Task EatExceptionAsync(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static T EatException<T>(Func<T> action, T defaultValue = default(T))
        {
            try
            {
                return action();
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static async Task<T> EatExceptionAsync<T>(Func<Task<T>> action, T defaultValue = default(T))
        {
            try
            {
                return await action();
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static (T, Exception) CatchException<T>(Func<T> action, T defaultValue = default(T))
        {
            try
            {
                return (action(), null);
            }
            catch (Exception e)
            {
                return (defaultValue, e);
            }
        }

        public static async Task<(T, Exception)> CatchExceptionAsync<T>(Func<Task<T>> action,
            T defaultValue = default(T))
        {
            try
            {
                return (await action(), null);
            }
            catch (Exception e)
            {
                return (defaultValue, e);
            }
        }
    }
}