﻿using System;
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
        public void Login(PanelPreferences preferences)
        {
            var viewState = Authorization.GetViewState(preferences);
            CookieJar = Authorization.AuthenticationPost(viewState, CookieJar, preferences);
            preferences.CookieJar = Home.HomeViewGet(CookieJar, preferences);
        }

        public void OpenPanelSettings(PanelPreferences preferences)
        {
            // TODO detect current location and handle more errors
            Home.HomeViewPostToPanelSettingsManager(preferences);

            // Code commented by Optimus as this is always leading to an exception

            /*
              if (CookieJar.SourceCode.IndexOf("Settings are locked by VcAdmin") > 0)
            {
                // avoid locking
                PanelSettingsManagement.PanelSettingsPostToHome(preferences);
                throw new Exception("Settings are currently locked, navigate away from settings, recycle or wait");
            }
              */
        }

        public void ClosePanelSettings(PanelPreferences preferences)
        {
            // TODO error handling potential here.
            PanelSettingsManagement.PanelSettingsPostToHome(preferences);
        }


        // Added by Optimus for making selected Context available
        public void MakeContextAvailable(PanelPreferences preferences, int[] contextIndexes)
        {
            var collection = preferences.ContextCollection;
            foreach (var contextIndex in contextIndexes)
            {
                if (contextIndex != 0)
                {
                    collection.UpdateFormValue(contextIndex, -1, "OpenPortalSkinFolder", String.Empty);
                }
            }
            PanelSettingsManagement.PanelSettingsUpdatePost(collection, preferences);
            PanelSettingsManagement.PanelSettingsPostToHome(preferences);
        }

        public LinkInfo SetUpContext(PanelPreferences preferences, string skinFolderPath)
        {
            // Offline and Advanced mode Changes by Optimus

            var linkInfo = new LinkInfo { FolderName = Res.FolderName };
            if (!preferences.OfflineMode)
            {
                Login(preferences);

                try
                {
                    OpenPanelSettings(preferences);

                    var collection = new ContextCollection(CookieJar.SourceCode);

                    ContextInfo contextInfo = collection.FindAvailableContext();
                    Environment = contextInfo.Environment;

                    collection.UpdateFormValue(contextInfo.ContextIndex, -1, "OpenPortalSkinFolder", contextInfo.FolderName());
                    collection.UpdateFormValue(contextInfo.ContextIndex, -1, "PortalSkinPath", Res.PortalSkinPathParent + "/" + contextInfo.FolderName() + "/");
                    collection.UpdateFormValue(contextInfo.ContextIndex, -1, "IsHidden", "False");
                    collection.UpdateFormValue(contextInfo.ContextIndex, -1, "Name", preferences.CompanyName + " Portal");

                    // Added for Language selection by Optimus
                    collection.UpdateFormValue(contextInfo.ContextIndex, -1, "Culture", preferences.Language);

                    PanelSettingsManagement.PanelSettingsUpdatePost(collection, preferences);
                    linkInfo.FolderName = contextInfo.FolderName();
                    linkInfo.PortalLink = contextInfo.OpenPortalTestBaseUrl; //TODO CHANGE TO LIVE LINK   
                }
                catch (Exception e)
                {
                    // attempt to Navigate away to attempt not to lock panel settings 
                    try
                    {
                        PanelSettingsManagement.PanelSettingsPostToHome(preferences);
                    }
                    catch (Exception) { }
                    throw e;
                }
            }
            return linkInfo;
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

        // Modified  by Khushbu for phase 2 task 'Allow the UI to load previously generated zip files in order to pre-populate the form'.
        // Now this function will return created SkinPackage Link.
        public string CreateSkin(PanelPreferences preferences, string skinFolderPath, string dataPath, string newFolderName)
        {
            // TODO the template path shouldn't be under the scratch path where we create 
            //      the packages. For session support the scratch folder should be independent
            // remove the old update folder
            SkinManager manager = new SkinManager(skinFolderPath);
            manager.DeleteUploadFolders();
            // manager.CreateDirectoriesAndUnzipFiles(preferences.HdnSelectedLayout);
            manager.CreateDirectoriesAndUnzipFiles(preferences.HdnSelectedLayoutName); // Modified by khushbu for phase 2 task.now operation  will be performed based on Template name rather than index.

            manager.UpdateSkin(preferences, dataPath);

            manager.UpdateSubstitutionFile(preferences); //Added by khushbu for phase 2 task. update the sbstitution file according to user selected value .

            string SkinPackageLink = manager.CompressNewSkins(newFolderName); // Added by khushbu for phase 2 task. Returned only one link for created pacakage.
            // Copy updated skinZips to Created Package Folder


            manager.DeleteUploadFolders();
            return SkinPackageLink;
        }

        // Added by Optimus for 'Allow the UI to load previously generated zip files in order to pre-populate the form ' module
        public bool ExtractPreviouslyGeneratedSkinPackageZip(string skinFolderPath)
        {
            SkinManager manager = new SkinManager(skinFolderPath);
            manager.CreateSkinPackageDirectoryAndUnzipFiles();
            return true;
        }

        public bool UploadSkins(PanelPreferences preferences, string skinFolderPath)
        {
            string newPortalSkinPath = Path.Combine(skinFolderPath, Res.NewPortalSkinPath);
            string newSurveySkinpath = Path.Combine(skinFolderPath, Res.NewSurveySkinPath);
            // Added by Khushbu for phase 2 task 'Allow the UI to load previously generated zip files in order to pre-populate the form'.
            // Now created skinPackage will also be uploaded on Sparq.
            string skinPackagePath = Path.Combine(skinFolderPath, Res.NewSkinPackagePath);
            CookieJar = UploadSkinsToAssetManager(newPortalSkinPath, newSurveySkinpath, skinPackagePath, preferences);
            return true;
        }

        // Modified by Khushbu for phase 2 task 'Allow the UI to load previously generated zip files in order to pre-populate the form'.
        // Now created skinPackage will also be uploaded on Sparq.
        private CookieJar UploadSkinsToAssetManager(string newPortalSkinPath, string newSurveySkinPath, string skinPackagePath, PanelPreferences preferences)
        {
            PanelSettingsManagement.PanelSettingsPostToAssetManager(preferences);
            CookieJar = AssetManager.AssetManagerGet(CookieJar, preferences);
            CookieJar = AssetManager.AssetManagerUploadNewPortalSkin(CookieJar, newPortalSkinPath, preferences);
            CookieJar = AssetManager.AssetManagerUploadNewPortalSkin(CookieJar, skinPackagePath, preferences);
            CookieJar = AssetManager.AssetManagerUploadNewSurveySkin(CookieJar, newSurveySkinPath, preferences);
            CookieJar = AssetManager.AssetManagerDecompressNewPortalSkin(CookieJar, preferences);
            CookieJar = AssetManager.AssetManagerDecompressNewSurveySkin(CookieJar, preferences);
            CookieJar = AssetManager.AssetManagerDeleteNewPortalSkinZip(CookieJar, preferences);
            CookieJar = AssetManager.AssetManagerDeleteNewSurveySkinZip(CookieJar, preferences);
            return CookieJar;
        }


    }
}
