using System.Collections.Generic;

namespace ProvisioningPrototype
{
    public class GuiVariable
    {
        public string AltText { get; set; }
        public string GuiName { get; set; }
        public string Substitution { get; set; }
        public string PropertyName { get; set; }
        public string Default { get; set; }
        public string UiComponent { get; set; }
        public string ComponentName { get; set; }
        public string ReplacementDirectory { get; set; }
        public string PathToUpload { get; set; } // Added by Khushbu for phase 2 tasks : To make variable definition 
        public List<string> ComponentValueList { get; set; }
    }

    public class GuiVariableGroup
    {
        public string GroupName { get; set; }
        public string GroupLabel { get; set; }
        public IList<GuiVariable> Variables;
    }

    public class GuiTemplate
    {
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string TemplateName { get; set; } //  Added by Khushbu for phase 2 tasks: To get Template Name
        public string SelectedTemplate { get; set; } // Added by Khushbu for phase 2 tasks: To get Template Index
        public IList<GuiVariableGroup> VariableGroups { get; set; }

        // Added by K.G(16-JAN-2012) to complete load from zip functionality
        public string QuestionnaireName { get; set; }
        public string CompanyName { get; set; }
        public string ContactEmail { get; set; }
        public string Language { get; set; }
    }

    public class DynamicGuiTemplates
    {
        public GuiTemplate[] GuiTemplates { get; set; }
    }
}