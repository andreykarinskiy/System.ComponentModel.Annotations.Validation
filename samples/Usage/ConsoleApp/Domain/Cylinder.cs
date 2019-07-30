using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Sуstem.ComponentModel.Annotations.Validation;

namespace ConsoleApp.Domain
{
    public sealed class Cylinder
    {
        public int Diameter { get; set; }

        [Range(0, 75)]
        public int WearOutPercent { get; set; }

        public Cylinder Clone()
        {
            return new Cylinder { Diameter = this.Diameter };
        }

        [Invariant]
        private ValidationResult Invariant()
        {
            return new Success();
        }
    }
}
