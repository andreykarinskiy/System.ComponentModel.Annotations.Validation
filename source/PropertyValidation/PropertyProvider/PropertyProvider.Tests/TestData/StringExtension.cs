using System.Collections.Generic;

namespace System.ComponentModel.Annotations.Validation.PropertyProvider.Tests.TestData
{
    public static class StringExtension
    {
        public static string ToSeparatedString(this IEnumerable<object> source, string separator = ", ")
        {
            return string.Join(separator, separator);
        }
    }
}
