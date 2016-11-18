using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Tracer.Classes.TracerInfo
{
    internal sealed class MethodTrace
    {
        public List<MethodTrace> NestedMethods { get; }
        public MethodMetadata Metadata;
        private Stopwatch _stopwatch;

        internal MethodTrace(MethodBase methodBase)
        {
            CreateStopwatch();
            Metadata = new MethodMetadata(methodBase);
            NestedMethods = new List<MethodTrace>();
        }

        public void AddNestedMethod(MethodTrace nestedTrace)
        {
            NestedMethods.Add(nestedTrace);
        }

        public void StopMeteringTime()
        {
            _stopwatch.Stop();
        }

        private void CreateStopwatch()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public long GetExecutionTime()
        {
            return _stopwatch.ElapsedMilliseconds;
        }
    }
}
