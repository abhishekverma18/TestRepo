using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProvisioningPrototype.Models;
using ProvisioningPrototype.Services;
using System.Collections.Generic;

namespace ProvisioningPrototype.Controllers
{
    public class HomeController : Controller
    {
        private readonly AutomationService _automationService;
        public HomeController()
        {
            _automationService = new AutomationService();
        }

        public ActionResult Index(LoginPreferences loginPreferences)
        {
            var preferencesModel = new PreferencesModel();

            var mainDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Content\Skins\Templates");
            int templateCount = mainDir.GetDirectories().Count(dir => (dir.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden);
            preferencesModel.HdnLayoutCount = templateCount.ToString();

            preferencesModel.QuestionnaireSelectList = new SelectList(_automationService.PxmlManager.GetPxmlList(Server.MapPath(Res.PQsDirectoryPath)), "PxmlName", "PxmlName");

            preferencesModel.QuickPollVisible = true;
            preferencesModel.NewsletterVisible = true;

            //---- Code commented by Optimus : Code was used for the static controls on the web page---
            /*
            preferencesModel.PrimaryTextHexCode = "000000";
            preferencesModel.SecondaryTextHexCode = "333333";
            preferencesModel.PageBackgroundHexCode = "FFFFFF";
            preferencesModel.ContentBackgroundHexCode = "FFFFFF";
            */
            preferencesModel.PanelAdminUrl = loginPreferences.PanelAdminUrl;
            preferencesModel.PanelAdminEmail = loginPreferences.PanelAdminEmail;
            preferencesModel.PanelPassword = loginPreferences.PanelPassword;
            preferencesModel.OfflineMode = loginPreferences.OfflineMode;

            // Clear session variable by Optimus
            Session[Res.SessionTemplates] = null;

            return View(preferencesModel);
        }

        //Added for Dynamic Template List module by Optimus
        public ActionResult Template(int id)
        {
            try
            {
                if (Session[Res.SessionTemplates] == null)
                {
                    Session[Res.SessionTemplates] = TemplateManager.Instance.LoadTemplates();
                }

                var temps = (DynamicGuiTemplates)Session[Res.SessionTemplates];
                return Json(temps.GuiTemplates[id], "application/json", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var errorModel = new ErrorModel { Exception = e };
                return View("Error", errorModel);
            }
        }

        [HttpPost]
        public ActionResult SaveAndContinue(FormCollection formCollection, PreferencesModel model, string returnUrl, HttpPostedFileBase headerFile, HttpPostedFileBase logoFile, IEnumerable<HttpPostedFileBase> postedImages)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var preferences = new PanelPreferences
                                          {
                                              HdnSelectedLayout = model.HdnSelectedLayout,
                                              QuestionnaireName = model.QuestionnaireId,
                                              CompanyName = model.CompanyName,
                                              ContactEmail = model.ContactEmail,
                                              //---- Code commented by Optimus : Code was used for the static controls on the web page--- 
                                              /*
                                              PageBackgroundHexCode = model.PageBackgroundHexCode,
                                              ContentBackgroundHexCode = model.ContentBackgroundHexCode,
                                              PrimaryTextHexCode = model.PrimaryTextHexCode,
                                              SecondaryTextHexCode = model.SecondaryTextHexCode,
                                               */
                                              NewsletterVisible = model.NewsletterVisible,
                                              QuickPollVisible = model.QuickPollVisible,
                                              PanelAdminEmail = model.PanelAdminEmail,
                                              PanelAdminUrl = model.PanelAdminUrl,
                                              PanelPassword = model.PanelPassword,
                                              OfflineMode = model.OfflineMode,
                                              Language = model.Language
                                          };


                    // Populate PanelPreferences with the dynamic GUI control values by Optimus
                    if (Session[Res.SessionTemplates] == null)
                    {
                        Session[Res.SessionTemplates] = TemplateManager.Instance.LoadTemplates();
                    }
                    var temps = (DynamicGuiTemplates)Session[Res.SessionTemplates];
                    var currentTemplate = temps.GuiTemplates[model.Counter];

                    // Set the current Template by Optimus
                    preferences.CurrentGuiTemplate = currentTemplate;
                    preferences.DynamicGuiVariables = new Dictionary<string, string>();
                    foreach (GuiVariableGroup group in currentTemplate.VariableGroups)
                    {
                        foreach (GuiVariable guiVar in group.Variables)
                        {

                            switch (group.GroupName)
                            {
                                case Constants.ImagesGroup:
                                    preferences.DynamicGuiVariables.Add(guiVar.ComponentName,
                                                                        formCollection["Hdn" + guiVar.ComponentName]);
                                    break;
                                case Constants.ChoiceGroup:
                                    preferences.DynamicGuiVariables.Add(guiVar.ComponentName,
                                                                        Convert.ToBoolean(
                                                                            formCollection[guiVar.ComponentName]).
                                                                            ToString());
                                    break;
                                default:
                                    preferences.DynamicGuiVariables.Add(guiVar.ComponentName,
                                                                        formCollection[guiVar.ComponentName]);
                                    break;
                            }

                        }
                    }

                    // Added for moving all dynamic upload control images to 'Uploads' folder by Optimus
                    foreach (HttpPostedFileBase imageFile in postedImages)
                    {
                        var path = Path.Combine(Server.MapPath(Res.UploadsDirectoryPath), Path.GetFileName(imageFile.FileName));
                        imageFile.SaveAs(path);
                    }

                    //---- Code commented by Optimus : Code was used for the static controls on the web page--- 
                    /*
                    if (null != headerFile && headerFile.ContentLength > 0)
                    {
                        preferences.HeaderFileName = Path.GetFileName(headerFile.FileName);
                        var path = Path.Combine(Server.MapPath(Res.UploadsDirectoryPath), preferences.HeaderFileName);
                        headerFile.SaveAs(path);
                    }

                    if (null != logoFile && logoFile.ContentLength > 0)
                    {
                        preferences.LogoFileName = Path.GetFileName(logoFile.FileName);
                        var path = Path.Combine(Server.MapPath(Res.UploadsDirectoryPath), preferences.LogoFileName);
                        logoFile.SaveAs(path);
                    }
                     */
                    var linkInfo = _automationService.SetUpContext(preferences,
                                                                          Server.MapPath(Res.SkinsDirectoryPath));

                    //Code modified by Optimus
                    var offlineLinks = _automationService.CreateSkin(preferences, Server.MapPath(Res.SkinsDirectoryPath),
                                                                              Server.MapPath(Res.DataDirectoryPath), linkInfo.FolderName);

                    var testLinkModel = new TestLinkModel
                                            {
                                                PortalLink = offlineLinks.portalLink,
                                                SurveyLink = offlineLinks.surveyLink
                                            };

                    // Code to be executed while in Advanced mode by Optimus
                    if (!preferences.OfflineMode)
                    {
                        _automationService.UploadSkins(preferences, Server.MapPath(Res.SkinsDirectoryPath));
                        linkInfo.Surveylink = _automationService.CreateSurveyTestLink(preferences,
                                                                                      Server.MapPath(Res.PQsDirectoryPath));
                        ViewData["PreferencesModel.QuestionnaireId"] = new SelectList(_automationService.PxmlManager.GetPxmlList(Server.MapPath(Res.PQsDirectoryPath)), "PxmlName", "PxmlName");

                        testLinkModel.PortalLink = HttpUtility.UrlDecode(linkInfo.PortalLink) +
                                                         HttpUtility.UrlDecode(linkInfo.FolderName);
                        testLinkModel.SurveyLink = linkInfo.Surveylink;
                    }
                    return View("Links", testLinkModel);
                }

                ViewData["PreferencesModel.QuestionnaireId"] =
                    new SelectList(_automationService.PxmlManager.GetPxmlList(Server.MapPath(Res.PQsDirectoryPath)),
                                   "PxmlName", "PxmlName");

                return View("Index", model);
            }
            catch (Exception e)
            {
                var errorModel = new ErrorModel { Exception = e };
                return View("Error", errorModel);
            }
        }
    }
}
