using System.Collections.Generic;
using System.Linq;

namespace Tracer.Classes.TracerInfo
{
    internal sealed class ThreadTrace
    {
        private readonly Stack<MethodTrace> _stackMethods;
        public List<MethodTrace> MethodList { get; }

        internal ThreadTrace()
        {
            _stackMethods = new Stack<MethodTrace>();
            MethodList = new List<MethodTrace>();
        }

        public long ExecutionTime => MethodList.Select(p => p.GetExecutionTime()).Sum();

        public void StartListenMethod(MethodTrace methodTrace)
        {
            if(_stackMethods.Count == 0)
            {
                MethodList.Add(methodTrace);
            }
            else
            {
                _stackMethods.Peek().AddNestedMethod(methodTrace);
            }
            _stackMethods.Push(methodTrace);
        }

        public void StopListenMethod()
        {
            _stackMethods.Pop().StopMeteringTime();
        }
    }
}
