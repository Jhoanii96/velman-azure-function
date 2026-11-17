using System;

namespace KomatsuAPI
{
    public class FuelUsedInThePreceding24HoursClass
    {

        public class Rootobject
        {
            public Link[] Links { get; set; }
            public Fuelusedlast24[] FuelUsedLast24 { get; set; }
        }

        public class Link
        {
            public string rel { get; set; }
            public string href { get; set; }
        }

        public class Fuelusedlast24
        {
            public string FuelUnits { get; set; }
            public int FuelConsumed { get; set; }
            public DateTime datetime { get; set; }
        }

    }
}
