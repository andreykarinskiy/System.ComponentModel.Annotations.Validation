using System.Reflection;

namespace PropertyProvider.Checkers
{
    /// <summary>
    /// Checks whether a property type is collection.
    /// </summary>
    public interface IObjectCollectionChecker
    {
        /// <summary>
        /// Returns TRUE if the property type is collection, otherwise FALSE.
        /// </summary>
        /// <param name="propertyInfo">property</param>
        /// <returns>flag</returns>
        bool IsCollection(PropertyInfo propertyInfo);
    }
}
