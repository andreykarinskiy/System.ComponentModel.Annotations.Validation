using System.Reflection;

namespace System.ComponentModel.Annotations.Validation.MethodProvider.Tests.TestData
{
    public static class ObjectExtension
    {
        public static MethodInfo Method(this object source, string methodName)
        {
            return source.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        }

        public static MethodInfo Method(this Type source, string methodName)
        {
            return source.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        }
    }
}
