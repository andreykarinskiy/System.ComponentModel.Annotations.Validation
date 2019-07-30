using System;
using System.ComponentModel.Annotations.Validation.TypePropertyProvider.Caching;
using System.Reflection;
using PropertyProvider.Accessor;
using PropertyProvider.AccessorFactory;

namespace PropertyProvider.Caching
{
    /// <summary>
    /// Caching decorator for <see cref="IPropertyAccessorFactory"/> interface.
    /// Uses <see cref="IObjectCache"/> for retrieve existing property accessors.
    /// </summary>
    public sealed class CachedPropertyAccessorFactoryDecorator : IPropertyAccessorFactory
    {
        private readonly IPropertyAccessorFactory decorated;
        private readonly IObjectCache objectCache;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="decorated"></param>
        /// <param name="objectCache"></param>
        public CachedPropertyAccessorFactoryDecorator(IPropertyAccessorFactory decorated, IObjectCache objectCache)
        {
            this.decorated = decorated;
            this.objectCache = objectCache;

            RegisterDefaultKeyProvider(objectCache);
        }

        #region Implementation of IPropertyAccessorFactory

        /// <inheritdoc cref="IPropertyAccessorFactory.Create"/>
        public IPropertyAccessor Create(PropertyInfo property)
        {
            var key = objectCache.GetKey(property);

            var accessor = objectCache.GetOrAdd(key, () => decorated.Create(property));

            return accessor as IPropertyAccessor;
        }

        #endregion

        private static void RegisterDefaultKeyProvider(IObjectCache objectCache)
        {
            IComparable KeyProvider(object o) => o.GetHashCode();

            objectCache.RegisterKeyProvider(KeyProvider);
        }
    }
}
