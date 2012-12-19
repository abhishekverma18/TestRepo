using System;
using System.IO;
using System.Collections.Generic;
using ProvisioningPrototype.Web_Automation;
using ProvisioningPrototype.Models;
using System.Web;

namespace ProvisioningPrototype.Services
{
    public class AutomationService
    {
        public PxmlManager PxmlManager { get; set; }
        public CookieJar CookieJar { get; set; }
        public string Environment { get; set; }

        public AutomationService()
        {
            PxmlManager = new PxmlManager();
            CookieJar = new CookieJar();
            Environment = String.Empty;
        }

        //Start ---------Added for available context module by Optimus
        public CookieJar GetCookieJar(PanelPreferences preferences)
        {
            CookieJar = Login(preferences);
            CookieJar = Home.HomeViewPostToPanelSettingsManager(CookieJar, preferences);
            return CookieJar;
        }
        public ContextCollection GetContextCollection(PanelPreferences preferences)
        {
            CookieJar = preferences.CookieJar;
            ContextCollection collection;
            try
            {
                if (CookieJar.SourceCode.IndexOf("Settings are locked by VcAdmin") > 0)
                {
                    throw new Exception("Settings are currently locked, navigate away from settings, recycle or wait");
                }
                collection = new ContextCollection(CookieJar.SourceCode);
            }
            catch (Exception e)
            {
                // attempt to Navigate away to attempt not to lock panel settings 
                try
                {
                    PanelSettingsManagement.PanelSettingsPostToAssetManager(CookieJar, preferences);
                }
                catch (Exception) { }
                throw e;
            }

            return collection;
        }

        // Added by Optimus for getting list of unavailable Contexts
        public List<ManagePanelModel> GetUnavailableContextItemsList(PanelPreferences preferences)
        {

            List<ManagePanelModel> items = new List<ManagePanelModel>();
            List<ContextInfo> contextInfoList = new List<ContextInfo>();
            contextInfoList = preferences.ContextCollection.FindUnAvailableContext();
            foreach (var contextInformation in contextInfoList)
            {
                var managePanelModel = new ManagePanelModel();
                managePanelModel.Culture = contextInformation.Culture;
                managePanelModel.Environment = contextInformation.Environment;
                managePanelModel.Name = contextInformation.Name;
                managePanelModel.ContextIndex = contextInformation.ContextIndex;
                managePanelModel.PortalUrl = HttpUtility.UrlDecode(contextInformation.PortalBaseUrl);
                items.Add(managePanelModel);
            }

            return items;
        }

        // Added by Optimus for making selected Context available
        public void MakeContextAvailable(PanelPreferences preferences, int[] contextIndexes)
        {
            var collection = preferences.ContextCollection;
            CookieJar = preferences.CookieJar;
            foreach (var contextIndex in contextIndexes)
            {
                collection.UpdateFormValue(contextIndex, -1, "OpenPortalSkinFolder", String.Empty);
            }
            PanelSettingsManagement.PanelSettingsUpdatePost(CookieJar, collection, preferences);
        }

        public LinkInfo SetUpContext(PanelPreferences preferences, string skinFolderPath)
        {
            // Offline and Advanced mode Changes by Optimus

            var linkInfo = new LinkInfo { FolderName = Res.FolderName };
            if (!preferences.OfflineMode)
            {
                CookieJar = Login(preferences);

                try
                {
                    CookieJar = Home.HomeViewPostToPanelSettingsManager(CookieJar, preferences);  //Source code here contains the Panel Settings Form

                    if (CookieJar.SourceCode.IndexOf("Settings are locked by VcAdmin") > 0)
                    {
                        throw new Exception("Settings are currently locked, navigate away from settings, recycle or wait");
                    }
                    var collection = new ContextCollection(CookieJar.SourceCode);

                    ContextInfo contextInfo = collection.FindAvailableContext();
                    Environment = contextInfo.Environment;

                    collection.UpdateFormValue(contextInfo.ContextIndex, -1, "OpenPortalSkinFolder", contextInfo.FolderName());
                    collection.UpdateFormValue(contextInfo.ContextIndex, -1, "PortalSkinPath", Res.PortalSkinPathParent + "/" + contextInfo.FolderName() + "/");
                    collection.UpdateFormValue(contextInfo.ContextIndex, -1, "IsHidden", "False");
                    collection.UpdateFormValue(contextInfo.ContextIndex, -1, "Name", preferences.CompanyName + " Portal");

                    // Added for Language selection by Optimus
                    collection.UpdateFormValue(contextInfo.ContextIndex, -1, "Culture", preferences.Language);

                    PanelSettingsManagement.PanelSettingsUpdatePost(CookieJar, collection, preferences);
                    linkInfo.FolderName = contextInfo.FolderName();
                    linkInfo.PortalLink = contextInfo.OpenPortalTestBaseUrl; //TODO CHANGE TO LIVE LINK   
                }
                catch (Exception e)
                {
                    // attempt to Navigate away to attempt not to lock panel settings 
                    try
                    {
                        PanelSettingsManagement.PanelSettingsPostToAssetManager(CookieJar, preferences);
                    }
                    catch (Exception) { }
                    throw e;
                }
            }
            return linkInfo;
        }

        private CookieJar Login(PanelPreferences preferences)
        {
            var viewState = Authorization.GetViewState(preferences);
            CookieJar = Authorization.AuthenticationPost(viewState, CookieJar, preferences);
            CookieJar = Home.HomeViewGet(CookieJar, preferences);
            return CookieJar;
        }

        public string CreateSurveyTestLink(PanelPreferences preferences, string pqFolderPath)
        {
            CookieJar = AssetManager.AssetManagerPostToHomeView(CookieJar, preferences);
            CookieJar = Home.HomeViewPostToImportNewStudyView(CookieJar, preferences);
            CookieJar = ImportNewStudy.ImportNewPxmlStudy(CookieJar, PxmlManager.GetPxml(pqFolderPath, preferences.QuestionnaireName), preferences);
            CookieJar = StudyStatus.StudyStatusPostToStudyQuestionnaire(CookieJar, preferences);
            CookieJar = StudyQuestionnaire.ValidateStudyPost(CookieJar, preferences);
            CookieJar = StudyQuestionnaire.StudyQuestionnairePostToDeployments(CookieJar, preferences);
            CookieJar = StudyDeployment.DeploymentsPostToAnonymousLinkView(CookieJar, preferences);
            CookieJar = AnonymousLink.UpdateLanguageAndSkinPost(CookieJar, Environment, preferences);
            string testLink = AutomationHelper.ExtractTestLink(CookieJar.SourceCode);
            return testLink;
        }

        public OfflineLinks CreateSkin(PanelPreferences preferences, string skinFolderPath, string dataPath, string newFolderName)
        {
            // TODO the template path shouldn't be under the scratch path where we create 
            //      the packages. For session support the scratch folder should be independent
            // remove the old update folder
            SkinManager manager = new SkinManager(skinFolderPath);
            manager.DeleteUploadFolders();
            manager.CreateDirectoriesAndUnzipFiles(preferences.HdnSelectedLayout);
            manager.UpdateSkin(preferences, dataPath);
            OfflineLinks offlineLinks = manager.CompressNewSkins(newFolderName);
            manager.DeleteUploadFolders();
            return offlineLinks;
        }

        public bool UploadSkins(PanelPreferences preferences, string skinFolderPath)
        {
            string newPortalSkinPath = Path.Combine(skinFolderPath, Res.NewPortalSkinPath);
            string newSurveySkinpath = Path.Combine(skinFolderPath, Res.NewSurveySkinPath);
            CookieJar = UploadSkinsToAssetManager(newPortalSkinPath, newSurveySkinpath, preferences);
            return true;
        }

        private CookieJar UploadSkinsToAssetManager(string newPortalSkinPath, string newSurveySkinPath, PanelPreferences preferences)
        {
            CookieJar = PanelSettingsManagement.PanelSettingsPostToAssetManager(CookieJar, preferences);
            CookieJar = AssetManager.AssetManagerGet(CookieJar, preferences);
            CookieJar = AssetManager.AssetManagerUploadNewPortalSkin(CookieJar, newPortalSkinPath, preferences);
            CookieJar = AssetManager.AssetManagerUploadNewSurveySkin(CookieJar, newSurveySkinPath, preferences);
            CookieJar = AssetManager.AssetManagerDecompressNewPortalSkin(CookieJar, preferences);
            CookieJar = AssetManager.AssetManagerDecompressNewSurveySkin(CookieJar, preferences);
            CookieJar = AssetManager.AssetManagerDeleteNewPortalSkinZip(CookieJar, preferences);
            CookieJar = AssetManager.AssetManagerDeleteNewSurveySkinZip(CookieJar, preferences);
            return CookieJar;
        }
    }
}
