using System;

namespace KomatsuAPI
{
    public class FaultsClass
    {

        public class Rootobject
        {
            public Link[] Links { get; set; }
            public Faultcode[] FaultCode { get; set; }
        }

        public class Link
        {
            public string rel { get; set; }
            public string href { get; set; }
        }

        public class Faultcode
        {
            public string CodeIdentifier { get; set; }
            public string CodeDescription { get; set; }
            public string CodeSeverity { get; set; }
            public string CodeSource { get; set; }
            public DateTime datetime { get; set; }
        }

    }
}
