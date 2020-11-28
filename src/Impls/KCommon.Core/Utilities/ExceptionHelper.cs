using System;

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
    }
}
