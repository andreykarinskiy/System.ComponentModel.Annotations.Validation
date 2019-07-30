using System.Collections;
using System.Reflection;

namespace PropertyProvider.Checkers
{
    /// <inheritdoc cref="IObjectCollectionChecker"/>
    public sealed class ReflectionCollectionChecker : IObjectCollectionChecker
    {
        /// <inheritdoc cref="IObjectCollectionChecker.IsCollection"/>
        public bool IsCollection(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType
                .GetInterface(nameof(IEnumerable)) != null;
        }
    }
}
