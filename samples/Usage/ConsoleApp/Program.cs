using ConsoleApp.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Sуstem.ComponentModel.Annotations.Validation;

namespace ConsoleApp
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            var engine = new GasolineEngine
            {
                Model = "aQR25DE",
                Description = "Nissan X-Trail",
                Power = 175,
                NumOfSparkPlugs = 4,
                //Cost = 2300 
                Cost = 1000
            };

            var cylinder = new Cylinder
            {
                Diameter = 89,
                WearOutPercent = 0
            };

            engine.Cylinders.Add(cylinder.Clone());
            engine.Cylinders.Add(cylinder.Clone());
            engine.Cylinders.Add(cylinder.Clone());
            engine.Cylinders.Add(cylinder.Clone());

            engine.Cylinders.Last().WearOutPercent = 80;


            IValidator validator = new ObjectValidator();

            IEnumerable<ValidationResult> report = validator.Validate(engine);



            var errors = report.ToArray();

            Console.ReadLine();
        }
    }
}
