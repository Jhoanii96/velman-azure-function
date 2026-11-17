namespace VisionLinkReader
{
    public class AssetUtilizationClass
    {


        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class AssetUtilizationList
        {

            private AssetUtilizationListLink[] navField;

            private bool isLastPageField;

            private AssetUtilizationListAssetUtilization[] assetUtilizationField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Link")]
            public AssetUtilizationListLink[] Nav
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
            [System.Xml.Serialization.XmlElementAttribute("AssetUtilization")]
            public AssetUtilizationListAssetUtilization[] AssetUtilization
            {
                get
                {
                    return this.assetUtilizationField;
                }
                set
                {
                    this.assetUtilizationField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AssetUtilizationListLink
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
        public partial class AssetUtilizationListAssetUtilization
        {

            private string errorField;

            private AssetUtilizationListAssetUtilizationAsset assetField;

            private AssetUtilizationListAssetUtilizationAssetUtilizationDetail[] assetUtilizationDetailsField;

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
            public AssetUtilizationListAssetUtilizationAsset Asset
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
            [System.Xml.Serialization.XmlArrayItemAttribute("AssetUtilizationDetail", IsNullable = false)]
            public AssetUtilizationListAssetUtilizationAssetUtilizationDetail[] AssetUtilizationDetails
            {
                get
                {
                    return this.assetUtilizationDetailsField;
                }
                set
                {
                    this.assetUtilizationDetailsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AssetUtilizationListAssetUtilizationAsset
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
        public partial class AssetUtilizationListAssetUtilizationAssetUtilizationDetail
        {

            private string errorField;

            private string calendarDayAssetLocalTimeField;

            private AssetUtilizationListAssetUtilizationAssetUtilizationDetailIdleHours idleHoursField;

            private AssetUtilizationListAssetUtilizationAssetUtilizationDetailWorkingHours workingHoursField;

            private AssetUtilizationListAssetUtilizationAssetUtilizationDetailRuntimeHours runtimeHoursField;

            private double expectedRuntimeHoursField;

            private AssetUtilizationListAssetUtilizationAssetUtilizationDetailWorkingUtilizationPercentage workingUtilizationPercentageField;

            private AssetUtilizationListAssetUtilizationAssetUtilizationDetailRunningUtilizationPercentage runningUtilizationPercentageField;

            private AssetUtilizationListAssetUtilizationAssetUtilizationDetailEfficiencyPercentage efficiencyPercentageField;

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
            public AssetUtilizationListAssetUtilizationAssetUtilizationDetailIdleHours IdleHours
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
            public AssetUtilizationListAssetUtilizationAssetUtilizationDetailWorkingHours WorkingHours
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
            public AssetUtilizationListAssetUtilizationAssetUtilizationDetailRuntimeHours RuntimeHours
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
            public double ExpectedRuntimeHours
            {
                get
                {
                    return this.expectedRuntimeHoursField;
                }
                set
                {
                    this.expectedRuntimeHoursField = value;
                }
            }

            /// <remarks/>
            public AssetUtilizationListAssetUtilizationAssetUtilizationDetailWorkingUtilizationPercentage WorkingUtilizationPercentage
            {
                get
                {
                    return this.workingUtilizationPercentageField;
                }
                set
                {
                    this.workingUtilizationPercentageField = value;
                }
            }

            /// <remarks/>
            public AssetUtilizationListAssetUtilizationAssetUtilizationDetailRunningUtilizationPercentage RunningUtilizationPercentage
            {
                get
                {
                    return this.runningUtilizationPercentageField;
                }
                set
                {
                    this.runningUtilizationPercentageField = value;
                }
            }

            /// <remarks/>
            public AssetUtilizationListAssetUtilizationAssetUtilizationDetailEfficiencyPercentage EfficiencyPercentage
            {
                get
                {
                    return this.efficiencyPercentageField;
                }
                set
                {
                    this.efficiencyPercentageField = value;
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
        public partial class AssetUtilizationListAssetUtilizationAssetUtilizationDetailIdleHours
        {

            private AssetUtilizationListAssetUtilizationAssetUtilizationDetailIdleHoursCallout calloutField;

            private string valueField;

            private bool supportedField;

            private bool receivedField;

            /// <remarks/>
            public AssetUtilizationListAssetUtilizationAssetUtilizationDetailIdleHoursCallout Callout
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
        public partial class AssetUtilizationListAssetUtilizationAssetUtilizationDetailIdleHoursCallout
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
        public partial class AssetUtilizationListAssetUtilizationAssetUtilizationDetailWorkingHours
        {

            private AssetUtilizationListAssetUtilizationAssetUtilizationDetailWorkingHoursCallout calloutField;

            private string valueField;

            private bool supportedField;

            private bool receivedField;

            /// <remarks/>
            public AssetUtilizationListAssetUtilizationAssetUtilizationDetailWorkingHoursCallout Callout
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
        public partial class AssetUtilizationListAssetUtilizationAssetUtilizationDetailWorkingHoursCallout
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
        public partial class AssetUtilizationListAssetUtilizationAssetUtilizationDetailRuntimeHours
        {

            private AssetUtilizationListAssetUtilizationAssetUtilizationDetailRuntimeHoursCallout calloutField;

            private string valueField;

            private bool supportedField;

            private bool receivedField;

            /// <remarks/>
            public AssetUtilizationListAssetUtilizationAssetUtilizationDetailRuntimeHoursCallout Callout
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
        public partial class AssetUtilizationListAssetUtilizationAssetUtilizationDetailRuntimeHoursCallout
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
        public partial class AssetUtilizationListAssetUtilizationAssetUtilizationDetailWorkingUtilizationPercentage
        {

            private AssetUtilizationListAssetUtilizationAssetUtilizationDetailWorkingUtilizationPercentageCallout[] calloutsField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Callout", IsNullable = false)]
            public AssetUtilizationListAssetUtilizationAssetUtilizationDetailWorkingUtilizationPercentageCallout[] Callouts
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
        public partial class AssetUtilizationListAssetUtilizationAssetUtilizationDetailWorkingUtilizationPercentageCallout
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
        public partial class AssetUtilizationListAssetUtilizationAssetUtilizationDetailRunningUtilizationPercentage
        {

            private AssetUtilizationListAssetUtilizationAssetUtilizationDetailRunningUtilizationPercentageCallout[] calloutsField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Callout", IsNullable = false)]
            public AssetUtilizationListAssetUtilizationAssetUtilizationDetailRunningUtilizationPercentageCallout[] Callouts
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
        public partial class AssetUtilizationListAssetUtilizationAssetUtilizationDetailRunningUtilizationPercentageCallout
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
        public partial class AssetUtilizationListAssetUtilizationAssetUtilizationDetailEfficiencyPercentage
        {

            private AssetUtilizationListAssetUtilizationAssetUtilizationDetailEfficiencyPercentageCallout[] calloutsField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Callout", IsNullable = false)]
            public AssetUtilizationListAssetUtilizationAssetUtilizationDetailEfficiencyPercentageCallout[] Callouts
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
        public partial class AssetUtilizationListAssetUtilizationAssetUtilizationDetailEfficiencyPercentageCallout
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
