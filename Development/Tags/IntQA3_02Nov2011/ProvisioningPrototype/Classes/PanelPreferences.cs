using System.Collections.Generic;

namespace ProvisioningPrototype
{
    public class PanelPreferences
    {
        public string QuestionnaireName { get; set; }
        public string CompanyName { get; set; }
        public string ContactEmail { get; set; }

        //---- Code commented by Optimus : Code was used for the static controls on the web page--- 
        /*
        public string PageBackgroundHexCode { get; set; }
        public string ContentBackgroundHexCode { get; set; }

        public string PrimaryTextHexCode { get; set; }
        public string SecondaryTextHexCode { get; set; }

        public string HeaderFileName { get; set; }
        public string LogoFileName { get; set; }
        */

        public bool QuickPollVisible { get; set; }
        public bool NewsletterVisible { get; set; }

        //Added for Dynamic Template List
        public bool HdnLayoutCount { get; set; }
        public string HdnSelectedLayout { get; set; }

        //Added for Offline and Advanced Mode
        public string PanelAdminEmail { get; set; }
        public string PanelPassword { get; set; }
        public string PanelAdminUrl { get; set; }
        public bool OfflineMode { get; set; }

        //Added by Khushbu:Language selection
        public string Language { get; set; }

        // Added for dynamically added GUI fields in Appearance section
        public IDictionary<string, string> DynamicGuiVariables { get; set; }

        // Added to keep track of the current Template being processed
        public GuiTemplate CurrentGuiTemplate { get; set; }
    }
}
