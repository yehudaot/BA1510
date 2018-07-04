using System;
using System.Threading.Tasks;


using atpLib.Infra;
using System.Threading;

namespace atpLib.Scenarios
{
    public abstract class Scenario
    {
        public class ScenarioResult
        {
            public enum RunResult
            {
                Pass,
                Fail,
                Canceled,
                TimedOut,
            }

            public RunResult result {get; set;}
            public object resultObj { get; set; }

            public ScenarioResult() {}

            public ScenarioResult(RunResult result, object resultObj)
            {
                this.result = result;
                this.resultObj = resultObj;
            }

            public ScenarioResult(RunResult result) : this(result, null)
            {

            }
        }

        public class ScenarioException : Exception
        {
            public ScenarioException()
            {
            }

            public ScenarioException(string message)
                : base(message)
            {
            }

            public ScenarioException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public const int DEFAULT_TIMEOUT = 10000;

        private Task<Task<ScenarioResult>> backgroundTask;
        CancellationTokenSource internalCancelationToken;

        public string name {get; set;}
        public int timeout {get; set;}
        
        public Scenario(string name)
        {
            this.name = name;
            this.timeout = DEFAULT_TIMEOUT;

            internalCancelationToken = new CancellationTokenSource();
        }

        protected abstract ScenarioResult internalRun(CancellationToken ct);

        /// <summary>
        /// synchronously run a task
        /// </summary>
        /// <param name="timeout">time in ms before TimeoutException will be thrown</param>
        /// <returns>ScenarioResult</returns>
        public ScenarioResult run(int timeout)
        {
            this.timeout = timeout;
            return run();
        }

        /// <summary>
        /// synchronously run a task
        /// </summary>
        /// <param name="timeout">time in ms before TimeoutException will be thrown</param>
        /// <param name="ct">an external cancellation token</param>
        /// <returns>ScenarioResult</returns>
        public ScenarioResult run(int timeout, CancellationToken ct)
        {
            this.timeout = timeout;
            return run(ct);
        }

        /// <summary>
        /// synchronously run a task
        /// default timeout of <paramref name="DEFAULT_TIMEOUT"/> is used
        /// </summary>
        /// <returns>ScenarioResult</returns>
        public ScenarioResult run()
        {
            /* use local cancellationToken */
            return run(new CancellationToken());
        }

        /// <summary>
        /// synchronously run a task
        /// default timeout of <paramref name="DEFAULT_TIMEOUT"/> is used
        /// <param name="externalCancelationToken">an external cancellation token</param>
        /// </summary>
        /// <returns>ScenarioResult</returns>
        public ScenarioResult run(CancellationToken externalCancelationToken)
        {
            try {
                Task<Task<ScenarioResult>> task = Task<Task<ScenarioResult>>.Factory.StartNew(() => {
                    return runTask(externalCancelationToken);

                }, externalCancelationToken);

                while (!task.IsCompleted)
                {
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(1);
                }

                return (task.Result == null) ? null : task.Result.Result;
            } catch (Exception ex)
            {
                /* act on the inner most exception */
                Exception tEx = ex;
                while(tEx.InnerException!=null)
                {
                    tEx = tEx.InnerException;
                }

                if (tEx is TimeoutException)  
                {
                    log.Warn("Task Timed out!");
                } else if(tEx is TaskCanceledException)
                {
                    log.Warn("Task canceled");
                } else
                {
                    log.Error("Internal Scenario Error", tEx);
                }
            }
            return null;
        }

        public Scenario backgroundStart()
        {
            return backgroundStart(new CancellationToken());
        }

        public Scenario backgroundStart(CancellationToken externalCancelationToken)
        {
            if(backgroundTask !=null && backgroundTask.IsCompleted == false)
            {
                throw new NotSupportedException("multiple background operations of the same task object is not supported!");
            }

            this.timeout = int.MaxValue;
            backgroundTask = Task<Task<ScenarioResult>>.Factory.StartNew(() => {
                   return runTask(externalCancelationToken);
            }, externalCancelationToken);

            /* let UI elements time to update */
            System.Windows.Forms.Application.DoEvents();
            System.Threading.Thread.Sleep(1);
            return this;
        }

        public TaskStatus backgroundStatus()
        {
            return backgroundTask.Status;
        }

        public ScenarioResult backgroundWaitFinish()
        {
            while (!backgroundTask.IsCompleted)
            {
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            return backgroundTask.Result.Result;
        }

        public void backgroundFinish()
        {
            internalCancelationToken.Cancel();

            //return backgroundTask.Result.Result;
        }

        private Task<ScenarioResult> runTask(CancellationToken externalCancelationToken)
        {
            Task<ScenarioResult> result = null;
            try
            {
                log.Info("Running scenario \"" + this.name + "\" - with timeout of: " + (this.timeout / 1000).ToString() + " seconds.");

                Task<ScenarioResult> t = Task<int>.Run<ScenarioResult>(() => internalRun(internalCancelationToken.Token), internalCancelationToken.Token);
                DateTime startTime = DateTime.Now;
                while (!t.IsCompleted)
                {
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(1);
                    TimeSpan diff = DateTime.Now - startTime;
                    if (externalCancelationToken.IsCancellationRequested == true || diff.TotalMilliseconds > this.timeout)
                    {
                        internalCancelationToken.Cancel();
                    }

                }

                if(t.IsFaulted && t.Exception.InnerException is TimeoutException)
                {
                    /* timeout exception */
                    internalCancelationToken.Cancel();
                    System.Threading.Thread.Sleep(1);
                }

                result = t;
            }
            catch (Exception ex)
            {
                if(ex.InnerException != null) 
                {
                    ex = ex.InnerException;
                }
                if (ex is OperationCanceledException)
                {
                    log.Info("Task " + this.name + " was terminated");
                } else if(ex is TimeoutException)
                {
                    log.Info("Task " + this.name + " has timed out!");
                    
                } else
                {
                    log.Error("Internal Scenario Error", ex);
                }
            }
            finally
            {
                //TODO: rename result to something else!
                if ((result?.Status != TaskStatus.Faulted && result?.Result?.result == ScenarioResult.RunResult.Pass))
                {
                    log.Info("Scenario Passed.");
                } else {
                    log.Warn((result?.Result?.result == ScenarioResult.RunResult.Fail) ? "failed." : (result?.Result?.result == ScenarioResult.RunResult.Canceled) ? "Canceled." : "Timed-Out.");
                }
            }
            return result;
        }
    }
}
