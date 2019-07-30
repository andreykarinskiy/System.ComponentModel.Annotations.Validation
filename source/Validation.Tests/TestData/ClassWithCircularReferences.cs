using System.ComponentModel.DataAnnotations;

namespace System.ComponentModel.Annotations.Validation.Tests.TestData
{
    public class ClassWithCircularReferences
    {
        [Required]
        public string StringProperty { get; set; } = default;

        public ClassWithBackReference ReferenceProperty { get; set; }
    }
}
