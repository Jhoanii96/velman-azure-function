namespace VisionLinkReader
{
    public class SMULocationClass
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.myvisionlink.com/feed/v4/ServiceMeterLocation")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.myvisionlink.com/feed/v4/ServiceMeterLocation", IsNullable = false)]
        public partial class topic
        {

            private nextBuffer nextBufferField;

            private message[] messageField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
            public nextBuffer nextBuffer
            {
                get
                {
                    return this.nextBufferField;
                }
                set
                {
                    this.nextBufferField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("message", Namespace = "")]
            public message[] message
            {
                get
                {
                    return this.messageField;
                }
                set
                {
                    this.messageField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class nextBuffer
        {

            private string urlField;

            private bool moreDataField;

            /// <remarks/>
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

            /// <remarks/>
            public bool moreData
            {
                get
                {
                    return this.moreDataField;
                }
                set
                {
                    this.moreDataField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class message
        {

            private messageHeader headerField;

            private messageServiceMeter serviceMeterField;

            private messageMileage mileageField;

            private messageLocation locationField;

            /// <remarks/>
            public messageHeader header
            {
                get
                {
                    return this.headerField;
                }
                set
                {
                    this.headerField = value;
                }
            }

            /// <remarks/>
            public messageServiceMeter serviceMeter
            {
                get
                {
                    return this.serviceMeterField;
                }
                set
                {
                    this.serviceMeterField = value;
                }
            }

            /// <remarks/>
            public messageMileage mileage
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
            public messageLocation location
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class messageHeader
        {

            private long messageIdField;

            private long masterMsgIdField;

            private messageHeaderEquipment equipmentField;

            private string moduleCodeField;

            private string moduleTimeField;

            private string receivedTimeField;

            private string ownerField;

            /// <remarks/>
            public long messageId
            {
                get
                {
                    return this.messageIdField;
                }
                set
                {
                    this.messageIdField = value;
                }
            }

            /// <remarks/>
            public long masterMsgId
            {
                get
                {
                    return this.masterMsgIdField;
                }
                set
                {
                    this.masterMsgIdField = value;
                }
            }

            /// <remarks/>
            public messageHeaderEquipment equipment
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
            public string moduleCode
            {
                get
                {
                    return this.moduleCodeField;
                }
                set
                {
                    this.moduleCodeField = value;
                }
            }

            /// <remarks/>
            public string moduleTime
            {
                get
                {
                    return this.moduleTimeField;
                }
                set
                {
                    this.moduleTimeField = value;
                }
            }

            /// <remarks/>
            public string receivedTime
            {
                get
                {
                    return this.receivedTimeField;
                }
                set
                {
                    this.receivedTimeField = value;
                }
            }

            /// <remarks/>
            public string owner
            {
                get
                {
                    return this.ownerField;
                }
                set
                {
                    this.ownerField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class messageHeaderEquipment
        {

            private string serialNumberField;

            private string makeField;

            private string modelField;

            private string nicknameField;

            private string equipmentVINField;

            /// <remarks/>
            public string serialNumber
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
            public string make
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
            public string model
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
            public string nickname
            {
                get
                {
                    return this.nicknameField;
                }
                set
                {
                    this.nicknameField = value;
                }
            }

            /// <remarks/>
            public string equipmentVIN
            {
                get
                {
                    return this.equipmentVINField;
                }
                set
                {
                    this.equipmentVINField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class messageServiceMeter
        {

            private double valueField;

            private string uomField;

            /// <remarks/>
            public double value
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
        public partial class messageMileage
        {

            private long valueField;

            private string uomField;

            /// <remarks/>
            public long value
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
        public partial class messageLocation
        {

            private double latitudeField;

            private double longitudeField;

            private string validityField;

            /// <remarks/>
            public double latitude
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
            public double longitude
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
            public string validity
            {
                get
                {
                    return this.validityField;
                }
                set
                {
                    this.validityField = value;
                }
            }
        }


    }
}
