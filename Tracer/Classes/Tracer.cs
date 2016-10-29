using System.Diagnostics;
using System.Threading;
using Tracer.Interfaces;

namespace Tracer.Classes
{
    public sealed class Tracer : ITracer
    {
        private readonly TraceResult _traceResult;

        public Tracer()
        {
            _traceResult = new TraceResult();
        }
        public TraceResult GetTraceResult()
        {
            return _traceResult;
        }

        public void StartTrace()
        {
            var method = new StackTrace(1).GetFrame(0).GetMethod();
            var idThread = Thread.CurrentThread.ManagedThreadId;
            _traceResult.StartListenThread(idThread, method);
        }

        public void StopTrace()
        {
            var idThread = Thread.CurrentThread.ManagedThreadId;
            _traceResult.StopListenThread(idThread);
        }
    }
}
