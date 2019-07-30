using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.ComponentModel.Annotations.Validation.TypePropertyProvider
{
    /// <summary>
    /// Implementation of <see cref="ITypePropertyProvider"/> interface.
    /// Uses reflection to retrieve type properties.
    /// </summary>
    public sealed class ReflectionTypePropertyProvider : ITypePropertyProvider
    {
        private const BindingFlags PropertyVisibility = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

        private static readonly Func<PropertyInfo, bool> PropertyFilter = p => p.CanRead && p.GetMethod.IsPublic;

        #region Implementation of ITypePropertyProvider

        /// <inheritdoc cref="ITypePropertyProvider.GetProperties"/>
        public IEnumerable<PropertyInfo> GetProperties(object source)
        {
            var type = source.GetType();

            return type.GetProperties(PropertyVisibility).Where(PropertyFilter);
        }

        #endregion
    }
}
