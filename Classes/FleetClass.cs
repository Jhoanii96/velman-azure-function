namespace VisionLinkReader
{
    public class FleetClass
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.aemp.org/fleet")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.aemp.org/fleet", IsNullable = false)]
        public partial class Fleet
        {

            private FleetEquipment[] equipmentField;

            private byte versionField;

            private System.DateTime snapshotTimeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Equipment")]
            public FleetEquipment[] Equipment
            {
                get
                {
                    return this.equipmentField;
                }
                set
                {
                    this.equipmentField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public System.DateTime snapshotTime
            {
                get
                {
                    return this.snapshotTimeField;
                }
                set
                {
                    this.snapshotTimeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.aemp.org/fleet")]
        public partial class FleetEquipment
        {

            private FleetEquipmentEquipmentHeader equipmentHeaderField;

            private FleetEquipmentLocation locationField;

            private FleetEquipmentCumulativeOperatingHours cumulativeOperatingHoursField;

            private FleetEquipmentFuelUsed fuelUsedField;

            private FleetEquipmentFuelUsedLast24 fuelUsedLast24Field;

            private FleetEquipmentDistance distanceField;

            /// <remarks/>
            public FleetEquipmentEquipmentHeader EquipmentHeader
            {
                get
                {
                    return this.equipmentHeaderField;
                }
                set
                {
                    this.equipmentHeaderField = value;
                }
            }

            /// <remarks/>
            public FleetEquipmentLocation Location
            {
                get
                {
                    return this.locationField;
                }
                set
                {
                    this.locationField = value;
                }
            }

            /// <remarks/>
            public FleetEquipmentCumulativeOperatingHours CumulativeOperatingHours
            {
                get
                {
                    return this.cumulativeOperatingHoursField;
                }
                set
                {
                    this.cumulativeOperatingHoursField = value;
                }
            }

            /// <remarks/>
            public FleetEquipmentFuelUsed FuelUsed
            {
                get
                {
                    return this.fuelUsedField;
                }
                set
                {
                    this.fuelUsedField = value;
                }
            }

            /// <remarks/>
            public FleetEquipmentFuelUsedLast24 FuelUsedLast24
            {
                get
                {
                    return this.fuelUsedLast24Field;
                }
                set
                {
                    this.fuelUsedLast24Field = value;
                }
            }

            /// <remarks/>
            public FleetEquipmentDistance Distance
            {
                get
                {
                    return this.distanceField;
                }
                set
                {
                    this.distanceField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.aemp.org/fleet")]
        public partial class FleetEquipmentEquipmentHeader
        {

            private System.DateTime unitInstallDateTimeField;

            private string makeField;

            private string modelField;

            private string equipmentIDField;

            private string serialNumberField;

            /// <remarks/>
            public System.DateTime UnitInstallDateTime
            {
                get
                {
                    return this.unitInstallDateTimeField;
                }
                set
                {
                    this.unitInstallDateTimeField = value;
                }
            }

            /// <remarks/>
            public string Make
            {
                get
                {
                    return this.makeField;
                }
                set
                {
                    this.makeField = value;
                }
            }

            /// <remarks/>
            public string Model
            {
                get
                {
                    return this.modelField;
                }
                set
                {
                    this.modelField = value;
                }
            }

            /// <remarks/>
            public string EquipmentID
            {
                get
                {
                    return this.equipmentIDField;
                }
                set
                {
                    this.equipmentIDField = value;
                }
            }

            /// <remarks/>
            public string SerialNumber
            {
                get
                {
                    return this.serialNumberField;
                }
                set
                {
                    this.serialNumberField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.aemp.org/fleet")]
        public partial class FleetEquipmentLocation
        {

            private decimal latitudeField;

            private decimal longitudeField;

            private FleetEquipmentLocationAltitude altitudeField;

            private System.DateTime datetimeField;

            /// <remarks/>
            public decimal Latitude
            {
                get
                {
                    return this.latitudeField;
                }
                set
                {
                    this.latitudeField = value;
                }
            }

            /// <remarks/>
            public decimal Longitude
            {
                get
                {
                    return this.longitudeField;
                }
                set
                {
                    this.longitudeField = value;
                }
            }

            /// <remarks/>
            public FleetEquipmentLocationAltitude Altitude
            {
                get
                {
                    return this.altitudeField;
                }
                set
                {
                    this.altitudeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public System.DateTime datetime
            {
                get
                {
                    return this.datetimeField;
                }
                set
                {
                    this.datetimeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.aemp.org/fleet")]
        public partial class FleetEquipmentLocationAltitude
        {

            private string altitudeUnitsField;

            private uint feetField;

            private byte inchesField;

            /// <remarks/>
            public string AltitudeUnits
            {
                get
                {
                    return this.altitudeUnitsField;
                }
                set
                {
                    this.altitudeUnitsField = value;
                }
            }

            /// <remarks/>
            public uint Feet
            {
                get
                {
                    return this.feetField;
                }
                set
                {
                    this.feetField = value;
                }
            }

            /// <remarks/>
            public byte Inches
            {
                get
                {
                    return this.inchesField;
                }
                set
                {
                    this.inchesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.aemp.org/fleet")]
        public partial class FleetEquipmentCumulativeOperatingHours
        {

            private string hourField;

            private System.DateTime resetDateTimeField;

            private System.DateTime datetimeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
            public string Hour
            {
                get
                {
                    return this.hourField;
                }
                set
                {
                    this.hourField = value;
                }
            }

            /// <remarks/>
            public System.DateTime ResetDateTime
            {
                get
                {
                    return this.resetDateTimeField;
                }
                set
                {
                    this.resetDateTimeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public System.DateTime datetime
            {
                get
                {
                    return this.datetimeField;
                }
                set
                {
                    this.datetimeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.aemp.org/fleet")]
        public partial class FleetEquipmentFuelUsed
        {

            private string fuelUnitsField;

            private uint fuelConsumedField;

            private System.DateTime datetimeField;

            /// <remarks/>
            public string FuelUnits
            {
                get
                {
                    return this.fuelUnitsField;
                }
                set
                {
                    this.fuelUnitsField = value;
                }
            }

            /// <remarks/>
            public uint FuelConsumed
            {
                get
                {
                    return this.fuelConsumedField;
                }
                set
                {
                    this.fuelConsumedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public System.DateTime datetime
            {
                get
                {
                    return this.datetimeField;
                }
                set
                {
                    this.datetimeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.aemp.org/fleet")]
        public partial class FleetEquipmentFuelUsedLast24
        {

            private string fuelUnitsField;

            private uint fuelConsumedField;

            private System.DateTime datetimeField;

            /// <remarks/>
            public string FuelUnits
            {
                get
                {
                    return this.fuelUnitsField;
                }
                set
                {
                    this.fuelUnitsField = value;
                }
            }

            /// <remarks/>
            public uint FuelConsumed
            {
                get
                {
                    return this.fuelConsumedField;
                }
                set
                {
                    this.fuelConsumedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public System.DateTime datetime
            {
                get
                {
                    return this.datetimeField;
                }
                set
                {
                    this.datetimeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.aemp.org/fleet")]
        public partial class FleetEquipmentDistance
        {

            private string odometerUnitsField;

            private uint odometerField;

            private System.DateTime resetDateTimeField;

            private System.DateTime datetimeField;

            /// <remarks/>
            public string OdometerUnits
            {
                get
                {
                    return this.odometerUnitsField;
                }
                set
                {
                    this.odometerUnitsField = value;
                }
            }

            /// <remarks/>
            public uint Odometer
            {
                get
                {
                    return this.odometerField;
                }
                set
                {
                    this.odometerField = value;
                }
            }

            /// <remarks/>
            public System.DateTime ResetDateTime
            {
                get
                {
                    return this.resetDateTimeField;
                }
                set
                {
                    this.resetDateTimeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public System.DateTime datetime
            {
                get
                {
                    return this.datetimeField;
                }
                set
                {
                    this.datetimeField = value;
                }
            }
        }


    }
}
