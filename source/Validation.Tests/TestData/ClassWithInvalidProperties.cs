using System.ComponentModel.DataAnnotations;

namespace System.ComponentModel.Annotations.Validation.Tests.TestData
{
    public class ClassWithInvalidProperties
    {
        /// <summary>
        /// [Required] RequiredStringProperty
        /// [Required, MinLength(2), MaxLength(10)] ComplexStringProperty
        /// [Range(21, 65)] RangedIntProperty
        /// </summary>
        // ReSharper disable once EmptyConstructor
        public ClassWithInvalidProperties()
        {
            // All properties is not valid.
        }

        /// <summary>
        /// should be not empty!
        /// </summary>
        [Required]
        public string RequiredStringProperty { get; set; } = default;

        /// <summary>
        /// should be in valid range. 
        /// </summary>
        [MinLength(2), MaxLength(10), EmailAddress]
        public string ComplexStringProperty { get; set; } = "X";

        /// <summary>
        /// should be in interval
        /// </summary>
        [Range(21, 65)]
        public int RangedIntProperty { get; set; } = 10;
    }
}
