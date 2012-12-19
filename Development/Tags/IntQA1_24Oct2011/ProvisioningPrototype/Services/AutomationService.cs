using System;
using System.IO;
using ProvisioningPrototype.Web_Automation;

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

        public LinkInfo SetUpContext(PanelPreferences preferences, string skinFolderPath)
        {
            // Offline and Advanced mode Changes
            
            var linkInfo = new LinkInfo {FolderName = Res.FolderName};
            if (!preferences.OfflineMode)
            {
                CookieJar = Login(preferences);

                CookieJar = Home.HomeViewPostToPanelSettingsManager(CookieJar, preferences);  //Source code here contains the Panel Settings Form

                var collection = new ContextCollection(CookieJar.SourceCode);

                ContextInfo contextInfo = collection.FindAvailableContext();
                Environment = contextInfo.Environment;

                collection.UpdateFormValue(contextInfo.ContextIndex, -1, "OpenPortalSkinFolder", contextInfo.SubDomain);
                collection.UpdateFormValue(contextInfo.ContextIndex, -1, "PortalSkinPath", Res.PortalSkinPathParent + "/" + contextInfo.SubDomain + "/");
                collection.UpdateFormValue(contextInfo.ContextIndex, -1, "IsHidden", "False");
                collection.UpdateFormValue(contextInfo.ContextIndex, -1, "Name", preferences.CompanyName + " Portal");
                // Added for Language selection 
                collection.UpdateFormValue(contextInfo.ContextIndex, -1, "Culture", preferences.Language);

                PanelSettingsManagement.PanelSettingsUpdatePost(CookieJar, collection, preferences);
                linkInfo.FolderName = contextInfo.SubDomain;
                linkInfo.PortalLink = contextInfo.OpenPortalTestBaseUrl; //TODO CHANGE TO LIVE LINK   
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

        public OfflineLinks CreateSkin(PanelPreferences preferences, string skinFolderPath, string appDataPath, string newFolderName)
        {
            SkinManager.CopyGenericFilesToUploadFolders(preferences.HdnSelectedLayout, skinFolderPath);
            SkinManager.ExtractUploadFolderZipFiles(skinFolderPath);
            SkinManager.DeleteUnusedZipFiles(skinFolderPath);
            SkinManager.UpdateSkin(preferences, skinFolderPath, appDataPath);
            OfflineLinks offlineLinks = SkinManager.CompressSkinFolder(skinFolderPath, newFolderName);
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
