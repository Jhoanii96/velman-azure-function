using System;

namespace KomatsuAPI
{
    public class FuelRemainingRatioClass
    {

        public class Rootobject
        {
            public Link[] Links { get; set; }
            public Fuelremaining[] FuelRemaining { get; set; }
        }

        public class Link
        {
            public string rel { get; set; }
            public string href { get; set; }
        }

        public class Fuelremaining
        {
            public float Percent { get; set; }
            public DateTime datetime { get; set; }
        }

    }
}
