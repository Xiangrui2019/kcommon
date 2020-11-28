﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KCommon.Core.Abstract.IO;
using KCommon.Core.Abstract.Logging;
using KCommon.Core.Extensions;
using KCommon.Core.Utilities;

namespace KCommon.Core.IO
{
    public class IOHelper
    {
        private readonly ILogger _logger;

        public IOHelper(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.Create(GetType().FullName);
        }

        public void TryIOAction(string actionName, Func<string> getContextInfo, Action action, int maxRetryTimes, bool continueRetryWhenRetryFailed = false, int retryInterval = 1000)
        {
            Ensure.NotNull(actionName, "actionName");
            Ensure.NotNull(getContextInfo, "getContextInfo");
            Ensure.NotNull(action, "action");
            TryIOActionRecursivelyInternal(actionName, getContextInfo, (x, y, z) => action(), 0, maxRetryTimes, continueRetryWhenRetryFailed, retryInterval);
        }
        public T TryIOFunc<T>(string funcName, Func<string> getContextInfo, Func<T> func, int maxRetryTimes, bool continueRetryWhenRetryFailed = false, int retryInterval = 1000)
        {
            Ensure.NotNull(funcName, "funcName");
            Ensure.NotNull(getContextInfo, "getContextInfo");
            Ensure.NotNull(func, "func");
            return TryIOFuncRecursivelyInternal(funcName, getContextInfo, (x, y, z) => func(), 0, maxRetryTimes, continueRetryWhenRetryFailed, retryInterval);
        }

        #region TryAsyncActionRecursively

        public void TryAsyncActionRecursively<TResult>(
            string asyncActionName,
            Func<Task<TResult>> asyncAction,
            Action<int> mainAction,
            Action<TResult> successAction,
            Func<string> getContextInfoFunc,
            Action<Exception, string> failedAction,
            int retryTimes,
            bool retryWhenFailed = false,
            int maxRetryTimes = 3,
            int retryInterval = 1000)
        {
            try
            {
                asyncAction().ContinueWith(TaskContinueAction, new TaskExecutionContext<TResult>
                {
                    AsyncActionName = asyncActionName,
                    MainAction = mainAction,
                    SuccessAction = successAction,
                    GetContextInfoFunc = getContextInfoFunc,
                    FailedAction = failedAction,
                    RetryTimes = retryTimes,
                    RetryWhenFailed = retryWhenFailed,
                    MaxRetryTimes = maxRetryTimes,
                    RetryInterval = retryInterval
                });
            }
            catch (IOException ex)
            {
                _logger.Error(string.Format("IOException raised when executing async task '{0}', contextInfo:{1}, current retryTimes:{2}, try to execute the async task again.", asyncActionName, GetContextInfo(getContextInfoFunc), retryTimes), ex);
                ExecuteRetryAction(asyncActionName, getContextInfoFunc, mainAction, retryTimes, maxRetryTimes, retryInterval);
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Unknown exception raised when executing async task '{0}', contextInfo:{1}, current retryTimes:{2}", asyncActionName, GetContextInfo(getContextInfoFunc), retryTimes), ex);
                if (retryWhenFailed)
                {
                    ExecuteRetryAction(asyncActionName, getContextInfoFunc, mainAction, retryTimes, maxRetryTimes, retryInterval);
                }
                else
                {
                    ExecuteFailedAction(asyncActionName, getContextInfoFunc, failedAction, ex, ex.Message);
                }
            }
        }
        public void TryAsyncActionRecursivelyWithoutResult(
            string asyncActionName,
            Func<Task> asyncAction,
            Action<int> mainAction,
            Action successAction,
            Func<string> getContextInfoFunc,
            Action<Exception, string> failedAction,
            int retryTimes,
            bool retryWhenFailed = false,
            int maxRetryTimes = 3,
            int retryInterval = 1000)
        {
            try
            {
                asyncAction().ContinueWith(TaskContinueAction, new TaskExecutionContext
                {
                    AsyncActionName = asyncActionName,
                    MainAction = mainAction,
                    SuccessAction = successAction,
                    GetContextInfoFunc = getContextInfoFunc,
                    FailedAction = failedAction,
                    RetryTimes = retryTimes,
                    RetryWhenFailed = retryWhenFailed,
                    MaxRetryTimes = maxRetryTimes,
                    RetryInterval = retryInterval
                });
            }
            catch (IOException ex)
            {
                _logger.Error(string.Format("IOException raised when executing async task '{0}', contextInfo:{1}, current retryTimes:{2}, try to execute the async task again.", asyncActionName, GetContextInfo(getContextInfoFunc), retryTimes), ex);
                ExecuteRetryAction(asyncActionName, getContextInfoFunc, mainAction, retryTimes, maxRetryTimes, retryInterval);
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Unknown exception raised when executing async task '{0}', contextInfo:{1}, current retryTimes:{2}", asyncActionName, GetContextInfo(getContextInfoFunc), retryTimes), ex);
                if (retryWhenFailed)
                {
                    ExecuteRetryAction(asyncActionName, getContextInfoFunc, mainAction, retryTimes, maxRetryTimes, retryInterval);
                }
                else
                {
                    ExecuteFailedAction(asyncActionName, getContextInfoFunc, failedAction, ex, ex.Message);
                }
            }
        }

        private string GetContextInfo(Func<string> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                _logger.Error("Failed to execute the getContextInfoFunc.", ex);
                return null;
            }
        }
        private void ExecuteFailedAction(string asyncActionName, Func<string> getContextInfoFunc, Action<Exception, string> failedAction, Exception exception, string errorMessage)
        {
            try
            {
                failedAction?.Invoke(exception, errorMessage);
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Failed to execute the failedAction of asyncAction:{0}, contextInfo:{1}", asyncActionName, GetContextInfo(getContextInfoFunc)), ex);
            }
        }
        private void ExecuteRetryAction(string asyncActionName, Func<string> getContextInfoFunc, Action<int> mainAction, int currentRetryTimes, int maxRetryTimes, int retryInterval)
        {
            try
            {
                if (currentRetryTimes >= maxRetryTimes)
                {
                    Task.Factory.StartDelayedTask(retryInterval, () => mainAction(currentRetryTimes + 1));
                }
                else
                {
                    mainAction(currentRetryTimes + 1);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Failed to execute the retryAction, asyncActionName:{0}, contextInfo:{1}", asyncActionName, GetContextInfo(getContextInfoFunc)), ex);
            }
        }
        private void ProcessTaskException(string asyncActionName, Func<string> getContextInfoFunc, Action<int> mainAction, Action<Exception, string> failedAction, Exception exception, int currentRetryTimes, int maxRetryTimes, int retryInterval, bool retryWhenFailed)
        {
            if (exception is IOException)
            {
                _logger.Error(string.Format("Async task '{0}' has io exception, contextInfo:{1}, current retryTimes:{2}, try to run the async task again.", asyncActionName, GetContextInfo(getContextInfoFunc), currentRetryTimes), exception);
                ExecuteRetryAction(asyncActionName, getContextInfoFunc, mainAction, currentRetryTimes, maxRetryTimes, retryInterval);
            }
            else if (exception is AggregateException
                && ((AggregateException)exception).InnerExceptions.IsNotEmpty()
                && ((AggregateException)exception).InnerExceptions.Any(x => x is IOException))
            {
                _logger.Error(string.Format("Async task '{0}' has aggregate exception, contextInfo:{1}, current retryTimes:{2}, try to run the async task again.", asyncActionName, GetContextInfo(getContextInfoFunc), currentRetryTimes), exception);
                ExecuteRetryAction(asyncActionName, getContextInfoFunc, mainAction, currentRetryTimes, maxRetryTimes, retryInterval);
            }
            else
            {
                _logger.Error(string.Format("Async task '{0}' has unknown exception, contextInfo:{1}, current retryTimes:{2}", asyncActionName, GetContextInfo(getContextInfoFunc), currentRetryTimes), exception);
                if (retryWhenFailed)
                {
                    ExecuteRetryAction(asyncActionName, getContextInfoFunc, mainAction, currentRetryTimes, maxRetryTimes, retryInterval);
                }
                else
                {
                    var realException = GetRealException(exception);
                    ExecuteFailedAction(asyncActionName, getContextInfoFunc, failedAction, realException, realException.Message);
                }
            }
        }
        private void TaskContinueAction<TResult>(Task<TResult> task, object obj)
        {
            var context = obj as TaskExecutionContext<TResult>;
            try
            {
                if (task.Exception != null)
                {
                    ProcessTaskException(
                        context.AsyncActionName,
                        context.GetContextInfoFunc,
                        context.MainAction,
                        context.FailedAction,
                        task.Exception,
                        context.RetryTimes,
                        context.MaxRetryTimes,
                        context.RetryInterval,
                        context.RetryWhenFailed);
                    return;
                }
                if (task.IsCanceled)
                {
                    _logger.ErrorFormat("Async task '{0}' was cancelled, contextInfo:{1}, current retryTimes:{2}.",
                        context.AsyncActionName,
                        GetContextInfo(context.GetContextInfoFunc),
                        context.RetryTimes);
                    ExecuteFailedAction(
                        context.AsyncActionName,
                        context.GetContextInfoFunc,
                        context.FailedAction,
                        task.Exception,
                        string.Format("Async task '{0}' was cancelled.", context.AsyncActionName));
                    return;
                }
                context.SuccessAction?.Invoke(task.Result);
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Failed to execute the taskContinueAction, asyncActionName:{0}, contextInfo:{1}",
                    context.AsyncActionName,
                    GetContextInfo(context.GetContextInfoFunc)), ex);
            }
        }
        private void TaskContinueAction(Task task, object obj)
        {
            var context = obj as TaskExecutionContext;
            try
            {
                if (task.Exception != null)
                {
                    ProcessTaskException(
                        context.AsyncActionName,
                        context.GetContextInfoFunc,
                        context.MainAction,
                        context.FailedAction,
                        task.Exception,
                        context.RetryTimes,
                        context.MaxRetryTimes,
                        context.RetryInterval,
                        context.RetryWhenFailed);
                    return;
                }
                if (task.IsCanceled)
                {
                    _logger.ErrorFormat("Async task '{0}' was cancelled, contextInfo:{1}, current retryTimes:{2}.",
                        context.AsyncActionName,
                        GetContextInfo(context.GetContextInfoFunc),
                        context.RetryTimes);
                    ExecuteFailedAction(
                        context.AsyncActionName,
                        context.GetContextInfoFunc,
                        context.FailedAction,
                        task.Exception,
                        string.Format("Async task '{0}' was cancelled.", context.AsyncActionName));
                    return;
                }
                context.SuccessAction?.Invoke();
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Failed to execute the taskContinueAction, asyncActionName:{0}, contextInfo:{1}",
                    context.AsyncActionName,
                    GetContextInfo(context.GetContextInfoFunc)), ex);
            }
        }
        private Exception GetRealException(Exception exception)
        {
            if (exception is AggregateException && ((AggregateException)exception).InnerExceptions.IsNotEmpty())
            {
                return ((AggregateException)exception).InnerExceptions.First();
            }
            return exception;
        }

        class TaskExecutionContext<TResult>
        {
            public string AsyncActionName;
            public Action<int> MainAction;
            public Action<TResult> SuccessAction;
            public Func<string> GetContextInfoFunc;
            public Action<Exception, string> FailedAction;
            public int RetryTimes;
            public bool RetryWhenFailed;
            public int MaxRetryTimes;
            public int RetryInterval;
        }
        class TaskExecutionContext
        {
            public string AsyncActionName;
            public Action<int> MainAction;
            public Action SuccessAction;
            public Func<string> GetContextInfoFunc;
            public Action<Exception, string> FailedAction;
            public int RetryTimes;
            public bool RetryWhenFailed;
            public int MaxRetryTimes;
            public int RetryInterval;
        }

        #endregion

        public void TryIOAction(Action action, string actionName)
        {
            Ensure.NotNull(action, "action");
            Ensure.NotNull(actionName, "actionName");
            try
            {
                action();
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new IOException(string.Format("{0} failed.", actionName), ex);
            }
        }
        public async Task TryIOActionAsync(Func<Task> action, string actionName)
        {
            Ensure.NotNull(action, "action");
            Ensure.NotNull(actionName, "actionName");
            try
            {
                await action();
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new IOException(string.Format("{0} failed.", actionName), ex);
            }
        }
        public T TryIOFunc<T>(Func<T> func, string funcName)
        {
            Ensure.NotNull(func, "func");
            Ensure.NotNull(funcName, "funcName");
            try
            {
                return func();
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new IOException(string.Format("{0} failed.", funcName), ex);
            }
        }
        public async Task<T> TryIOFuncAsync<T>(Func<Task<T>> func, string funcName)
        {
            Ensure.NotNull(func, "func");
            Ensure.NotNull(funcName, "funcName");
            try
            {
                return await func();
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new IOException(string.Format("{0} failed.", funcName), ex);
            }
        }

        private void TryIOActionRecursivelyInternal(string actionName, Func<string> getContextInfo, Action<string, Func<string>, int> action, int retryTimes, int maxRetryTimes, bool continueRetryWhenRetryFailed = false, int retryInterval = 1000)
        {
            try
            {
                action(actionName, getContextInfo, retryTimes);
            }
            catch (IOException ex)
            {
                var errorMessage = string.Format("IOException raised when executing action '{0}', currentRetryTimes:{1}, maxRetryTimes:{2}, contextInfo:{3}", actionName, retryTimes, maxRetryTimes, getContextInfo());
                _logger.Error(errorMessage, ex);
                if (retryTimes >= maxRetryTimes)
                {
                    if (!continueRetryWhenRetryFailed)
                    {
                        throw;
                    }
                    else
                    {
                        Thread.Sleep(retryInterval);
                    }
                }
                retryTimes++;
                TryIOActionRecursivelyInternal(actionName, getContextInfo, action, retryTimes, maxRetryTimes, continueRetryWhenRetryFailed, retryInterval);
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format("Unknown exception raised when executing action '{0}', currentRetryTimes:{1}, maxRetryTimes:{2}, contextInfo:{3}", actionName, retryTimes, maxRetryTimes, getContextInfo());
                _logger.Error(errorMessage, ex);
                throw;
            }
        }
        private T TryIOFuncRecursivelyInternal<T>(string funcName, Func<string> getContextInfo, Func<string, Func<string>, long, T> func, int retryTimes, int maxRetryTimes, bool continueRetryWhenRetryFailed = false, int retryInterval = 1000)
        {
            try
            {
                return func(funcName, getContextInfo, retryTimes);
            }
            catch (IOException ex)
            {
                var errorMessage = string.Format("IOException raised when executing func '{0}', currentRetryTimes:{1}, maxRetryTimes:{2}, contextInfo:{3}", funcName, retryTimes, maxRetryTimes, getContextInfo());
                _logger.Error(errorMessage, ex);
                if (retryTimes >= maxRetryTimes)
                {
                    if (!continueRetryWhenRetryFailed)
                    {
                        throw;
                    }
                    else
                    {
                        Thread.Sleep(retryInterval);
                    }
                }
                retryTimes++;
                return TryIOFuncRecursivelyInternal(funcName, getContextInfo, func, retryTimes, maxRetryTimes, continueRetryWhenRetryFailed, retryInterval);
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format("Unknown exception raised when executing func '{0}', currentRetryTimes:{1}, maxRetryTimes:{2}, contextInfo:{3}", funcName, retryTimes, maxRetryTimes, getContextInfo());
                _logger.Error(errorMessage, ex);
                throw;
            }
        }
    }
}