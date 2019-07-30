using System.ComponentModel.Annotations.Validation.MethodProvider.AccessorFactory;
using System.ComponentModel.Annotations.Validation.TypeMethodProvider;
using System.ComponentModel.Annotations.Validation.TypeMethodProvider.Caching;

namespace System.ComponentModel.Annotations.Validation.MethodProvider
{
    /// <summary>
    /// Internal used.
    /// </summary>
    public static class MethodProviderFactory
    {
        /// <summary>
        /// Create caching method provider.
        /// </summary>
        /// <returns></returns>
        public static IMethodProvider CreateCached()
        {
            ITypeMethodProvider typeMethodProvider = new ReflectionTypeMethodProvider();
            IObjectCache objectCache = new ConcurrentObjectCache();
            ITypeMethodProvider cachingDecorator = new CachingTypeMethodProviderDecorator(typeMethodProvider, objectCache);
            IMethodAccessorFactory accessorFactory = new LambdaMethodAccessorFactory();
            return new DefaultMethodProvider(cachingDecorator, accessorFactory);
        }
    }
}
