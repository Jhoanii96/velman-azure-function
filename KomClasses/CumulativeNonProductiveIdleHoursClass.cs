using System;

namespace KomatsuAPI
{
    public class CumulativeNonProductiveIdleHoursClass
    {

        public class Rootobject
        {
            public Link[] Links { get; set; }
            public Cumulativenonproductiveidlehour[] CumulativeNonproductiveIdleHours { get; set; }
        }

        public class Link
        {
            public string rel { get; set; }
            public string href { get; set; }
        }

        public class Cumulativenonproductiveidlehour
        {
            public float Hour { get; set; }
            public DateTime datetime { get; set; }
        }

    }
}
