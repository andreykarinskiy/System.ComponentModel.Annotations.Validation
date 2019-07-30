using System;
using System.Collections.Generic;
using System.Reflection;

namespace PropertyProvider.Checkers
{
    /// <summary>
    /// Implementation of <see cref="IObjectReferenceChecker"/> interface.
    /// Uses reflection to check properties.
    /// </summary>
    public sealed class ReflectionReferenceChecker : IObjectReferenceChecker
    {
        #region Implementation of IObjectReferenceChecker

        /// <inheritdoc cref="IObjectReferenceChecker.IsReference"/>
        public bool IsReference(PropertyInfo propertyInfo)
        {
            var itemType = GetElementType(propertyInfo.PropertyType);

            if (itemType != null && !IsSystemType(itemType))
            {
                return true;
            }

            return !IsSystemType(propertyInfo.PropertyType);
        }

        #endregion

        private static bool IsSystemType(Type type) => type.Assembly == typeof(object).Assembly;

        private static Type GetElementType(Type seqType)
        {
            var collection = GetCollectionIfPresent(seqType);

            return collection == null ? 
                seqType : 
                collection.GetGenericArguments()[0];
        }

        private static Type GetCollectionIfPresent(Type seqType)
        {
            if (seqType == null || seqType == typeof(string))
            {
                return null;
            }

            if (seqType.IsArray)
            {
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());
            }

            if (seqType.IsGenericType)
            {
                foreach (var arg in seqType.GetGenericArguments())
                {
                    var collection = typeof(IEnumerable<>).MakeGenericType(arg);

                    if (collection.IsAssignableFrom(seqType))
                    {
                        return collection;
                    }
                }
            }

            var interfaces = seqType.GetInterfaces();

            if (interfaces.Length > 0)
            {
                foreach (var ifc in interfaces)
                {
                    var collection = GetCollectionIfPresent(ifc);

                    if (collection != null)
                    {
                        return collection;
                    }
                }
            }

            if (seqType.BaseType != null && seqType.BaseType != typeof(object))
            {
                return GetCollectionIfPresent(seqType.BaseType);
            }

            return null;
        }
    }
}
