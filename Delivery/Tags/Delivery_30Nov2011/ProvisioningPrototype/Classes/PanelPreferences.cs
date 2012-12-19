using System.Collections.Generic;

namespace ProvisioningPrototype
{
    public class PanelPreferences
    {
        public string QuestionnaireName { get; set; }
        public string CompanyName { get; set; }
        public string ContactEmail { get; set; }
        public bool QuickPollVisible { get; set; }
        public bool NewsletterVisible { get; set; }

        //Added for Dynamic Template List
        public bool HdnLayoutCount { get; set; }
        public string HdnSelectedLayout { get; set; }

        // Added by khushbu for phase 2 task: To select template based on Template Name
        public string HdnSelectedLayoutName { get; set; }

        //Added for Offline and Advanced Mode
        public string PanelAdminEmail { get; set; }
        public string PanelPassword { get; set; }
        public string PanelAdminUrl { get; set; }
        public bool OfflineMode { get; set; }

        //Added for Language selection
        public string Language { get; set; }

        // Added for dynamically added GUI fields in Appearance section
        public IDictionary<string, string> DynamicGuiVariables { get; set; }

        // Added to keep track of the current Template being processed
        public GuiTemplate CurrentGuiTemplate { get; set; }

        //Added for Available Context
        public CookieJar CookieJar { get; set; }
        public ContextCollection ContextCollection { get; set; }
    }
}
