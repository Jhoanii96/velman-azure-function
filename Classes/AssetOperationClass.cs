namespace VisionLinkReader
{
    public class AssetOperationClasses
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class AssetOperations
        {

            private AssetOperationsLink[]? navField;

            private bool isLastPageField;

            private AssetOperationsAssetOperation[]? assetOperationField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Link")]
            public AssetOperationsLink[] Nav
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
            [System.Xml.Serialization.XmlElementAttribute("AssetOperation")]
            public AssetOperationsAssetOperation[] AssetOperation
            {
                get
                {
                    return this.assetOperationField;
                }
                set
                {
                    this.assetOperationField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AssetOperationsLink
        {

            private string? relField;

            private string? methodsField;

            private string? hrefField;

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
        public partial class AssetOperationsAssetOperation
        {

            private string? errorField;

            private AssetOperationsAssetOperationAsset? assetField;

            private AssetOperationsAssetOperationAssetOperationDate[]? assetOperationDetailsField;

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
            public AssetOperationsAssetOperationAsset Asset
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
            [System.Xml.Serialization.XmlArrayItemAttribute("AssetOperationDate")]
            public AssetOperationsAssetOperationAssetOperationDate[] AssetOperationDetails
            {
                get
                {
                    return this.assetOperationDetailsField;
                }
                set
                {
                    this.assetOperationDetailsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AssetOperationsAssetOperationAsset
        {

            private string errorField;

            private long visionLinkIdentifierField;

            private string makeCodeField;

            private string serialNumberField;

            private string assetIDField;

            private string modelField;

            private string urlField;

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
        public partial class AssetOperationsAssetOperationAssetOperationDate
        {

            private string calendarDayAssetLocalTimeField;

            private AssetOperationsAssetOperationAssetOperationDateAssetOperationDateInfo[] assetOperationDateInfoField;

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
            [System.Xml.Serialization.XmlElementAttribute("AssetOperationDateInfo")]
            public AssetOperationsAssetOperationAssetOperationDateAssetOperationDateInfo[] AssetOperationDateInfo
            {
                get
                {
                    return this.assetOperationDateInfoField;
                }
                set
                {
                    this.assetOperationDateInfoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AssetOperationsAssetOperationAssetOperationDateAssetOperationDateInfo
        {

            private string errorField;

            private System.DateTime startStateUtcField;

            private string startStateAssetLocalTimeField;

            private string startStateAssetTimeZoneAbbrevField;

            private AssetOperationsAssetOperationAssetOperationDateAssetOperationDateInfoStartLocation startLocationField;

            private System.DateTime endStateUtcField;

            private string endStateAssetLocalTimeField;

            private string endStateAssetTimeZoneAbbrevField;

            private AssetOperationsAssetOperationAssetOperationDateAssetOperationDateInfoEndLocation endLocationField;

            private long durationSecondsField;

            private string workDefinitionField;

            private string workingStateField;

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
            public System.DateTime StartStateUtc
            {
                get
                {
                    return this.startStateUtcField;
                }
                set
                {
                    this.startStateUtcField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public string StartStateAssetLocalTime
            {
                get
                {
                    return this.startStateAssetLocalTimeField;
                }
                set
                {
                    this.startStateAssetLocalTimeField = value;
                }
            }

            /// <remarks/>
            public string StartStateAssetTimeZoneAbbrev
            {
                get
                {
                    return this.startStateAssetTimeZoneAbbrevField;
                }
                set
                {
                    this.startStateAssetTimeZoneAbbrevField = value;
                }
            }

            /// <remarks/>
            public AssetOperationsAssetOperationAssetOperationDateAssetOperationDateInfoStartLocation StartLocation
            {
                get
                {
                    return this.startLocationField;
                }
                set
                {
                    this.startLocationField = value;
                }
            }

            /// <remarks/>
            public System.DateTime EndStateUtc
            {
                get
                {
                    return this.endStateUtcField;
                }
                set
                {
                    this.endStateUtcField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public string EndStateAssetLocalTime
            {
                get
                {
                    return this.endStateAssetLocalTimeField;
                }
                set
                {
                    this.endStateAssetLocalTimeField = value;
                }
            }

            /// <remarks/>
            public string EndStateAssetTimeZoneAbbrev
            {
                get
                {
                    return this.endStateAssetTimeZoneAbbrevField;
                }
                set
                {
                    this.endStateAssetTimeZoneAbbrevField = value;
                }
            }

            /// <remarks/>
            public AssetOperationsAssetOperationAssetOperationDateAssetOperationDateInfoEndLocation EndLocation
            {
                get
                {
                    return this.endLocationField;
                }
                set
                {
                    this.endLocationField = value;
                }
            }

            /// <remarks/>
            public long DurationSeconds
            {
                get
                {
                    return this.durationSecondsField;
                }
                set
                {
                    this.durationSecondsField = value;
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

            /// <remarks/>
            public string WorkingState
            {
                get
                {
                    return this.workingStateField;
                }
                set
                {
                    this.workingStateField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AssetOperationsAssetOperationAssetOperationDateAssetOperationDateInfoStartLocation
        {

            private string errorField;

            private string latitudeField;

            private string longitudeField;

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
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public string Latitude
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
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public string Longitude
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AssetOperationsAssetOperationAssetOperationDateAssetOperationDateInfoEndLocation
        {

            private string errorField;

            private string latitudeField;

            private string longitudeField;

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
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public string Latitude
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
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public string Longitude
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
        }


    }
}
