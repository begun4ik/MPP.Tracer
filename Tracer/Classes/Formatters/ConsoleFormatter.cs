using System;
using Tracer.Classes;
using Tracer.Classes.TracerInfo;
using Tracer.Interfaces;

namespace Tracer.Classes.Formatters
{
    public sealed class ConsoleFormatter: ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            if (traceResult == null) return;
            foreach (var threadInfo in traceResult.ThreadsInfo)
            {
                Console.WriteLine($"Thread id: {threadInfo.Key}; Time: {threadInfo.Value.ExecutionTime};");
                foreach (var methodInfo in threadInfo.Value.MethodList)
                {
                    WriteMthodInfo(methodInfo);
                }
                Console.WriteLine();
            }
        }

        private void WriteMthodInfo(MethodTrace methodInfo, int nestingLevel = 0)
        {
            var nesting = new string('\t', nestingLevel);
            Console.WriteLine($"{nesting}MethodName: {methodInfo.Metadata.Name}; Time: {methodInfo.GetExecutionTime()}; " +
                $" Class: {methodInfo.Metadata.ClassName}; Params: {methodInfo.Metadata.CountParameters}");
            foreach(var nestedMethod in methodInfo.NestedMethods)
            {
                WriteMthodInfo(nestedMethod, nestingLevel + 1);
            }
        }
    }
}
