using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Sуstem.ComponentModel.Annotations.Validation;

namespace ConsoleApp.Domain
{
    public abstract class Engine
    {
        [Required]
        [StringLength(15)]
        public string Model { get; set; }

        public string Description { get; set; }

        [Range(1000, 4500)]
        public decimal Cost { get; set; }

        [Range(12, 500)]
        public int Power { get; set; }

        [Invariant("Name must be started with capitalize symbol")]
        private ValidationResult NameMustBeStartedWithCapitalize()
        {
            var firstSymbol = Model.First();
            if (!char.IsUpper(firstSymbol))
            {
                return new Fail($"'{firstSymbol}' is not uppercase.");
            }

            return new Success();
        }
    }
}
