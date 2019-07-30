using System.Collections.Generic;
using System.Reflection;

namespace System.ComponentModel.Annotations.Validation.TypePropertyProvider.Caching
{
    /// <inheritdoc />
    /// <summary>
    /// Caching decorator for TypePropertyProvider.
    /// </summary>
    public sealed class CachingTypePropertyProviderDecorator : ITypePropertyProvider
    {
        private readonly ITypePropertyProvider decorated;
        private readonly IObjectCache objectCache;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="decorated"></param>
        /// <param name="objectCache"></param>
        public CachingTypePropertyProviderDecorator(ITypePropertyProvider decorated, IObjectCache objectCache)
        {
            this.decorated = decorated;
            this.objectCache = objectCache;

            RegisterDefaultKeyProvider(objectCache);
        }

        #region Implementation of ITypePropertyProvider

        /// <inheritdoc cref="ITypePropertyProvider.GetProperties"/>
        /// <remarks>
        /// Returns the properties of the type from the cache, if they are there,
        /// otherwise delegates the call to the decorated provider,
        /// then writes to the cache and returns the result.
        /// </remarks>
        public IEnumerable<PropertyInfo> GetProperties(object source)
        {
            var key = objectCache.GetKey(source);

            var result = objectCache.GetOrAdd(key, () => decorated.GetProperties(source));

            return result as IEnumerable<PropertyInfo>;
        }

        #endregion

        private static void RegisterDefaultKeyProvider(IObjectCache objectCache)
        {
            IComparable KeyProvider(object o) => o.GetType().GetHashCode();

            objectCache.RegisterKeyProvider(KeyProvider);
        }
    }
}
