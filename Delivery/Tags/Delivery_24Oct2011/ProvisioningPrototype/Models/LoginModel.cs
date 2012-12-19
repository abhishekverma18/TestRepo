using System.ComponentModel.DataAnnotations;

namespace ProvisioningPrototype.Models
{
    //Added for offline and Advanced mode 
    public class LoginModel
    {
        [Required(ErrorMessage = @"Panel admin email is required")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = @"Email Format is wrong")]
        [Display(Name = "PanelAdminEmail")]
        public string PanelAdminEmail { get; set; }

        [Required(ErrorMessage = @"Panel password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "PanelPassword")]
        public string PanelPassword { get; set; }

        [Required(ErrorMessage = @"Panel admin url is required")]
        [RegularExpression(@"((https?|ftp|gopher|telnet|file|notes|ms-help):((//)|(\\\\))+[\w\d:#@%/;$()~_?\+-=\\\.&]*)", ErrorMessage = @"Url Format is wrong")]
        [Display(Name = "PanelAdminUrl")]
        public string PanelAdminUrl { get; set; }

        [Required]
        [Display(Name = "OfflineMode")]
        public bool OfflineMode { get; set; }
    }
}
