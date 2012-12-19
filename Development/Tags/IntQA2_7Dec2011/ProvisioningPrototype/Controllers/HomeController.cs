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

            preferencesModel.PanelAdminUrl = loginPreferences.PanelAdminUrl;
            preferencesModel.PanelAdminEmail = loginPreferences.PanelAdminEmail;
            preferencesModel.PanelPassword = loginPreferences.PanelPassword;
            preferencesModel.OfflineMode = loginPreferences.OfflineMode;

            // Clear session variable by Optimus
            Session[Res.SessionTemplates] = null;

            return View(preferencesModel);
        }

        // Added By Khushbu For 'Allow the UI to load previously generated zip files in order to pre-populate the form' Module



        public JsonResult LoadFromZip(int templateId, string qqfile)
        {
            byte[] fileData = null;
            using (var reader = new BinaryReader(Request.InputStream))
            {

                // This will contain the uploaded file data and the qqfile the name

                fileData = reader.ReadBytes((int)Request.InputStream.Length);

            }
            var path = Path.Combine(Server.MapPath(Res.UploadsDirectoryPath), qqfile);

            System.IO.File.WriteAllBytes(path, fileData);


            if (Session[Res.SessionTemplates] == null)
            {
                Session[Res.SessionTemplates] = TemplateManager.Instance.LoadTemplates();
            }
            var temps = (DynamicGuiTemplates)Session[Res.SessionTemplates];
            if (!_automationService.ExtractPreviouslyGeneratedSkinPackageZip(Server.MapPath(Res.SkinsDirectoryPath), path))
            {
                temps.GuiTemplates[templateId].SelectedTemplate = "-2";
                return Json(temps.GuiTemplates[templateId], JsonRequestBehavior.AllowGet);
            }

            var prevTemplate = TemplateManager.Instance.LoadPrevTemp();
            for (int i = 0; i < temps.GuiTemplates.Length; i++)
            {
                if (prevTemplate.TemplateName.Equals(temps.GuiTemplates[i].TemplateName))
                {
                    prevTemplate.SelectedTemplate = (i + 1).ToString();
                    return Json(prevTemplate, "application/json", JsonRequestBehavior.AllowGet);

                }
            }

            temps.GuiTemplates[templateId].SelectedTemplate = "-1";
            return Json(temps.GuiTemplates[templateId], JsonRequestBehavior.AllowGet);
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

        public ActionResult SaveAndContinue(FormCollection formCollection, PreferencesModel model, IEnumerable<HttpPostedFileBase> postedFiles)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var preferences = new PanelPreferences
                                          {
                                              HdnSelectedLayout = model.HdnSelectedLayout,
                                              HdnSelectedLayoutName = model.HdnSelectedLayoutName, // Added by Khushbu For pahse2 task 'Allow the UI to load previously generated zip files in order to pre-populate the form'.
                                              QuestionnaireName = model.QuestionnaireId,
                                              CompanyName = model.CompanyName,
                                              ContactEmail = model.ContactEmail,
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
                        if (group.Variables == null) continue;
                        foreach (GuiVariable guiVar in group.Variables)
                        {
                            // Modified by K.G(07-12-2011) to support file upload controlS for all the groups.
                            switch (guiVar.UiComponent)
                            {
                                case Constants.FileComponent:
                                    preferences.DynamicGuiVariables.Add(guiVar.ComponentName,
                                                                        formCollection["Hdn" + guiVar.ComponentName]);
                                    break;
                                case Constants.ChoiceComponent:
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
                    foreach (HttpPostedFileBase file in postedFiles)
                    {
                        var path = Path.Combine(Server.MapPath(Res.UploadsDirectoryPath), Path.GetFileName(file.FileName)); // Modified by Khushbu For pahse2 task 'Support upload/processing of non-image files (e.g. pdf)'.
                        file.SaveAs(path);
                    }

                    var linkInfo = _automationService.SetUpContext(preferences, Server.MapPath(Res.SkinsDirectoryPath));

                    //Code modified by Optimus
                    var SkinPackageLink = _automationService.CreateSkin(preferences, Server.MapPath(Res.SkinsDirectoryPath),
                                                                              Server.MapPath(Res.DataDirectoryPath), linkInfo.FolderName);

                    var testLinkModel = new TestLinkModel
                                            {
                                                SkinPackageLink = this.UrlForFile(SkinPackageLink) // Modified by Khushbu For pahse2 task 'Allow the UI to load previously generated zip files in order to pre-populate the form'.

                                            };

                    // Code to be executed while in Advanced mode by Optimus
                    if (!preferences.OfflineMode)
                    {

                        // Added by K.G(07/12/11) to upload email templates on Sparq
                        _automationService.UploadEmailTemplates(preferences, Server.MapPath(Res.SkinsDirectoryPath),
                                                                             Server.MapPath(Res.DataDirectoryPath), linkInfo.FolderName);
                        _automationService.UploadSkins(preferences, Server.MapPath(Res.SkinsDirectoryPath), linkInfo.FolderName);

                        linkInfo.Surveylink = _automationService.CreateSurveyTestLink(preferences,
                                                                                      Server.MapPath(Res.PQsDirectoryPath));
                        ViewData["PreferencesModel.QuestionnaireId"] = new SelectList(_automationService.PxmlManager.GetPxmlList(Server.MapPath(Res.PQsDirectoryPath)), "PxmlName", "PxmlName");

                        testLinkModel.PortalLink = HttpUtility.UrlDecode(linkInfo.PortalLink) +
                                                         HttpUtility.UrlDecode(linkInfo.FolderName);
                        testLinkModel.SurveyLink = linkInfo.Surveylink;
                    }

                    // Added By K.G(25/11/2011) to delete extra zip files.
                    _automationService.DeleteUnusedZipFiles(Server.MapPath(Res.SkinsDirectoryPath));

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

        private string UrlForFile(string filePath)
        {
            string appPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
            string url = filePath.Substring(appPath.Length).Replace('\\', '/').Insert(0, "/");
            return (url);
        }
    }
}
