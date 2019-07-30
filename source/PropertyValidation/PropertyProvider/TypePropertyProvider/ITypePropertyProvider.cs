using System.Collections.Generic;
using System.Reflection;

namespace System.ComponentModel.Annotations.Validation.TypePropertyProvider
{
    /// <summary>
    /// Provides a collection of class properties.
    /// </summary>
    public interface ITypePropertyProvider
    {
        /// <summary>
        /// Returns properties that belong to an instance of type.
        /// </summary>
        /// <param name="source">Class instance</param>
        /// <returns>Property collection</returns>
        IEnumerable<PropertyInfo> GetProperties(object source);
    }
}
