using System;
using System.Collections.Generic;

namespace PropertyProvider.Accessor
{
    /// <inheritdoc cref="IPropertyAccessor"/>
    internal sealed class DelegatePropertyAccessor : IPropertyAccessor
    {
        private readonly Func<object, object> getter;

        public DelegatePropertyAccessor(string propertyName, bool isReference, bool isCollection, IEnumerable<Attribute> attributes, Func<object, object> getter)
        {
            Name = propertyName;
            IsReference = isReference;
            IsCollection = isCollection;
            Attributes = attributes;

            this.getter = getter;
        }

        #region Implementation of IPropertyAccessor

        public string Name { get; }

        public bool IsReference { get; }

        public bool IsCollection { get; }

        public IEnumerable<Attribute> Attributes { get; }

        public object GetValue(object source)
        {
            return getter(source);
        }

        #endregion
    }
}
