using System.Collections.Generic;
using System.ComponentModel.Annotations.Validation.TypeMethodProvider;
using System.Reflection;

namespace System.ComponentModel.Annotations.Validation.MethodProvider.Tests.TestData
{
    public static class TypeExtension
    {
        public static object Instantiate(this Type type, params object[] ctorArgs)
        {
            return Activator.CreateInstance(type, ctorArgs);
        }

        public static T Instantiate<T>(this Type type, params object[] ctorArgs)
        {
            return (T)Instantiate(type, ctorArgs);
        }

        public static IEnumerable<MethodInfo> Methods(this Type type)
        {
            return new ReflectionTypeMethodProvider().GetMethods(type.Instantiate());
        }

        public static IEnumerable<MethodInfo> Methods(this object source)
        {
            return new ReflectionTypeMethodProvider().GetMethods(source);
        }
    }
}
