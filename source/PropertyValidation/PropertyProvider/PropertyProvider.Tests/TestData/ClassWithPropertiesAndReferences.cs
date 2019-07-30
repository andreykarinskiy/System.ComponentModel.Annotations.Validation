using System.Collections.Generic;

namespace System.ComponentModel.Annotations.Validation.PropertyProvider.Tests.TestData
{
    public class ClassWithPropertiesAndReferences
    {
        public string StringProperty { get; set; }

        public ClassWithPropertiesOnly ReferenceProperty { get; set; }

        public IEnumerable<ClassWithPropertiesOnly> CustomTypesCollection { get; set; }
    }
}
