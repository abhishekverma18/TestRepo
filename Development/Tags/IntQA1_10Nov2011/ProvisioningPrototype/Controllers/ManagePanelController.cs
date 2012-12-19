using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProvisioningPrototype.Models;
using ProvisioningPrototype.Services;

namespace ProvisioningPrototype.Controllers
{
    public class ManagePanelController : Controller
    {
        //
        // GET: /ManagePanel/
        private readonly AutomationService _automationService;

        public ManagePanelController()
        {
            _automationService = new AutomationService();

        }
        public ActionResult Index(LoginPreferences loginPreferences, string a)
        {
            try
            {
                List<ManagePanelModel> contextInformationList = new List<ManagePanelModel>();
                var preferences = new PanelPreferences();
                preferences.PanelAdminEmail = loginPreferences.PanelAdminEmail;
                preferences.PanelAdminUrl = loginPreferences.PanelAdminUrl;
                preferences.PanelPassword = loginPreferences.PanelPassword;
                preferences.CookieJar = _automationService.GetCookieJar(preferences);
                preferences.ContextCollection = _automationService.GetContextCollection(preferences);
                var list = _automationService.GetUnavailableContextItemsList(preferences);
                contextInformationList = list;
                Session["ContextCollection"] = preferences.ContextCollection;
                Session["CookieJar"] = preferences.CookieJar;
                Session["PanelAdminUrl"] = loginPreferences.PanelAdminUrl;
                return View(contextInformationList);
            }
            catch (Exception e)
            {
                var errorModel = new ErrorModel { Exception = e };
                return View("Error", errorModel);
            }
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "Back")]
        public ActionResult Back()
        {
            var loginModel = new LoginModel();
            return RedirectToAction("Index", "Login", loginModel);
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "Save")]
        public ActionResult Index(int[] selectedIndex)
        {
            try
            {
                var preferences = new PanelPreferences();
                List<ManagePanelModel> contextInformationList = new List<ManagePanelModel>();
                preferences.ContextCollection = (ContextCollection)Session["ContextCollection"];
                preferences.CookieJar = (CookieJar)Session["CookieJar"];
                preferences.PanelAdminUrl = Session["PanelAdminUrl"].ToString();
                _automationService.MakeContextAvailable(preferences, selectedIndex);
                contextInformationList = _automationService.GetUnavailableContextItemsList(preferences);
                return View(contextInformationList);
            }
            catch (Exception e)
            {
                var errorModel = new ErrorModel { Exception = e };
                return View("Error", errorModel);
            }

        }
    }
}
