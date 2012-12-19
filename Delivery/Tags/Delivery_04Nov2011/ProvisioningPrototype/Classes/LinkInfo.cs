using System;

namespace ProvisioningPrototype
{
    public class LinkInfo
    {
        public string PortalLink { get; set; }
        public string Surveylink { get; set; }
        public string FolderName { get; set; }

        public LinkInfo()
        {
            PortalLink = String.Empty;
            Surveylink = String.Empty;
            FolderName = String.Empty;
        }
    }
}