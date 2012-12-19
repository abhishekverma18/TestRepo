using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProvisioningPrototype.Models
{
    public class PreferencesModel
    {
        //Added for Language selection 
        private readonly List<SelectListItem> _languages = new List<SelectListItem>();

        [Required]
        [Display(Name = "Questionnaire")]
        public string QuestionnaireId { get; set; }
        public IEnumerable<SelectListItem> QuestionnaireSelectList { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Contact Email")]
        public string ContactEmail { get; set; }

        //Added for Language selection 
        [Required]
        [Display(Name = "Language")]
        public string Language { get; set; }

        public List<SelectListItem> LanguageSelectList
        {
            get
            {
                _languages.Add(new SelectListItem() { Text = @"en-CA", Value = "en-CA" });
                _languages.Add(new SelectListItem() { Text = @"fr-CA", Value = "fr-CA" });
                return _languages;
            }
        }


        [Required]
        [Display(Name = "LayoutCount")]
        public string HdnLayoutCount { get; set; }

        // Added by Khushbu for phase2 task 'Allow the UI to load previously generated zip files in order to pre-populate the form'.
        [Required]
        [Display(Name = "SelectedLayoutName")]
        public string HdnSelectedLayoutName { get; set; }

        [Required]
        [Display(Name = "SelectedLayout")]
        public string HdnSelectedLayout { get; set; }

        //Added for offline and Advanced mode 

        [Display(Name = "PanelAdminEmail")]
        public string PanelAdminEmail { get; set; }


        [Display(Name = "PanelPassword")]
        public string PanelPassword { get; set; }


        [Display(Name = "PanelAdminUrl")]
        public string PanelAdminUrl { get; set; }

        [Required]
        [Display(Name = "OfflineMode")]
        public bool OfflineMode { get; set; }

        [Display(Name = "Counter")]
        public int Counter { get; set; }
    }
       
    public class TestLinkModel
    {
        [Display(Name = "Portal Link")]
        public string PortalLink { get; set; }

        [Display(Name = "Survey Link")]
        public string SurveyLink { get; set; }

        // Added by Khushbu for phase2 task 'Allow the UI to load previously generated zip files in order to pre-populate the form'.
        [Display(Name = "Skin Package Link")]
        public string SkinPackageLink { get; set; }
    }     

}