using System;
using System.Collections.Generic;
using System.Threading;
using Tracer.Classes.Formatters;

namespace Test
{
    class Program
    {
        private static readonly Tracer.Classes.Tracer tracer = new Tracer.Classes.Tracer();
        static void Main(string[] args)
        {
            tracer.StartTrace();

            TestMethod1(10);
            TestMethod2(10);

            var threads = new List<Thread>();
            for (var i = 0; i < 1; i++)
            {
                var thread = i % 3 == 0 ? new Thread(TestMethod1) : new Thread(TestMethod2);
                threads.Add(thread);
                thread.Start(10);
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            tracer.StopTrace();

            PrintInfo();
            Console.ReadKey();
        }

        private static void TestMethod1(object value)
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        private static void TestMethod2(object value)
        {
            tracer.StartTrace();

            var test = 0;

            Thread.Sleep(100);
            TestMethod1(0);

            for (var i = 0; i < (int)value; i++)
            {
                test += i;
            }

            tracer.StopTrace();
        }

        private static void PrintInfo()
        {
            var xmlFormatter = new XmlFormatter("Result.xml");
            var consoleFormatter = new ConsoleFormatter();

            consoleFormatter.Format(tracer.GetTraceResult());
            xmlFormatter.Format(tracer.GetTraceResult());

        }
    }
}
