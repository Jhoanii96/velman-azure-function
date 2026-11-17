using System;

namespace FastFieldSchemas.Classes
{
    class FF_AuthenticateClass
    {

        public class RootAuthenticate
        {
            public string sessionToken { get; set; }
            public DateTime sessionExpiration { get; set; }
        }

    }
}
