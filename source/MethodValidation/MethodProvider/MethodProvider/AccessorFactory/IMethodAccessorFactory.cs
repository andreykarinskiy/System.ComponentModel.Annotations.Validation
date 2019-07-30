using System.ComponentModel.Annotations.Validation.MethodProvider.Accessor;
using System.Reflection;

namespace System.ComponentModel.Annotations.Validation.MethodProvider.AccessorFactory
{
    /// <summary>
    /// Factory to create accessors.
    /// Hides the specifics of their creation.
    /// </summary>
    public interface IMethodAccessorFactory
    {
        /// <summary>
        /// Creates an accessor for a class method.
        /// </summary>
        /// <param name="methodInfo">method</param>
        /// <returns>accessor</returns>
        IMethodAccessor Create(MethodInfo methodInfo);
    }
}
