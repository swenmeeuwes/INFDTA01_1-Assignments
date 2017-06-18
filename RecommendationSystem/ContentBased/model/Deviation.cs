using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentBased.model
{
    class Deviation
    {
        public int Readings { get; set; } // Amount of ratings
        public double Value { get; set; }

        public double Average {
            get {
                return Value / Readings;
            }
        }

        public Deviation(int readings, double value)
        {
            Readings = readings;
            Value = value;
        }
    }
}
