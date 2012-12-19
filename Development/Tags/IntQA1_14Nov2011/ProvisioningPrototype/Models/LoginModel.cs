using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace ProvisioningPrototype.Models
{
    //Added for offline and Advanced mode 
    public class LoginModel
    {
        private string panelAdminEmail = null;
        private string panelPassword = null;
        private string panelAdminUrl = null;

        [Required(ErrorMessage = @"Panel admin email is required")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = @"Email Format is wrong")]
        [Display(Name = "PanelAdminEmail")]
        public string PanelAdminEmail {
            get
            {
                if (this.panelAdminEmail == null)
                {
                    string @default = ConfigurationManager.AppSettings["PanelAdminEmail"];
                    if (@default != null)
                    {
                        return @default;
                    }
                }
                return this.panelAdminEmail;

            }
            set { this.panelAdminEmail = value; } 
        }

        [Required(ErrorMessage = @"Panel password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "PanelPassword")]
        public string PanelPassword { 
            get
            {
                if (this.panelPassword == null)
                {
                    string @default = ConfigurationManager.AppSettings["panelPassword"];
                    if (@default != null)
                    {
                        return @default;
                    }
                }
                return this.panelPassword;

            }
            set { this.panelPassword = value; } 
        }

        [Required(ErrorMessage = @"Panel admin url is required")]
        [RegularExpression(@"((https?|ftp|gopher|telnet|file|notes|ms-help):((//)|(\\\\))+[\w\d:#@%/;$()~_?\+-=\\\.&]*)", ErrorMessage = @"Url Format is wrong")]
        [Display(Name = "PanelAdminUrl")]
        public string PanelAdminUrl {
            get
            {
                if (this.panelAdminUrl == null)
                {
                    string @default = ConfigurationManager.AppSettings["panelAdminUrl"];
                    if (@default != null)
                    {
                        return @default;
                    }
                }
                return this.panelAdminUrl;

            }
            set { this.panelAdminUrl = value; }
        }

        [Required]
        [Display(Name = "OfflineMode")]
        public bool OfflineMode { get; set; }
    }
}
