using System.Web.Mvc;
using ProvisioningPrototype.Models;
using ProvisioningPrototype;
using ProvisioningPrototype.Utility;

namespace ProvisioningPrototype.Controllers
{
    //Added  for Offline mode     
    public class LoginController : Controller
    {
        public LoginController()
        {
        }

        public ActionResult Index()
        {
            // provide default values
            var loginModel = new LoginModel();
            return View("Index", loginModel);
        }



        [HttpPost]
        [MultiButton(MatchFormKey = "ManagePanel")]
        public ActionResult ManagePanel(LoginModel model)
        {
            string encryptedPassword = PasswordUtill.Encrypt(model.PanelPassword);
            var loginPreferences = new LoginPreferences
            {
                OfflineMode = model.OfflineMode,
                PanelAdminEmail = model.PanelAdminEmail,
                PanelAdminUrl = model.PanelAdminUrl,
                PanelPassword = encryptedPassword
            };
            if (model.OfflineMode == false)
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Index", "ManagePanel", loginPreferences);
                }
                return View("Index");
            }
            return View("Index");
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "CreatePanel")]
        public ActionResult CreatePanel(LoginModel model)
        {
            string encryptedPassword = PasswordUtill.Encrypt(model.PanelPassword);
            var loginPreferences = new LoginPreferences
            {
                OfflineMode = model.OfflineMode,
                PanelAdminEmail = model.PanelAdminEmail,
                PanelAdminUrl = model.PanelAdminUrl,
                PanelPassword = encryptedPassword
            };

            if (model.OfflineMode == false)
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Index", "Home", loginPreferences);
                }
                return View("Index");
            }
            return RedirectToAction("Index", "Home", loginPreferences);
        }
    }
}
