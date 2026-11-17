using System;

namespace KomatsuAPI
{
    public class LocationsClass
    {

        public class Rootobject
        {
            public Link[] Links { get; set; }
            public Location[] Location { get; set; }
        }

        public class Link
        {
            public string rel { get; set; }
            public string href { get; set; }
        }

        public class Location
        {
            public float Latitude { get; set; }
            public float Longitude { get; set; }
            public DateTime datetime { get; set; }
        }

    }
}
