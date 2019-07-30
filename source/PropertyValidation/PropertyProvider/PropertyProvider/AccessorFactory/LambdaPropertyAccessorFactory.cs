using System;
using System.Linq.Expressions;
using System.Reflection;
using PropertyProvider.Accessor;
using PropertyProvider.Checkers;
using PropertyProvider.MemberAttributes;

namespace PropertyProvider.AccessorFactory
{
    /// <summary>
    /// Implementation of <see cref="IPropertyAccessorFactory"/> interface.
    /// Creates fast property accessor instances from PropertyInfo objects using System.Linq.Expressions.
    /// </summary>
    public sealed class LambdaPropertyAccessorFactory : IPropertyAccessorFactory
    {
        private readonly IObjectReferenceChecker referenceChecker;
        private readonly IObjectCollectionChecker collectionChecker;
        private readonly IMemberAttributeProvider attributeProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="referenceChecker"></param>
        /// <param name="collectionChecker"></param>
        /// <param name="attributeProvider"></param>
        public LambdaPropertyAccessorFactory(IObjectReferenceChecker referenceChecker, IObjectCollectionChecker collectionChecker, IMemberAttributeProvider attributeProvider)
        {
            this.referenceChecker = referenceChecker;
            this.collectionChecker = collectionChecker;
            this.attributeProvider = attributeProvider;
        }

        #region Implementation of IPropertyAccessorFactory

        /// <inheritdoc cref="IPropertyAccessorFactory.Create"/>
        public IPropertyAccessor Create(PropertyInfo property)
        {
            var propertyName = property.Name;

            var isReference = referenceChecker.IsReference(property);

            var isCollection = collectionChecker.IsCollection(property);

            var propertyAttributes = attributeProvider.GetAttributes(property);

            var getter = CreateGetAccessor(property);

            return new DelegatePropertyAccessor(propertyName, isReference, isCollection, propertyAttributes, getter);
        }

        #endregion

        private static Func<object, object> CreateGetAccessor(PropertyInfo property)
        {
            var getMethod = property.GetGetMethod();

            var instance = Expression.Parameter(typeof(object), "instance");

            if (property.DeclaringType == null)
            {
                throw new ArgumentException(property.Name);
            }

            var instanceCast = property.DeclaringType != null && !property.DeclaringType.IsValueType ? 
                Expression.TypeAs(instance, property.DeclaringType) : 
                Expression.Convert(instance, property.DeclaringType);

            var exprBody = Expression.TypeAs(Expression.Call(instanceCast, getMethod), typeof(object));

            return Expression.Lambda<Func<object, object>>(exprBody, instance).Compile();

        }
    }
}
