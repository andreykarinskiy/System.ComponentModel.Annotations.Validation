using System.Reflection;

namespace PropertyProvider.Checkers
{
    /// <summary>
    /// Checks whether a property type is custom or built-in.
    /// </summary>
    public interface IObjectReferenceChecker
    {
        /// <summary>
        /// Returns TRUE if the property type is custom,
        /// and FALSE if it is built-in.
        /// </summary>
        /// <param name="propertyInfo">property</param>
        /// <returns>flag</returns>
        bool IsReference(PropertyInfo propertyInfo);
    }
}
