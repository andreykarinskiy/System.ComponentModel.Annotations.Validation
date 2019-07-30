using System.Collections.Generic;
using System.Reflection;

namespace System.ComponentModel.Annotations.Validation.TypeMethodProvider
{
    /// <summary>
    /// Provides a collection of class methods.
    /// </summary>
    public interface ITypeMethodProvider
    {
        /// <summary>
        /// Returns methods that belong to an instance of type.
        /// </summary>
        /// <param name="source">Class instance</param>
        /// <returns>Method collection</returns>
        IEnumerable<MethodInfo> GetMethods(object source);
    }
}
