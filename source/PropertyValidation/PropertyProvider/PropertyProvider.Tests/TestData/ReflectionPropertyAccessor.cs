using System.Collections.Generic;
using System.Reflection;
using PropertyProvider.Accessor;

// ReSharper disable UnassignedGetOnlyAutoProperty

namespace System.ComponentModel.Annotations.Validation.PropertyProvider.Tests.TestData
{
    public sealed class ReflectionPropertyAccessor : IPropertyAccessor
    {
        private readonly PropertyInfo propertyInfo;

        public ReflectionPropertyAccessor(PropertyInfo propertyInfo)
        {
            this.propertyInfo = propertyInfo;
        }

        #region Implementation of IPropertyAccessor

        public string Name => propertyInfo.Name;

        public bool IsReference { get; }

        public bool IsCollection { get; }

        public IEnumerable<Attribute> Attributes { get; }

        public object GetValue(object source)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
