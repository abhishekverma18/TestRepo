using System;

namespace ProvisioningPrototype
{
    public class CookieJar
    {
        public string AspNetSessionId { get; set; }
        public string VcAuthentication { get; set; }
        public string MachineId { get; set; }
        public string UniqueRequestId { get; set; }
        public string SourceCode { get; set; }

        public CookieJar()
        {
            AspNetSessionId = String.Empty;
            VcAuthentication = String.Empty;
            MachineId = String.Empty;
            UniqueRequestId = String.Empty;
            SourceCode = String.Empty;
        }
    }
}
