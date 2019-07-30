using System.Reflection;

namespace System.ComponentModel.Annotations.Validation.MethodProvider.Accessor
{
    /// <summary>
    /// Implementation of <see cref="IMethodAccessor"/> interface.
    /// Used for performance test only.
    /// </summary>
    public sealed class ReflectionMethodAccessor : IMethodAccessor
    {
        private readonly MethodInfo methodInfo;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="methodInfo"></param>
        public ReflectionMethodAccessor(MethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;
        }

        /// <inheritdoc cref="IMethodAccessor.InvariantName"/>
        public string InvariantName { get; set; }

        /// <inheritdoc cref="IMethodAccessor.MethodName"/>
        public string MethodName => methodInfo.Name;

        /// <inheritdoc cref="IMethodAccessor.Invoke"/>
        public object Invoke(object target)
        {
            return methodInfo.Invoke(target, Array.Empty<object>());
        }
    }
}
