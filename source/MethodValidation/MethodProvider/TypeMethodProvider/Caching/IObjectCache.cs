namespace System.ComponentModel.Annotations.Validation.TypeMethodProvider.Caching
{
    /// <summary>
    /// Object cache.
    /// </summary>
    public interface IObjectCache
    {
        /// <summary>
        /// Get a unique key to identify the cache item.
        /// The key is calculated using a previously registered provider.
        /// </summary>
        /// <param name="item">item</param>
        /// <returns>key</returns>
        IComparable GetKey(object item);

        /// <summary>
        /// Register a provider to get unique keys per instance.
        /// </summary>
        /// <param name="keyProvider">provider delegate</param>
        void RegisterKeyProvider(Func<object, IComparable> keyProvider);

        /// <summary>
        /// Returns value from cache by key.
        /// If the key does not exist, it writes
        /// the result of the function to the cache.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="function"></param>
        /// <returns>result of function</returns>
        object GetOrAdd(IComparable key, Func<object> function);
    }
}
