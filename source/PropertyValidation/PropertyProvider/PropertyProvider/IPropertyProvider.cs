using System.Collections.Generic;
using PropertyProvider.Accessor;

namespace PropertyProvider
{
    /// <summary>
    /// Provides a collection of class property accessors.
    /// Replacing reflection for high speed access to properties.
    /// </summary>
    public interface IPropertyProvider
    {
        /// <summary>
        /// Returns a collection of property accessors.
        /// </summary>
        /// <param name="source">object</param>
        /// <returns>accessors</returns>
        IEnumerable<IPropertyAccessor> GetProperties(object source);
    }
}
