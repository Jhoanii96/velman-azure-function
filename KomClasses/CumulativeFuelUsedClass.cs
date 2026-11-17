using System;

namespace KomatsuAPI
{
    public class CumulativeFuelUsedClass
    {

        public class Rootobject
        {
            public Link[] Links { get; set; }
            public Fuelused[] FuelUsed { get; set; }
        }

        public class Link
        {
            public string rel { get; set; }
            public string href { get; set; }
        }

        public class Fuelused
        {
            public string FuelUnits { get; set; }
            public int FuelConsumed { get; set; }
            public DateTime datetime { get; set; }
        }

    }
}
