using System;

namespace ProvisioningPrototype
{
    public class ContextInfo
    {
        public string OpenPortalSkinFolder { get; set; }
        public string OpenPortalLiveBaseUrl { get; set; }
        public string OpenPortalTestBaseUrl { get; set; }
        public string SubDomain { get; set; }
        public string PortalSkinPath { get; set; }
        public int ContextIndex { get; set; }
        public string Environment { get; set; }
        public string Culture { get; set; }

        public ContextInfo()
        {
            OpenPortalLiveBaseUrl = String.Empty;
            OpenPortalSkinFolder = String.Empty;
            OpenPortalTestBaseUrl = String.Empty;
            SubDomain = String.Empty;
            PortalSkinPath = String.Empty;
            ContextIndex = -1;
            Environment = String.Empty;
            Culture = String.Empty;
        }
    }
}