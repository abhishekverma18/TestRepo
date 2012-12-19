using System;
using System.IO;
using ProvisioningPrototype.Web_Automation;
using System.Collections.Generic;

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
        public ContextCollection GetContextCollection(CookieJar cookieJar)
        {
            var collection = new ContextCollection(CookieJar.SourceCode);
            return collection;
        }
        public List<ContextList> GetAllContextItemsList(PanelPreferences preferences)
        {

            var items = new List<ContextList>();

            foreach (var context in preferences.ContextCollection.ContextList)
            {

                ContextInfo contextInformation = preferences.ContextCollection.GetContextInformation(context.ContextItems[4].PanelSettings, context.ContextItems[2].PanelSettings, context.ContextIndex);
                string[] portalBaseUrl = contextInformation.PortalBaseUrl.Split('.');
                var contextUrl = String.Format("{0}.{1}", contextInformation.SubDomain, portalBaseUrl[1]);
                if (contextInformation.OpenPortalSkinFolder.Equals(string.Empty))
                {
                    var item = new ContextList(contextUrl + "  Available", contextInformation);
                    items.Insert(0, item);
                }
                else
                {
                    var item = new ContextList(contextUrl + "  " + contextInformation.Name, contextInformation);
                    items.Add(item);
                }
            }
            return items;
        }

        public ContextInfo GetContextDetails(string availableContextValues)
        {
            string[] contextValues = availableContextValues.Split(',');
            var contextInfo = new ContextInfo
                                  {
                                      ContextIndex = Convert.ToInt32(contextValues[0]),
                                      Culture = contextValues[1],
                                      Environment = contextValues[2],
                                      OpenPortalLiveBaseUrl = contextValues[3],
                                      SubDomain = contextValues[4],
                                      OpenPortalTestBaseUrl = contextValues[5]
                                  };
            return contextInfo;
        }

        //End ---------Added for Available context module by Optimus

        public LinkInfo SetUpContext(PanelPreferences preferences, string skinFolderPath)
        {
            // Offline and Advanced mode Changes by Optimus

            var linkInfo = new LinkInfo { FolderName = Res.FolderName };
            if (!preferences.OfflineMode)
            {
                /* Code commented by Optimus
                 CookieJar = Login(preferences);
                CookieJar = Home.HomeViewPostToPanelSettingsManager(CookieJar, preferences);  //Source code here contains the Panel Settings Form
                var collection = new ContextCollection(CookieJar.SourceCode);
                 */
                CookieJar = preferences.CookieJar;
                var collection = preferences.ContextCollection;
                var contextInfo = GetContextDetails(preferences.AvailableContextValues);
                Environment = contextInfo.Environment;

                collection.UpdateFormValue(contextInfo.ContextIndex, -1, "OpenPortalSkinFolder", contextInfo.SubDomain);
                collection.UpdateFormValue(contextInfo.ContextIndex, -1, "PortalSkinPath", Res.PortalSkinPathParent + "/" + contextInfo.SubDomain + "/");
                collection.UpdateFormValue(contextInfo.ContextIndex, -1, "IsHidden", "False");
                collection.UpdateFormValue(contextInfo.ContextIndex, -1, "Name", preferences.CompanyName + " Portal");

                // Added for Language selection by Optimus
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

        public OfflineLinks CreateSkin(PanelPreferences preferences, string skinFolderPath, string dataPath, string newFolderName)
        {
            SkinManager.CopyGenericFilesToUploadFolders(preferences.HdnSelectedLayout, skinFolderPath);
            SkinManager.ExtractUploadFolderZipFiles(skinFolderPath);
            SkinManager.DeleteUnusedZipFiles(skinFolderPath);
            SkinManager.UpdateSkin(preferences, skinFolderPath, dataPath);
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
