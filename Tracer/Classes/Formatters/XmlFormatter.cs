using System.Collections.Generic;
using System.Xml.Linq;
using Tracer.Classes.TracerInfo;
using Tracer.Interfaces;

namespace Tracer.Classes.Formatters
{
    public sealed class XmlFormatter: ITraceResultFormatter
    {
        private readonly string _filePath;
        public XmlFormatter(string filePath)
        {
            _filePath = filePath ?? "DefaultResult.xml";
        }

        public void Format(TraceResult traceResult)
        {
            if (traceResult == null) return;
            var xDoc = new XDocument();
            var rootElement = new XElement("root");

            foreach (var threadInfo in traceResult.ThreadsInfo)
            {
                var threadElement = GetInfoThread(threadInfo);

                foreach (var methodInfo in threadInfo.Value.MethodList)
                {
                    threadElement.Add(GetAllInfoMethod(methodInfo));
                }
                rootElement.Add(threadElement);
            }
            xDoc.Add(rootElement);
            xDoc.Save(_filePath);
        }

        private XElement GetAllInfoMethod(MethodTrace methodInfo)
        {
            var result = GetInfoMethod(methodInfo);
            foreach(var nestedMethod in methodInfo.NestedMethods)
            {
                result.Add(GetAllInfoMethod(nestedMethod));
            }
            return result;
        }

        private XElement GetInfoMethod(MethodTrace methodInfo)
        {
            var result = new XElement("method");
            result.Add(new XAttribute("name", methodInfo.Metadata.Name));
            result.Add(new XAttribute("time", methodInfo.GetExecutionTime()));
            result.Add(new XAttribute("class", methodInfo.Metadata.ClassName));
            result.Add(new XAttribute("params", methodInfo.Metadata.CountParameters));
            return result;
        }

        private XElement GetInfoThread(KeyValuePair<int, ThreadTrace> threadInfo)
        {
            var result = new XElement("thread");
            result.Add(new XAttribute("id", threadInfo.Key));
            result.Add(new XAttribute("time", threadInfo.Value.ExecutionTime));
            return result;
        }


    }
}
