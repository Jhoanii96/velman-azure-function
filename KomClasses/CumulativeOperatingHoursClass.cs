using System;

namespace KomatsuAPI
{
    public class CumulativeOperatingHoursClass
    {

        public class Rootobject
        {
            public Link[] Links { get; set; }
            public Cumulativeoperatinghour[] CumulativeOperatingHours { get; set; }
        }

        public class Link
        {
            public string rel { get; set; }
            public string href { get; set; }
        }

        public class Cumulativeoperatinghour
        {
            public float Hour { get; set; }
            public DateTime datetime { get; set; }
        }

    }
}
