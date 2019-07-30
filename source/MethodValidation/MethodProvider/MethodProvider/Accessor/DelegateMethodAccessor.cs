namespace System.ComponentModel.Annotations.Validation.MethodProvider.Accessor
{
    internal sealed class DelegateMethodAccessor : IMethodAccessor
    {
        private readonly Func<object, object> accessor;

        public DelegateMethodAccessor(string methodName, string invariantName, Func<object, object> accessor)
        {
            MethodName = methodName;
            InvariantName = invariantName;
            this.accessor = accessor;
        }

        public string InvariantName { get; }

        public string MethodName { get; }

        public object Invoke(object target)
        {
            return accessor(target);
        }
    }
}
