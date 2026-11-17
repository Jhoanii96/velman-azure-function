namespace VisionLinkReader
{
    public class DiagnosticClass
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.myvisionlink.com/feed/v4/Diagnostic")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.myvisionlink.com/feed/v4/Diagnostic", IsNullable = false)]
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

            private byte numberOfDiagnosticsField;

            private messageDiagnostic[] diagnosticBlockField;

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
            public byte numberOfDiagnostics
            {
                get
                {
                    return this.numberOfDiagnosticsField;
                }
                set
                {
                    this.numberOfDiagnosticsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("diagnostic", IsNullable = false)]
            public messageDiagnostic[] diagnosticBlock
            {
                get
                {
                    return this.diagnosticBlockField;
                }
                set
                {
                    this.diagnosticBlockField = value;
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
        public partial class messageDiagnostic
        {

            private int levelField;

            private int midField;

            private int spnField;

            private int fmiField;

            private int occurancesField;

            private string timestampField;

            /// <remarks/>
            public int level
            {
                get
                {
                    return this.levelField;
                }
                set
                {
                    this.levelField = value;
                }
            }

            /// <remarks/>
            public int mid
            {
                get
                {
                    return this.midField;
                }
                set
                {
                    this.midField = value;
                }
            }

            /// <remarks/>
            public int spn
            {
                get
                {
                    return this.spnField;
                }
                set
                {
                    this.spnField = value;
                }
            }

            /// <remarks/>
            public int fmi
            {
                get
                {
                    return this.fmiField;
                }
                set
                {
                    this.fmiField = value;
                }
            }

            /// <remarks/>
            public int occurances
            {
                get
                {
                    return this.occurancesField;
                }
                set
                {
                    this.occurancesField = value;
                }
            }

            /// <remarks/>
            public string timestamp
            {
                get
                {
                    return this.timestampField;
                }
                set
                {
                    this.timestampField = value;
                }
            }
        }


    }
}
