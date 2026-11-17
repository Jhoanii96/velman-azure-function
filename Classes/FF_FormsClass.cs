using System;

namespace FastFieldSchemas.Classes
{
    public class FF_FormsClass
    {

        public class RootForms
        {
            public ClassRoot[] Root { get; set; }
        }

        public class ClassRoot
        {
            public int id { get; set; }
            public string uniqueId { get; set; }
            public string name { get; set; }
            public int? accountId { get; set; }
            public bool? isSubForm { get; set; }
            public bool? isDeleted { get; set; }
            public int? version { get; set; }
            public DateTime? updatedAt { get; set; }
        }


    }
}
