namespace VisionLinkReader
{
    public class FleetSummaryClass
    {


        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class FleetSummary
        {

            private FleetSummaryLink linkField;

            private FleetSummaryStyle styleField;

            private bool isLastPageField;

            private FleetSummaryFleetSummaryRecord[] fleetSummaryRecordsField;

            /// <remarks/>
            public FleetSummaryLink link
            {
                get
                {
                    return this.linkField;
                }
                set
                {
                    this.linkField = value;
                }
            }

            /// <remarks/>
            public FleetSummaryStyle style
            {
                get
                {
                    return this.styleField;
                }
                set
                {
                    this.styleField = value;
                }
            }

            /// <remarks/>
            public bool IsLastPage
            {
                get
                {
                    return this.isLastPageField;
                }
                set
                {
                    this.isLastPageField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("FleetSummaryRecord", IsNullable = false)]
            public FleetSummaryFleetSummaryRecord[] FleetSummaryRecords
            {
                get
                {
                    return this.fleetSummaryRecordsField;
                }
                set
                {
                    this.fleetSummaryRecordsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FleetSummaryLink
        {

            private string typeField;

            private string idField;

            private string relField;

            private string hrefField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string rel
            {
                get
                {
                    return this.relField;
                }
                set
                {
                    this.relField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string href
            {
                get
                {
                    return this.hrefField;
                }
                set
                {
                    this.hrefField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FleetSummaryStyle
        {

            private string typeField;

            private string idField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FleetSummaryFleetSummaryRecord
        {

            private FleetSummaryFleetSummaryRecordAsset assetField;

            private ulong visionLinkIdentifierField;

            private decimal latitudeField;

            private decimal longitudeField;

            private System.DateTime lastLocationUTCField;

            private string addressField;

            private System.DateTime lastReportedUTCField;

            private string fuelPercentRemainingField;

            private FleetSummaryFleetSummaryRecordMileage mileageField;

            private FleetSummaryFleetSummaryRecordSpeed speedField;

            private FleetSummaryFleetSummaryRecordRuntimeHours runtimeHoursField;

            private FleetSummaryFleetSummaryRecordLifetimeFuelConsumed lifetimeFuelConsumedField;

            private FleetSummaryFleetSummaryRecordAltitude altitudeField;

            private string lastKnownStatusField;

            /// <remarks/>
            public FleetSummaryFleetSummaryRecordAsset Asset
            {
                get
                {
                    return this.assetField;
                }
                set
                {
                    this.assetField = value;
                }
            }

            /// <remarks/>
            public ulong VisionLinkIdentifier
            {
                get
                {
                    return this.visionLinkIdentifierField;
                }
                set
                {
                    this.visionLinkIdentifierField = value;
                }
            }

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
            public System.DateTime LastLocationUTC
            {
                get
                {
                    return this.lastLocationUTCField;
                }
                set
                {
                    this.lastLocationUTCField = value;
                }
            }

            /// <remarks/>
            public string Address
            {
                get
                {
                    return this.addressField;
                }
                set
                {
                    this.addressField = value;
                }
            }

            /// <remarks/>
            public System.DateTime LastReportedUTC
            {
                get
                {
                    return this.lastReportedUTCField;
                }
                set
                {
                    this.lastReportedUTCField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public string FuelPercentRemaining
            {
                get
                {
                    return this.fuelPercentRemainingField;
                }
                set
                {
                    this.fuelPercentRemainingField = value;
                }
            }

            /// <remarks/>
            public FleetSummaryFleetSummaryRecordMileage Mileage
            {
                get
                {
                    return this.mileageField;
                }
                set
                {
                    this.mileageField = value;
                }
            }

            /// <remarks/>
            public FleetSummaryFleetSummaryRecordSpeed Speed
            {
                get
                {
                    return this.speedField;
                }
                set
                {
                    this.speedField = value;
                }
            }

            /// <remarks/>
            public FleetSummaryFleetSummaryRecordRuntimeHours RuntimeHours
            {
                get
                {
                    return this.runtimeHoursField;
                }
                set
                {
                    this.runtimeHoursField = value;
                }
            }

            /// <remarks/>
            public FleetSummaryFleetSummaryRecordLifetimeFuelConsumed LifetimeFuelConsumed
            {
                get
                {
                    return this.lifetimeFuelConsumedField;
                }
                set
                {
                    this.lifetimeFuelConsumedField = value;
                }
            }

            /// <remarks/>
            public FleetSummaryFleetSummaryRecordAltitude Altitude
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
            public string LastKnownStatus
            {
                get
                {
                    return this.lastKnownStatusField;
                }
                set
                {
                    this.lastKnownStatusField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FleetSummaryFleetSummaryRecordAsset
        {

            private ulong visionLinkIdentifierField;

            private string makeCodeField;

            private string serialNumberField;

            private string assetIDField;

            private string modelField;

            private string urlField;

            /// <remarks/>
            public ulong VisionLinkIdentifier
            {
                get
                {
                    return this.visionLinkIdentifierField;
                }
                set
                {
                    this.visionLinkIdentifierField = value;
                }
            }

            /// <remarks/>
            public string MakeCode
            {
                get
                {
                    return this.makeCodeField;
                }
                set
                {
                    this.makeCodeField = value;
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

            /// <remarks/>
            public string AssetID
            {
                get
                {
                    return this.assetIDField;
                }
                set
                {
                    this.assetIDField = value;
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
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string url
            {
                get
                {
                    return this.urlField;
                }
                set
                {
                    this.urlField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FleetSummaryFleetSummaryRecordMileage
        {

            private string valueField;

            private string uomField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string uom
            {
                get
                {
                    return this.uomField;
                }
                set
                {
                    this.uomField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FleetSummaryFleetSummaryRecordSpeed
        {

            private object valueField;

            private string uomField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string uom
            {
                get
                {
                    return this.uomField;
                }
                set
                {
                    this.uomField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FleetSummaryFleetSummaryRecordRuntimeHours
        {

            private decimal valueField;

            private string uomField;

            /// <remarks/>
            public decimal Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string uom
            {
                get
                {
                    return this.uomField;
                }
                set
                {
                    this.uomField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FleetSummaryFleetSummaryRecordLifetimeFuelConsumed
        {

            private decimal valueField;

            private string uomField;

            /// <remarks/>
            public decimal Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string uom
            {
                get
                {
                    return this.uomField;
                }
                set
                {
                    this.uomField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FleetSummaryFleetSummaryRecordAltitude
        {

            private object valueField;

            private string uomField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string uom
            {
                get
                {
                    return this.uomField;
                }
                set
                {
                    this.uomField = value;
                }
            }
        }





    }
}
