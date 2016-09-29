using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Classes.TracerInfo
{
    class MethodInfo
    {
        public string Name { get; }
        public string ClassName { get; }
        public int CountParameters { get; }

        internal MethodInfo(MethodBase methodBase)
        {
            Name = methodBase.Name;
            ClassName = methodBase.DeclaringType?.ToString();
            CountParameters = methodBase.GetParameters().Length;
        }
    }
}
