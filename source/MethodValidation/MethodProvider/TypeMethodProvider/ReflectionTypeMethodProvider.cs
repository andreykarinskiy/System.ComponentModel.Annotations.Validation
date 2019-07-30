using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.ComponentModel.Annotations.Validation.TypeMethodProvider
{
    /// <summary>
    /// Implementation of <see cref="ITypeMethodProvider"/> interface.
    /// Uses reflection to retrieve type methods.
    /// </summary>
    public sealed class ReflectionTypeMethodProvider : ITypeMethodProvider
    {
        private const BindingFlags MethodVisibility =

            // static or non static method
            BindingFlags.Instance | BindingFlags.Static |

            // only non-public methods
            BindingFlags.NonPublic;

        private static readonly IEnumerable<string> Exclusions = new[] 
        {
            "MemberwiseClone",
            "Finalize",
            "FieldGetter",
            "FieldSetter",
            "GetFieldInfo"
        };

        /// <inheritdoc cref="ITypeMethodProvider.GetMethods"/>
        public IEnumerable<MethodInfo> GetMethods(object source)
        {
            var type = source.GetType();

            return GetMethods(type);
        }

        private static IEnumerable<MethodInfo> GetMethods(Type type)
        {
            var uniqMethods = new Dictionary<string, MethodInfo>();

            GetMethods(type, uniqMethods);

            return uniqMethods.Values;
        }

        private static void GetMethods(Type type, IDictionary<string, MethodInfo> uniqMethods)
        {
            var allMethods = type
                .GetMethods(MethodVisibility)
                .Where(m => !Exclusions.Contains(m.Name));

            foreach (var method in allMethods)
            {
                if (!uniqMethods.ContainsKey(method.Name))
                {
                    uniqMethods.Add(method.Name, method);
                }
            }

            var baseType = type.BaseType;

            if (baseType != null)
            {
                GetMethods(baseType, uniqMethods);
            }
        }
    }
}