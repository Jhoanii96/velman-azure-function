namespace VisionLinkReader
{
    public class FuelUtilizationClass
    {


        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class FuelUtilizationList
        {

            private FuelUtilizationListLink[] navField;

            private bool isLastPageField;

            private FuelUtilizationListFuelUtilization[] fuelUtilizationField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Link")]
            public FuelUtilizationListLink[] Nav
            {
                get
                {
                    return this.navField;
                }
                set
                {
                    this.navField = value;
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
            [System.Xml.Serialization.XmlElementAttribute("FuelUtilization")]
            public FuelUtilizationListFuelUtilization[] FuelUtilization
            {
                get
                {
                    return this.fuelUtilizationField;
                }
                set
                {
                    this.fuelUtilizationField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListLink
        {

            private string relField;

            private string methodsField;

            private string hrefField;

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
            public string methods
            {
                get
                {
                    return this.methodsField;
                }
                set
                {
                    this.methodsField = value;
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
        public partial class FuelUtilizationListFuelUtilization
        {

            private string errorField;

            private FuelUtilizationListFuelUtilizationAsset assetField;

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetail[] fuelUtilizationDetailsField;

            /// <remarks/>
            public string Error
            {
                get
                {
                    return this.errorField;
                }
                set
                {
                    this.errorField = value;
                }
            }

            /// <remarks/>
            public FuelUtilizationListFuelUtilizationAsset Asset
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
            [System.Xml.Serialization.XmlArrayItemAttribute("FuelUtilizationDetail", IsNullable = false)]
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetail[] FuelUtilizationDetails
            {
                get
                {
                    return this.fuelUtilizationDetailsField;
                }
                set
                {
                    this.fuelUtilizationDetailsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationAsset
        {

            private long visionLinkIdentifierField;

            private string makeCodeField;

            private string serialNumberField;

            private string assetIDField;

            private string modelField;

            private string urlField;

            /// <remarks/>
            public long VisionLinkIdentifier
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
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetail
        {

            private string errorField;

            private string calendarDayAssetLocalTimeField;

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetailIdleFuelBurnedGallons idleFuelBurnedGallonsField;

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetailIdleHours idleHoursField;

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetailIdleFuelBurnRate idleFuelBurnRateField;

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetailWorkingFuelBurnedGallons workingFuelBurnedGallonsField;

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetailWorkingHours workingHoursField;

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetailWorkingFuelBurnRate workingFuelBurnRateField;

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetailRuntimeFuelBurnedGallons runtimeFuelBurnedGallonsField;

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetailRuntimeHours runtimeHoursField;

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetailRuntimeFuelBurnRate runtimeFuelBurnRateField;

            private string workDefinitionField;

            /// <remarks/>
            public string Error
            {
                get
                {
                    return this.errorField;
                }
                set
                {
                    this.errorField = value;
                }
            }

            /// <remarks/>
            public string CalendarDayAssetLocalTime
            {
                get
                {
                    return this.calendarDayAssetLocalTimeField;
                }
                set
                {
                    this.calendarDayAssetLocalTimeField = value;
                }
            }

            /// <remarks/>
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetailIdleFuelBurnedGallons IdleFuelBurnedGallons
            {
                get
                {
                    return this.idleFuelBurnedGallonsField;
                }
                set
                {
                    this.idleFuelBurnedGallonsField = value;
                }
            }

            /// <remarks/>
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetailIdleHours IdleHours
            {
                get
                {
                    return this.idleHoursField;
                }
                set
                {
                    this.idleHoursField = value;
                }
            }

            /// <remarks/>
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetailIdleFuelBurnRate IdleFuelBurnRate
            {
                get
                {
                    return this.idleFuelBurnRateField;
                }
                set
                {
                    this.idleFuelBurnRateField = value;
                }
            }

            /// <remarks/>
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetailWorkingFuelBurnedGallons WorkingFuelBurnedGallons
            {
                get
                {
                    return this.workingFuelBurnedGallonsField;
                }
                set
                {
                    this.workingFuelBurnedGallonsField = value;
                }
            }

            /// <remarks/>
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetailWorkingHours WorkingHours
            {
                get
                {
                    return this.workingHoursField;
                }
                set
                {
                    this.workingHoursField = value;
                }
            }

            /// <remarks/>
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetailWorkingFuelBurnRate WorkingFuelBurnRate
            {
                get
                {
                    return this.workingFuelBurnRateField;
                }
                set
                {
                    this.workingFuelBurnRateField = value;
                }
            }

            /// <remarks/>
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetailRuntimeFuelBurnedGallons RuntimeFuelBurnedGallons
            {
                get
                {
                    return this.runtimeFuelBurnedGallonsField;
                }
                set
                {
                    this.runtimeFuelBurnedGallonsField = value;
                }
            }

            /// <remarks/>
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetailRuntimeHours RuntimeHours
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
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetailRuntimeFuelBurnRate RuntimeFuelBurnRate
            {
                get
                {
                    return this.runtimeFuelBurnRateField;
                }
                set
                {
                    this.runtimeFuelBurnRateField = value;
                }
            }

            /// <remarks/>
            public string WorkDefinition
            {
                get
                {
                    return this.workDefinitionField;
                }
                set
                {
                    this.workDefinitionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetailIdleFuelBurnedGallons
        {

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetailIdleFuelBurnedGallonsCallout calloutField;

            private string valueField;

            private bool supportedField;

            private bool receivedField;

            /// <remarks/>
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetailIdleFuelBurnedGallonsCallout Callout
            {
                get
                {
                    return this.calloutField;
                }
                set
                {
                    this.calloutField = value;
                }
            }

            /// <remarks/>
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
            public bool supported
            {
                get
                {
                    return this.supportedField;
                }
                set
                {
                    this.supportedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool received
            {
                get
                {
                    return this.receivedField;
                }
                set
                {
                    this.receivedField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetailIdleFuelBurnedGallonsCallout
        {

            private string valueField;

            private string messageUrlField;

            /// <remarks/>
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
            public string messageUrl
            {
                get
                {
                    return this.messageUrlField;
                }
                set
                {
                    this.messageUrlField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetailIdleHours
        {

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetailIdleHoursCallout calloutField;

            private string valueField;

            private bool supportedField;

            private bool receivedField;

            /// <remarks/>
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetailIdleHoursCallout Callout
            {
                get
                {
                    return this.calloutField;
                }
                set
                {
                    this.calloutField = value;
                }
            }

            /// <remarks/>
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
            public bool supported
            {
                get
                {
                    return this.supportedField;
                }
                set
                {
                    this.supportedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool received
            {
                get
                {
                    return this.receivedField;
                }
                set
                {
                    this.receivedField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetailIdleHoursCallout
        {

            private string valueField;

            private string messageUrlField;

            /// <remarks/>
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
            public string messageUrl
            {
                get
                {
                    return this.messageUrlField;
                }
                set
                {
                    this.messageUrlField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetailIdleFuelBurnRate
        {

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetailIdleFuelBurnRateCallout[] calloutsField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Callout", IsNullable = false)]
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetailIdleFuelBurnRateCallout[] Callouts
            {
                get
                {
                    return this.calloutsField;
                }
                set
                {
                    this.calloutsField = value;
                }
            }

            /// <remarks/>
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetailIdleFuelBurnRateCallout
        {

            private string valueField;

            private string messageUrlField;

            /// <remarks/>
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
            public string messageUrl
            {
                get
                {
                    return this.messageUrlField;
                }
                set
                {
                    this.messageUrlField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetailWorkingFuelBurnedGallons
        {

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetailWorkingFuelBurnedGallonsCallout calloutField;

            private string valueField;

            private bool supportedField;

            private bool receivedField;

            /// <remarks/>
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetailWorkingFuelBurnedGallonsCallout Callout
            {
                get
                {
                    return this.calloutField;
                }
                set
                {
                    this.calloutField = value;
                }
            }

            /// <remarks/>
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
            public bool supported
            {
                get
                {
                    return this.supportedField;
                }
                set
                {
                    this.supportedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool received
            {
                get
                {
                    return this.receivedField;
                }
                set
                {
                    this.receivedField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetailWorkingFuelBurnedGallonsCallout
        {

            private string valueField;

            private string messageUrlField;

            /// <remarks/>
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
            public string messageUrl
            {
                get
                {
                    return this.messageUrlField;
                }
                set
                {
                    this.messageUrlField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetailWorkingHours
        {

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetailWorkingHoursCallout calloutField;

            private string valueField;

            private bool supportedField;

            private bool receivedField;

            /// <remarks/>
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetailWorkingHoursCallout Callout
            {
                get
                {
                    return this.calloutField;
                }
                set
                {
                    this.calloutField = value;
                }
            }

            /// <remarks/>
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
            public bool supported
            {
                get
                {
                    return this.supportedField;
                }
                set
                {
                    this.supportedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool received
            {
                get
                {
                    return this.receivedField;
                }
                set
                {
                    this.receivedField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetailWorkingHoursCallout
        {

            private string valueField;

            private string messageUrlField;

            /// <remarks/>
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
            public string messageUrl
            {
                get
                {
                    return this.messageUrlField;
                }
                set
                {
                    this.messageUrlField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetailWorkingFuelBurnRate
        {

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetailWorkingFuelBurnRateCallout[] calloutsField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Callout", IsNullable = false)]
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetailWorkingFuelBurnRateCallout[] Callouts
            {
                get
                {
                    return this.calloutsField;
                }
                set
                {
                    this.calloutsField = value;
                }
            }

            /// <remarks/>
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetailWorkingFuelBurnRateCallout
        {

            private string valueField;

            private string messageUrlField;

            /// <remarks/>
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
            public string messageUrl
            {
                get
                {
                    return this.messageUrlField;
                }
                set
                {
                    this.messageUrlField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetailRuntimeFuelBurnedGallons
        {

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetailRuntimeFuelBurnedGallonsCallout calloutField;

            private string valueField;

            private bool supportedField;

            private bool receivedField;

            /// <remarks/>
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetailRuntimeFuelBurnedGallonsCallout Callout
            {
                get
                {
                    return this.calloutField;
                }
                set
                {
                    this.calloutField = value;
                }
            }

            /// <remarks/>
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
            public bool supported
            {
                get
                {
                    return this.supportedField;
                }
                set
                {
                    this.supportedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool received
            {
                get
                {
                    return this.receivedField;
                }
                set
                {
                    this.receivedField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetailRuntimeFuelBurnedGallonsCallout
        {

            private string valueField;

            private string messageUrlField;

            /// <remarks/>
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
            public string messageUrl
            {
                get
                {
                    return this.messageUrlField;
                }
                set
                {
                    this.messageUrlField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetailRuntimeHours
        {

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetailRuntimeHoursCallout calloutField;

            private string valueField;

            private bool supportedField;

            private bool receivedField;

            /// <remarks/>
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetailRuntimeHoursCallout Callout
            {
                get
                {
                    return this.calloutField;
                }
                set
                {
                    this.calloutField = value;
                }
            }

            /// <remarks/>
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
            public bool supported
            {
                get
                {
                    return this.supportedField;
                }
                set
                {
                    this.supportedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool received
            {
                get
                {
                    return this.receivedField;
                }
                set
                {
                    this.receivedField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetailRuntimeHoursCallout
        {

            private string valueField;

            private string messageUrlField;

            /// <remarks/>
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
            public string messageUrl
            {
                get
                {
                    return this.messageUrlField;
                }
                set
                {
                    this.messageUrlField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetailRuntimeFuelBurnRate
        {

            private FuelUtilizationListFuelUtilizationFuelUtilizationDetailRuntimeFuelBurnRateCallout[] calloutsField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Callout", IsNullable = false)]
            public FuelUtilizationListFuelUtilizationFuelUtilizationDetailRuntimeFuelBurnRateCallout[] Callouts
            {
                get
                {
                    return this.calloutsField;
                }
                set
                {
                    this.calloutsField = value;
                }
            }

            /// <remarks/>
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FuelUtilizationListFuelUtilizationFuelUtilizationDetailRuntimeFuelBurnRateCallout
        {

            private string valueField;

            private string messageUrlField;

            /// <remarks/>
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
            public string messageUrl
            {
                get
                {
                    return this.messageUrlField;
                }
                set
                {
                    this.messageUrlField = value;
                }
            }
        }

    }
}
