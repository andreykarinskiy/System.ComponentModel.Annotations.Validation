using System.Collections.Generic;
using System.Reflection;
using PropertyProvider;
using PropertyProvider.Accessor;
using PropertyProvider.AccessorFactory;

namespace System.ComponentModel.Annotations.Validation.PropertyValidator.Tests.TestData
{
    public sealed class ReflectionPropertyAccessorFactory : IPropertyAccessorFactory
    {
        public IPropertyAccessor Create(PropertyInfo property)
        {
            return new ReflectionPropertyAccessor(property);
        }

        public IPropertyAccessor Create(object source, string propertyName, bool isReference, IEnumerable<Attribute> attributes)
        {
            var propertyInfo = source.GetType().GetProperty(propertyName);
            var accessor = (ReflectionPropertyAccessor)Create(propertyInfo);

            accessor.IsReference = isReference;
            accessor.Attributes = attributes;

            return accessor;
        }

        private class ReflectionPropertyAccessor : IPropertyAccessor
        {
            private readonly PropertyInfo propertyInfo;

            public ReflectionPropertyAccessor(PropertyInfo propertyInfo)
            {
                this.propertyInfo = propertyInfo;
            }

            public string Name => propertyInfo.Name;

            public bool IsReference { get; set; }

            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public bool IsCollection { get; set; }

            public IEnumerable<Attribute> Attributes { get; set; }

            public object GetValue(object source)
            {
                return propertyInfo.GetValue(source);
            }
        }
    }
}
