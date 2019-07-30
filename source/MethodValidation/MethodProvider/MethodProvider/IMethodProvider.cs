using System.Collections.Generic;
using System.ComponentModel.Annotations.Validation.MethodProvider.Accessor;

namespace System.ComponentModel.Annotations.Validation.MethodProvider
{
    /// <summary>
    /// Provides a collection of class method accessors.
    /// Replacing reflection for high speed access to methods.
    /// </summary>
    public interface IMethodProvider
    {
        /// <summary>
        /// Returns a collection of method accessors.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<IMethodAccessor> GetMethods(object source);
    }
}
