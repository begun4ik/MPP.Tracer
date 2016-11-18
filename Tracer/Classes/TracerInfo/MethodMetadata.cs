using System.Reflection;

namespace Tracer.Classes.TracerInfo
{
    internal sealed class MethodMetadata
    {
        public string Name { get; }
        public string ClassName { get; }
        public int CountParameters { get; }

        internal MethodMetadata(MethodBase methodBase)
        {
            Name = methodBase.Name;
            ClassName = methodBase.DeclaringType?.ToString();
            CountParameters = methodBase.GetParameters().Length;
        }
    }
}
