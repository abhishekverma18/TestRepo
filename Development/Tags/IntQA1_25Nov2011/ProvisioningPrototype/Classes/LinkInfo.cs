using System;

namespace ProvisioningPrototype
{
    public class LinkInfo
    {
        public string PortalLink { get; set; }
        public string Surveylink { get; set; }
        public string SkinPackageLink { get; set; } //Added by Khushbu for phase2 tasks: To create a SkinPackage Link 
        public string FolderName { get; set; }

        public LinkInfo()
        {
            PortalLink = String.Empty;
            Surveylink = String.Empty;
            SkinPackageLink = String.Empty;
            FolderName = String.Empty;
        }
    }
}