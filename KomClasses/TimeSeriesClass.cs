using System;

namespace KomatsuAPI
{
    public class TimeSeriesClass
    {

        public class Rootobject
        {
            public Link[] Links { get; set; }
            public Equipment[] Equipment { get; set; }
            public int version { get; set; }
            public DateTime snapshotTime { get; set; }
        }

        public class Link
        {
            public string rel { get; set; }
            public string href { get; set; }
        }

        public class Equipment
        {
            public Equipmentheader EquipmentHeader { get; set; }
            public Link1[] Links { get; set; }
        }

        public class Equipmentheader
        {
            public string OEMName { get; set; }
            public string Model { get; set; }
            public string EquipmentID { get; set; }
            public string SerialNumber { get; set; }
        }

        public class Link1
        {
            public string rel { get; set; }
            public string href { get; set; }
        }

    }
}
