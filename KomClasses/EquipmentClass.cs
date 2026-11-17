using System;

namespace KomatsuAPI
{
    public class EquipmentClass
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
            public Location Location { get; set; }
            public Cumulativeidlenonoperatinghours CumulativeIdleNonOperatingHours { get; set; }
            public Cumulativeoperatinghours CumulativeOperatingHours { get; set; }
            public Fuelused FuelUsed { get; set; }
            public Fuelusedlast24 FuelUsedLast24 { get; set; }
            public Fuelremaining FuelRemaining { get; set; }
        }

        public class Equipmentheader
        {
            public string OEMName { get; set; }
            public string Model { get; set; }
            public string EquipmentID { get; set; }
            public string SerialNumber { get; set; }
        }

        public class Location
        {
            public float Latitude { get; set; }
            public float Longitude { get; set; }
            public DateTime datetime { get; set; }
        }

        public class Cumulativeidlenonoperatinghours
        {
            public float Hour { get; set; }
            public DateTime datetime { get; set; }
        }

        public class Cumulativeoperatinghours
        {
            public float Hour { get; set; }
            public DateTime datetime { get; set; }
        }

        public class Fuelused
        {
            public string FuelUnits { get; set; }
            public int FuelConsumed { get; set; }
            public DateTime datetime { get; set; }
        }

        public class Fuelusedlast24
        {
            public string FuelUnits { get; set; }
            public int FuelConsumed { get; set; }
            public DateTime datetime { get; set; }
        }

        public class Fuelremaining
        {
            public float Percent { get; set; }
            public DateTime datetime { get; set; }
        }


    }
}
