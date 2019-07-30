using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Domain
{
    public sealed class GasolineEngine : CombustionEngine
    {
        public int NumOfSparkPlugs { get; set; }
    }
}
