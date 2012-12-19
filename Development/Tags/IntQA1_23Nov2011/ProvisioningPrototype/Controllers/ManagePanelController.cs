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
                
                _automationService.Login(preferences);
                _automationService.OpenPanelSettings(preferences);

                preferences.ContextCollection = new ContextCollection(preferences.CookieJar.SourceCode);

                // need to unlock panel settings now just in case the browser is closed or the user takes no 
                // further action
                _automationService.ClosePanelSettings(preferences);

                var list = GetContextItemsList(preferences);
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


        // Added by Optimus for getting list of unavailable Contexts
        // cw: moved out of _automationService as no automation involved... 
        public List<ManagePanelModel> GetContextItemsList(PanelPreferences preferences)
        {

            List<ManagePanelModel> items = new List<ManagePanelModel>();
            List<ContextInfo> contextInfoList = new List<ContextInfo>();
            contextInfoList = preferences.ContextCollection.GetAllContextInfo();
            foreach (var contextInformation in contextInfoList)
            {
                var managePanelModel = new ManagePanelModel();
                managePanelModel.Culture = contextInformation.Culture;
                managePanelModel.Available = contextInformation.OpenPortalSkinFolder == String.Empty;
                managePanelModel.Environment = contextInformation.Environment;
                managePanelModel.Name = contextInformation.Name;
                managePanelModel.ContextIndex = contextInformation.ContextIndex;
                managePanelModel.PortalUrl = HttpUtility.UrlDecode(contextInformation.PortalBaseUrl) + "PortalStaging/" + contextInformation.OpenPortalSkinFolder;
                items.Add(managePanelModel);
            }

            return items;
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

                _automationService.OpenPanelSettings(preferences);
                _automationService.MakeContextAvailable(preferences, selectedIndex);
                contextInformationList = GetContextItemsList(preferences);
                _automationService.ClosePanelSettings(preferences); // clean up and unlock when finished

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
