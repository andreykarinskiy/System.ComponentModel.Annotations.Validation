using System.Reflection;

namespace System.ComponentModel.Annotations.Validation.MethodValidator.Tests.TestData
{
    public static class ObjectExtension
    {
        public static MethodInfo Method(this object source, string methodName)
        {
            return source.GetType().GetMethod(methodName);
        }
    }
}
