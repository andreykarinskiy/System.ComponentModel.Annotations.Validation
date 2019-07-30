using System.Collections.Generic;
using System.ComponentModel.Annotations.Validation.TypePropertyProvider;
using System.Linq;
using PropertyProvider.Accessor;
using PropertyProvider.AccessorFactory;

namespace PropertyProvider
{
    /// <inheritdoc cref="IPropertyProvider"/>
    public sealed class DefaultPropertyProvider : IPropertyProvider
    {
        private readonly ITypePropertyProvider typePropertyProvider;
        private readonly IPropertyAccessorFactory propertyAccessorFactory;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="typePropertyProvider"></param>
        /// <param name="propertyAccessorFactory"></param>
        public DefaultPropertyProvider(ITypePropertyProvider typePropertyProvider, IPropertyAccessorFactory propertyAccessorFactory)
        {
            this.typePropertyProvider = typePropertyProvider;
            this.propertyAccessorFactory = propertyAccessorFactory;
        }

        #region Implementation of IPropertyProvider

        /// <inheritdoc cref="IPropertyProvider.GetProperties"/>
        public IEnumerable<IPropertyAccessor> GetProperties(object source)
        {
            return typePropertyProvider
                .GetProperties(source)
                .Select(property => propertyAccessorFactory.Create(property));
        }

        #endregion
    }
}
