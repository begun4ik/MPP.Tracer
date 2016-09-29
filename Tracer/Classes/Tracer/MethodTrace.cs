using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Classes.Tracer
{
    class MethodTrace
    {
        public List<MethodTrace> NestedMethods { get; }
        public TracerInfo.MethodInfo MethodInfo;
        private Stopwatch _stopwatch;

        internal MethodTrace(MethodBase methodBase)
        {
            CreateStopwatch();
            MethodInfo = new TracerInfo.MethodInfo(methodBase);
            NestedMethods = new List<MethodTrace>();
        }

        public void AddNestedMethod(MethodTrace methodTrace)
        {
            NestedMethods.Add(methodTrace);
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
