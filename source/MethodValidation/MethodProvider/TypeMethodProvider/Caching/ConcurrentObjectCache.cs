using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace System.ComponentModel.Annotations.Validation.TypeMethodProvider.Caching
{
    /// <inheritdoc cref="IObjectCache"/>
    public sealed class ConcurrentObjectCache : IObjectCache
    {
        private readonly ConcurrentDictionary<IComparable, Lazy<object>> cacheMap = new ConcurrentDictionary<IComparable, Lazy<object>>();

        private Func<object, IComparable> keyProviderFunc;

        private const string KeyProviderFunctionNotRegistered = "KeyProvider function not registered.";

        #region Implementation of IObjectCache

        /// <inheritdoc cref="IObjectCache.GetKey"/>
        [ExcludeFromCodeCoverage]
        public IComparable GetKey(object item)
        {
            if (keyProviderFunc == null)
            {
                throw new ArgumentException(KeyProviderFunctionNotRegistered);
            }

            return keyProviderFunc(item);
        }

        /// <inheritdoc cref="IObjectCache.RegisterKeyProvider"/>
        public void RegisterKeyProvider(Func<object, IComparable> keyProvider)
        {
            keyProviderFunc = keyProvider;
        }

        /// <inheritdoc cref="IObjectCache.GetOrAdd"/>
        public object GetOrAdd(IComparable key, Func<object> function)
        {
            return cacheMap.GetOrAdd(key, o => new Lazy<object>(function)).Value;
        }

        #endregion
    }
}
