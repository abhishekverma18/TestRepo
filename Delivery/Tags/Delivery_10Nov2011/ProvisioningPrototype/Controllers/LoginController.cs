using System.Web.Mvc;
using ProvisioningPrototype.Models;
using ProvisioningPrototype;

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
            var loginPreferences = new LoginPreferences
            {
                OfflineMode = model.OfflineMode,
                PanelAdminEmail = model.PanelAdminEmail,
                PanelAdminUrl = model.PanelAdminUrl,
                PanelPassword = model.PanelPassword
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
            var loginPreferences = new LoginPreferences
            {
                OfflineMode = model.OfflineMode,
                PanelAdminEmail = model.PanelAdminEmail,
                PanelAdminUrl = model.PanelAdminUrl,
                PanelPassword = model.PanelPassword
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
