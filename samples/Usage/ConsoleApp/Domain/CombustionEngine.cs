using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Sуstem.ComponentModel.Annotations.Validation;

namespace ConsoleApp.Domain
{
    public abstract class CombustionEngine : Engine
    {
        public int NumOfCylinders => Cylinders.Count();

        public ICollection<Cylinder> Cylinders { get; } = new HashSet<Cylinder>();


        [Invariant("The cost depends on the number of cylinders")]
        private IEnumerable<ValidationResult> CostDependsOnNumberOfCylinders()
        {
            if (NumOfCylinders == 4 && Cost < 1500)
            {
                yield return new Fail("4-cylinder engine cannot cost less than 1500!");
            }

            if (NumOfCylinders > 12)
            {
                yield return new Fail("We do not serve tanks!");
            }
        }
    }
}
