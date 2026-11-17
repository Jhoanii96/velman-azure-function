namespace KomatsuAPI.XML
{
    public class KomatsuAPIClass
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class SettingApi
        {

            private string aPINameField;

            private string urlRootField;

            private string urlBaseTimeSeriesField;

            private string komatsuIdField;

            private string schemaTableField;

            private string createTableField;

            private string startDateField;

            private string endDateField;

            private SettingApiAPISettings aPISettingsField;

            private SettingApiSQLSettings sQLSettingsField;

            /// <remarks/>
            public string APIName
            {
                get
                {
                    return this.aPINameField;
                }
                set
                {
                    this.aPINameField = value;
                }
            }

            /// <remarks/>
            public string UrlRoot
            {
                get
                {
                    return this.urlRootField;
                }
                set
                {
                    this.urlRootField = value;
                }
            }

            /// <remarks/>
            public string UrlBaseTimeSeries
            {
                get
                {
                    return this.urlBaseTimeSeriesField;
                }
                set
                {
                    this.urlBaseTimeSeriesField = value;
                }
            }

            /// <remarks/>
            public string KomatsuId
            {
                get
                {
                    return this.komatsuIdField;
                }
                set
                {
                    this.komatsuIdField = value;
                }
            }

            /// <remarks/>
            public string SchemaTable
            {
                get
                {
                    return this.schemaTableField;
                }
                set
                {
                    this.schemaTableField = value;
                }
            }

            /// <remarks/>
            public string CreateTable
            {
                get
                {
                    return this.createTableField;
                }
                set
                {
                    this.createTableField = value;
                }
            }

            /// <remarks/>
            public string StartDate
            {
                get
                {
                    return this.startDateField;
                }
                set
                {
                    this.startDateField = value;
                }
            }

            /// <remarks/>
            public string EndDate
            {
                get
                {
                    return this.endDateField;
                }
                set
                {
                    this.endDateField = value;
                }
            }

            /// <remarks/>
            public SettingApiAPISettings APISettings
            {
                get
                {
                    return this.aPISettingsField;
                }
                set
                {
                    this.aPISettingsField = value;
                }
            }

            /// <remarks/>
            public SettingApiSQLSettings SQLSettings
            {
                get
                {
                    return this.sQLSettingsField;
                }
                set
                {
                    this.sQLSettingsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SettingApiAPISettings
        {

            private string authTypeField;

            private string usernameField;

            private string passwordField;

            private string tokenNameField;

            private string tokenUrlField;

            private string grantTypeField;

            private object clientIdField;

            private object clientSecretField;

            private string headerPrefixField;

            /// <remarks/>
            public string AuthType
            {
                get
                {
                    return this.authTypeField;
                }
                set
                {
                    this.authTypeField = value;
                }
            }

            /// <remarks/>
            public string Username
            {
                get
                {
                    return this.usernameField;
                }
                set
                {
                    this.usernameField = value;
                }
            }

            /// <remarks/>
            public string Password
            {
                get
                {
                    return this.passwordField;
                }
                set
                {
                    this.passwordField = value;
                }
            }

            /// <remarks/>
            public string TokenName
            {
                get
                {
                    return this.tokenNameField;
                }
                set
                {
                    this.tokenNameField = value;
                }
            }

            /// <remarks/>
            public string TokenUrl
            {
                get
                {
                    return this.tokenUrlField;
                }
                set
                {
                    this.tokenUrlField = value;
                }
            }

            /// <remarks/>
            public string GrantType
            {
                get
                {
                    return this.grantTypeField;
                }
                set
                {
                    this.grantTypeField = value;
                }
            }

            /// <remarks/>
            public object ClientId
            {
                get
                {
                    return this.clientIdField;
                }
                set
                {
                    this.clientIdField = value;
                }
            }

            /// <remarks/>
            public object ClientSecret
            {
                get
                {
                    return this.clientSecretField;
                }
                set
                {
                    this.clientSecretField = value;
                }
            }

            /// <remarks/>
            public string HeaderPrefix
            {
                get
                {
                    return this.headerPrefixField;
                }
                set
                {
                    this.headerPrefixField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SettingApiSQLSettings
        {

            private string servernameField;

            private string databaseField;

            private string loginField;

            private string passwordField;

            /// <remarks/>
            public string Servername
            {
                get
                {
                    return this.servernameField;
                }
                set
                {
                    this.servernameField = value;
                }
            }

            /// <remarks/>
            public string Database
            {
                get
                {
                    return this.databaseField;
                }
                set
                {
                    this.databaseField = value;
                }
            }

            /// <remarks/>
            public string Login
            {
                get
                {
                    return this.loginField;
                }
                set
                {
                    this.loginField = value;
                }
            }

            /// <remarks/>
            public string Password
            {
                get
                {
                    return this.passwordField;
                }
                set
                {
                    this.passwordField = value;
                }
            }
        }


    }
}
