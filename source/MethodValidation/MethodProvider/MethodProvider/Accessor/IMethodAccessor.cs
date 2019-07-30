namespace System.ComponentModel.Annotations.Validation.MethodProvider.Accessor
{
    /// <summary>
    /// Represents a class method,
    /// allows you to invoke it quickly.
    /// </summary>
    public interface IMethodAccessor
    {
        /// <summary>
        /// Gets invariant name.
        /// </summary>
        string InvariantName { get; }

        /// <summary>
        /// Gets method name.
        /// </summary>
        string MethodName { get; }

        /// <summary>
        /// Invoke invariant method.
        /// </summary>
        /// <param name="target">object instance</param>
        /// <returns>method result</returns>
        object Invoke(object target);
    }
}
