using System.Collections.Generic;
using System.Reflection;

namespace System.ComponentModel.Annotations.Validation.TypeMethodProvider.Caching
{
    /// <inheritdoc />
    /// <summary>
    /// Caching decorator for TypeMethodProvider.
    /// </summary>
    public sealed class CachingTypeMethodProviderDecorator : ITypeMethodProvider
    {
        private readonly ITypeMethodProvider decorated;
        private readonly IObjectCache objectCache;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="decorated"></param>
        /// <param name="objectCache"></param>
        public CachingTypeMethodProviderDecorator(ITypeMethodProvider decorated, IObjectCache objectCache)
        {
            this.decorated = decorated;
            this.objectCache = objectCache;

            RegisterDefaultKeyProvider(objectCache);
        }

        #region Implementation of ITypeMethodProvider

        /// <inheritdoc cref="ITypeMethodProvider.GetMethods"/>
        public IEnumerable<MethodInfo> GetMethods(object source)
        {
            var key = objectCache.GetKey(source);

            var result = objectCache.GetOrAdd(key, () => decorated.GetMethods(source));

            return result as IEnumerable<MethodInfo>;
        }

        #endregion

        private static void RegisterDefaultKeyProvider(IObjectCache objectCache)
        {
            IComparable KeyProvider(object o) => o.GetType().GetHashCode();

            objectCache.RegisterKeyProvider(KeyProvider);
        }
    }
}
