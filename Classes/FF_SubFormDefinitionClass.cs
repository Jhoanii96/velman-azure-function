using System;

namespace FastFieldSchemas.Classes
{
    public class FF_SubFormDefinitionClass
    {


        public class RootSubFormDefinition
        {
            public int id { get; set; }
            public string uniqueId { get; set; }
            public string name { get; set; }
            public int accountId { get; set; }
            public bool isSubForm { get; set; }
            public object isDeleted { get; set; }
            public object reportTemplatePdf { get; set; }
            public Formjson formJson { get; set; }
            public int version { get; set; }
            public object formDisplayMask { get; set; }
            public object emailRecipients { get; set; }
            public object formThumbnail { get; set; }
            public DateTime updatedAt { get; set; }
            public object formConfiguration { get; set; }
            public object subFormConfiguration { get; set; }
            public float locationOutlierFactor { get; set; }
            public bool hideAllEmptyFieldsFromReports { get; set; }
            public bool isDefaultTaskForm { get; set; }
            public string jsonPath { get; set; }
        }

        public class Formjson
        {
            public bool isSubForm { get; set; }
            public int formId { get; set; }
            public int version { get; set; }
            public int accountId { get; set; }
            public string formName { get; set; }
            public Keycounters keyCounters { get; set; }
            public Section[] section { get; set; }
        }

        public class Keycounters
        {
            public int pageKeyCounter { get; set; }
            public int sectionKeyCounter { get; set; }
            public int fieldKeyCounter { get; set; }
        }

        public class Section
        {
            public string sectionHeader { get; set; }
            public string sectionLabel { get; set; }
            public string sectionKey { get; set; }
            public int maxSections { get; set; }
            public Field[] field { get; set; }
        }

        public class Field
        {
            public string fieldKey { get; set; }
            public Subform subForm { get; set; }
            public string fieldType { get; set; }
            public string fieldName { get; set; }
            public bool enabled { get; set; }
            public object value { get; set; }
            public string fieldValueType { get; set; }
            public bool enableMinMaxValues { get; set; }
            public int maxLength { get; set; }
            public string format { get; set; }
            public string keyboard { get; set; }
            public string displayMask { get; set; }
            public int ratingRangeMax { get; set; }
            public bool includeReset { get; set; }
            public string[] ratingLabels { get; set; }
            public bool? captureGeoLocation { get; set; }
            public bool allowMultiSelect { get; set; }
            public bool captureTimeStamp { get; set; }
        }

        public class Subform
        {
        }


    }
}
