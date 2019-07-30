using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.ComponentModel.Annotations.Validation.TypeMethodProvider.Tests.TestData
{
    public static class Expectation
    {
        public static IEnumerable<MethodInfo> OfMethods<T>(T instance, params string[] o)
        {
            const BindingFlags filter = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            return 
                from method in instance.GetType().GetMethods(filter)
                where o.Contains(method.Name)
                select method;
        }
    }
}
