using System.ComponentModel.DataAnnotations;

namespace System.ComponentModel.Annotations.Validation.Tests.TestData
{
    public class ClassWithInvalidPropertiesAndReferences
    {
        [Required]
        public string StringProperty { get; set; } = default;

        public ClassWithInvalidProperties ReferenceProperty { get; set; } = new ClassWithInvalidProperties();

        public ClassWithInvariants[] CollectionProperty { get; set; } = { new ClassWithInvariants() };
    }
}