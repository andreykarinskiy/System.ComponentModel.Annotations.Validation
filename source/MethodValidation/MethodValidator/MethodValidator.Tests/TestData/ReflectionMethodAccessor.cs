using System.ComponentModel.Annotations.Validation.MethodProvider.Accessor;
using System.Reflection;

namespace System.ComponentModel.Annotations.Validation.MethodValidator.Tests.TestData
{
    public sealed class ReflectionMethodAccessor : IMethodAccessor
    {
        private readonly MethodInfo methodInfo;

        public ReflectionMethodAccessor(MethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;
        }

        public string InvariantName { get; set; }

        public string MethodName => methodInfo.Name;

        public object Invoke(object target)
        {
            return methodInfo.Invoke(target, Array.Empty<object>());
        }
    }
}
