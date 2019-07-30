using System.ComponentModel.Annotations.Validation.TypePropertyProvider;
using System.ComponentModel.Annotations.Validation.TypePropertyProvider.Caching;
using System.Diagnostics.CodeAnalysis;
using PropertyProvider.AccessorFactory;
using PropertyProvider.Caching;
using PropertyProvider.Checkers;
using PropertyProvider.MemberAttributes;

namespace PropertyProvider
{
    /// <summary>
    /// Entry point to create a property provider.
    /// </summary>
    public static class PropertyProviderFactory
    {
        /// <summary>
        /// Creates an instance of the property provider without caching.
        /// </summary>
        /// <returns>property provider</returns>
        [ExcludeFromCodeCoverage]
        public static IPropertyProvider CreateNonCached()
        {
            var propertyProvider = CreateTypePropertyProvider();
            var accessorFactory = CreatePropertyAccessorFactory();

            return new DefaultPropertyProvider(propertyProvider, accessorFactory);
        }

        /// <summary>
        /// Creates an instance of the property provider with caching.
        /// </summary>
        /// <returns>property provider</returns>
        public static IPropertyProvider CreateCached()
        {
            var propertyProvider = CreateTypePropertyProvider();
            var cachedPropertyProvider = CreateCachedPropertyProvider(propertyProvider);

            var accessorFactory = CreatePropertyAccessorFactory();
            var cachedAccessorFactory = CreateCachedPropertyAccessorFactory(accessorFactory);

            return new DefaultPropertyProvider(cachedPropertyProvider, cachedAccessorFactory);
        }

        private static ITypePropertyProvider CreateTypePropertyProvider()
        {
            return new ReflectionTypePropertyProvider();
        }

        private static ITypePropertyProvider CreateCachedPropertyProvider(ITypePropertyProvider propertyProvider)
        {
            IObjectCache propertyCache = new ConcurrentObjectCache();

            return new CachingTypePropertyProviderDecorator(propertyProvider, propertyCache);
        }

        private static IPropertyAccessorFactory CreatePropertyAccessorFactory()
        {
            IObjectReferenceChecker referenceChecker = new ReflectionReferenceChecker();
            IObjectCollectionChecker collectionChecker = new ReflectionCollectionChecker();
            IMemberAttributeProvider attributeProvider = new ReflectionMemberAttributeProvider();

            return new LambdaPropertyAccessorFactory(referenceChecker, collectionChecker, attributeProvider);
        }

        private static IPropertyAccessorFactory CreateCachedPropertyAccessorFactory(IPropertyAccessorFactory accessorFactory)
        {
            IObjectCache accessorCache = new ConcurrentObjectCache();

            return new CachedPropertyAccessorFactoryDecorator(accessorFactory, accessorCache);
        }
    }
}
