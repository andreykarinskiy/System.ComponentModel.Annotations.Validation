using System.ComponentModel.DataAnnotations;

namespace System.ComponentModel.Annotations.Validation.Tests.TestData
{
    public class ClassWithBackReference
    {
        [Required]
        public string StringProperty { get; set; }

        public ClassWithCircularReferences ReferenceProperty { get; set; }
    }
}
