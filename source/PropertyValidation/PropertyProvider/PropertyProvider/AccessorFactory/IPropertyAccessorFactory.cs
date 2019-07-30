using System.Reflection;
using PropertyProvider.Accessor;

namespace PropertyProvider.AccessorFactory
{
    /// <summary>
    /// Factory to create accessors.
    /// Hides the specifics of their creation.
    /// </summary>
    public interface IPropertyAccessorFactory
    {
        /// <summary>
        /// Creates an accessor for a class property.
        /// </summary>
        /// <param name="property">property</param>
        /// <returns>accessor</returns>
        IPropertyAccessor Create(PropertyInfo property);
    }
}
