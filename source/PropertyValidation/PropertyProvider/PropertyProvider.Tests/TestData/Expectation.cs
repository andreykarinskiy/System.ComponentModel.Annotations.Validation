using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.ComponentModel.Annotations.Validation.PropertyProvider.Tests.TestData
{
    public static class Expectation
    {
        public static IEnumerable<PropertyInfo> OfProperties<T>(T instance, params Expression<Func<T, object>>[] o)
        {
            const BindingFlags filter = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            return from property in instance.GetType().GetProperties(filter)
                let propertyNames = instance.MemberNames(o)
                where propertyNames.Contains(property.Name)
                select property;
        }
    }
}
