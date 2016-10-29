using System.Collections.Concurrent;
using System.Reflection;
using Tracer.Classes.TracerInfo;

namespace Tracer.Classes
{
    public class TraceResult
    {
        internal ConcurrentDictionary<int, ThreadTrace> ThreadsInfo;
        internal TraceResult()
        {
            ThreadsInfo = new ConcurrentDictionary<int, ThreadTrace>();
        }

        public void StartListenThread(int idThread, MethodBase methodBase)
        {
            var threadInfo = ThreadsInfo.GetOrAdd(idThread, new ThreadTrace());
            threadInfo.StartListenMethod(new MethodTrace(methodBase));
        }

        public void StopListenThread(int idThread)
        {
            ThreadTrace threadInfo;
            if(ThreadsInfo.TryGetValue(idThread, out threadInfo))
            {
                threadInfo.StopListenMethod();
            }
        }

        //public IEnumerable<KeyValuePair<int, ThreadTrace>> ThreadsInfo { get { return _threadsInfo; } }
    }
}
