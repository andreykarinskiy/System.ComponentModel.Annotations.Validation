using System.Collections.Generic;
using System.ComponentModel.Annotations.Validation.MethodProvider.Accessor;
using System.ComponentModel.Annotations.Validation.MethodProvider.AccessorFactory;
using System.ComponentModel.Annotations.Validation.TypeMethodProvider;
using System.Linq;
using System.Reflection;

namespace System.ComponentModel.Annotations.Validation.MethodProvider
{
    /// <inheritdoc cref="IMethodProvider"/>
    public sealed class DefaultMethodProvider : IMethodProvider
    {
        private readonly ITypeMethodProvider typeMethodProvider;
        private readonly IMethodAccessorFactory methodAccessorFactory;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="typeMethodProvider"></param>
        /// <param name="methodAccessorFactory"></param>
        public DefaultMethodProvider(ITypeMethodProvider typeMethodProvider, IMethodAccessorFactory methodAccessorFactory)
        {
            this.typeMethodProvider = typeMethodProvider;
            this.methodAccessorFactory = methodAccessorFactory;
        }

        #region Implementation of IPropertyProvider

        /// <inheritdoc cref="IMethodProvider.GetMethods"/>
        public IEnumerable<IMethodAccessor> GetMethods(object source)
        {
            return typeMethodProvider
                .GetMethods(source)
                .Where(InvariantMethodFilter)
                .Select(methodAccessorFactory.Create);
        }

        #endregion

        private static bool InvariantMethodFilter(MethodInfo method)
        {
            if (!method.GetCustomAttributes().Any())
            {
                return false;
            }

            var exists = method.GetCustomAttributes().Select(attr => attr.GetType().Name);

            return exists.Contains("InvariantAttribute");
        }
    }
}
