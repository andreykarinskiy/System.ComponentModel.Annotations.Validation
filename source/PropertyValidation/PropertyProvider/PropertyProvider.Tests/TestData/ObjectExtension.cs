using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.ComponentModel.Annotations.Validation.PropertyProvider.Tests.TestData
{
    public static class ObjectExtension
    {
        public static PropertyInfo Property<T>(this T source, string propertyName)
        {
            const BindingFlags filter = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            var allProperties = source.GetType().GetProperties(filter);
            return allProperties.Single(p => p.Name.Equals(propertyName));
        }

        public static PropertyInfo Property<T>(this T source, Expression<Func<T, object>> o)
        {
            const BindingFlags filter = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            var allProperties = source.GetType().GetProperties(filter);
            return allProperties.Single(p => p == GetPropertyInfo(source, o));
        }

        public static PropertyInfo GetPropertyInfo<T>(T source, Expression<Func<T, object>> propertyLambda)
        {
            var type = typeof(T);

            if (!(propertyLambda.Body is MemberExpression member))
            {
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a method, not a property.");
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");
            }

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            {
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a property that is not from type {type}.");
            }

            return propInfo;
        }
    }
}
