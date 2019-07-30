using System.Collections.Generic;

namespace System.ComponentModel.Annotations.Validation.MethodValidator.Dispatcher
{
    /// <summary>
    /// Collection extension.
    /// </summary>
    public static class CollectionExtension
    {
        /// <summary>
        /// Add collection to another collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        public static void AddRange<T>(this ICollection<T> destination, IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                destination.Add(item);
            }
        }
    }
}
