namespace System.ComponentModel.Annotations.Validation.PropertyProvider.Tests.TestData
{
    public static class TypeExtension
    {
        public static object Instantiate(this Type type, params object[] ctorArgs)
        {
            return Activator.CreateInstance(type, ctorArgs);
        }

        public static T Instantiate<T>(this Type type, params object[] ctorArgs)
        {
            return (T)Instantiate(type, ctorArgs);
        }
    }
}
