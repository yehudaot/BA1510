using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace atpLib.Infra
{
    public static class TaskExtention
    {
        public static Task<T> TimeoutAfter<T>(this Task<T> task, int millisecondsTimeout)
        {
            return task.ToObservable<T>().Timeout(TimeSpan.FromMilliseconds(millisecondsTimeout)).ToTask<T>();
        }
    }
}
