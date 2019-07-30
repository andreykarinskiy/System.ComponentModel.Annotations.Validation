using System;
using System.Collections.Generic;

namespace PropertyProvider.Accessor
{
    /// <summary>
    /// Represents a class property,
    /// allows you to get its value quickly.
    /// </summary>
    public interface IPropertyAccessor
    {
        /// <summary>
        /// Gets property name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets TRUE when the property refers to a custom type,
        /// and FALSE if it is an build-in type.
        /// </summary>
        bool IsReference { get; }

        /// <summary>
        /// Get TRUE when the property is collection.
        /// </summary>
        bool IsCollection { get; }

        /// <summary>
        /// Gets a collection of property attributes
        /// </summary>
        IEnumerable<Attribute> Attributes { get; }

        /// <summary>
        /// Returns the value of its property from an object.
        /// </summary>
        /// <param name="source">object</param>
        /// <returns>property value</returns>
        object GetValue(object source);
    }
}
