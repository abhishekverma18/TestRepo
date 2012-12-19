using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace ProvisioningPrototype
{
    public class SkinManager
    {
        private DirectoryInfo skinRootDir;
        private DirectoryInfo portalUploadDir;
        private DirectoryInfo surveyUploadDir;
        private DirectoryInfo templatesDir;
        private DirectoryInfo skinPackageUploadDir;

        private DirectoryInfo communityUploadDir;

        public SkinManager(String skinRootDir)
        {
            this.skinRootDir = new DirectoryInfo(skinRootDir);
            this.portalUploadDir = new DirectoryInfo(Path.Combine(skinRootDir, Res.PortalSkinToUploadPath));
            this.surveyUploadDir = new DirectoryInfo(Path.Combine(skinRootDir, Res.SurveySkinToUploadPath));
            this.templatesDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + Res.LocalTemplatePath);
            this.skinPackageUploadDir = new DirectoryInfo(Path.Combine(skinRootDir, Res.SkinPackageToUploadPath));

            this.communityUploadDir = new DirectoryInfo(Path.Combine(skinRootDir, Res.CommunitySkinToUploadPath)); // Added by K.G.(24-11-2011) To Support a third zip package called CommunitySkin.zip
        }


        public void DeleteUploadFolders()
        {
            try
            {
                portalUploadDir.Delete(true);
            }
            catch (System.IO.DirectoryNotFoundException) { }
            try
            {
                surveyUploadDir.Delete(true);
            }
            catch (System.IO.DirectoryNotFoundException) { }
            try
            {
                skinPackageUploadDir.Delete(true);
            }
            catch (System.IO.DirectoryNotFoundException) { }


            try
            {
                communityUploadDir.Delete(true);
            }
            catch (System.IO.DirectoryNotFoundException) { }

        }

        // Added by K.G(25/11/2011) to delete extra file after uploading on the Sparq panel.
        public void DeleteExtraZipFiles(String skinRootDir)
        {
            FileInfo portalZip = new FileInfo(Path.Combine(skinRootDir, "NewPortalSkin.zip"));
            portalZip.Delete();
            FileInfo surveyZip = new FileInfo(Path.Combine(skinRootDir, "NewSurveySkin.zip"));
            surveyZip.Delete();
            FileInfo communityZip = new FileInfo(Path.Combine(skinRootDir, "NewCommunitySkin.zip"));
            communityZip.Delete();
        }

        // Code modified for Dynamic GUI template by Optimus
        // TODO this shouldn't rely on enumeration order being consistent - selection should be 
        //  the folder name in case UI order doesn't match "EnumerateDirectories" order. 
        public bool CreateDirectoriesAndUnzipFiles(string selectedLayoutName)
        {
            // Modified by K.G. for Phase 2 Tasks now the template will be selected by name rather than index.
            var templateDirPath = string.Empty;
            foreach (var subDir in templatesDir.EnumerateDirectories())
            {
                if (subDir.Name.Equals(selectedLayoutName))
                {
                    templateDirPath = subDir.FullName;
                }
            }
            
            SkinManagerHelper.Copy(templateDirPath, skinPackageUploadDir.ToString());
            // unzip the survey template to upload folder
            string surveyZip = Path.Combine(templateDirPath, "SurveySkin.zip");
            surveyUploadDir.Create();
            SkinManagerHelper.UnZipFile(surveyZip, surveyUploadDir.FullName);

            string portalZip = Path.Combine(templateDirPath, "PortalSkin.zip");
            portalUploadDir.Create();
            SkinManagerHelper.UnZipFile(portalZip, portalUploadDir.FullName);

            string communityZip = Path.Combine(templateDirPath, "CommunitySkin.zip");
            communityUploadDir.Create();
            SkinManagerHelper.UnZipFile(communityZip, communityUploadDir.FullName);
            return true;
        }

        // Added by K.G. for phase 2 task: To create CreateSkinPackageDirectory with in the skin folder
        public bool CreateSkinPackageDirectoryAndUnzipFiles()
        {
            string skinFolderPath = skinRootDir.ToString();
            string skinPackageZip = Path.Combine(skinFolderPath, "SkinPackage.zip");
            if (!File.Exists(skinPackageZip) & !Directory.Exists(skinPackageZip))
            {
                return false;
            }
            skinPackageUploadDir.Create();
            SkinManagerHelper.UnZipFile(skinPackageZip, skinFolderPath);
            return true;
        }

        // Added by K.G for phase 2 task: To update substitution file
        public void UpdateSubstitutionFile(PanelPreferences prefs)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Path.Combine(skinPackageUploadDir.FullName, "Substitution.xml"));
            XmlNode variablesNode = doc.SelectSingleNode(Res.SubstitutionVariablesPath);

            foreach (XmlNode variableNode in variablesNode.ChildNodes)
            {
                foreach (var keyValuePair in prefs.DynamicGuiVariables)
                {
                    if (variableNode.Attributes[Res.ComponentNameAttribute].Value == keyValuePair.Key)
                    {
                        variableNode.Attributes[Res.DefaultAttribute].Value = keyValuePair.Value;
                        break;
                    }
                }
            }
            doc.Save(Path.Combine(skinPackageUploadDir.FullName, "Substitution.xml"));

        }

        // Modified by K.G(24-11-2011) TO support multi path upload 
        public void UpdateSkin(PanelPreferences prefs, string dataPath)
        {
            //Changes added for Dynamic GUI creation by Optimus
            foreach (GuiVariableGroup group in prefs.CurrentGuiTemplate.VariableGroups)
            {
                if (null == group.Variables) continue;
                foreach (GuiVariable guiVar in group.Variables)
                {
                    switch (guiVar.ReplacementDirectory.ToLower())
                    {
                        case Constants.Portal:
                            ApplyDynamicVariables(prefs, group.GroupName, guiVar, portalUploadDir.FullName, dataPath, ReplacementDirectory.Replacement.portal);
                            break;
                        case Constants.Survey:
                            ApplyDynamicVariables(prefs, group.GroupName, guiVar, surveyUploadDir.FullName, dataPath, ReplacementDirectory.Replacement.survey);
                            break;
                        case Constants.Community:
                            ApplyDynamicVariables(prefs, group.GroupName, guiVar, communityUploadDir.FullName, dataPath, ReplacementDirectory.Replacement.community);
                            break;
                        case Constants.PortalSurvey:
                        case Constants.SurveyPortal:
                            ApplyDynamicVariables(prefs, group.GroupName, guiVar, portalUploadDir.FullName, dataPath, ReplacementDirectory.Replacement.portal);
                            ApplyDynamicVariables(prefs, group.GroupName, guiVar, surveyUploadDir.FullName, dataPath, ReplacementDirectory.Replacement.survey);
                            break;
                        case Constants.PortalCommunity:
                        case Constants.CommunityPortal:
                            ApplyDynamicVariables(prefs, group.GroupName, guiVar, portalUploadDir.FullName, dataPath, ReplacementDirectory.Replacement.portal);
                            ApplyDynamicVariables(prefs, group.GroupName, guiVar, communityUploadDir.FullName, dataPath, ReplacementDirectory.Replacement.community);
                            break;
                        case Constants.SurveyCommunity:
                        case Constants.CommunitySurvey:
                            ApplyDynamicVariables(prefs, group.GroupName, guiVar, surveyUploadDir.FullName, dataPath, ReplacementDirectory.Replacement.survey);
                            ApplyDynamicVariables(prefs, group.GroupName, guiVar, communityUploadDir.FullName, dataPath, ReplacementDirectory.Replacement.community);
                            break;
                        case Constants.All:
                            ApplyDynamicVariables(prefs, group.GroupName, guiVar, portalUploadDir.FullName, dataPath, ReplacementDirectory.Replacement.portal);
                            ApplyDynamicVariables(prefs, group.GroupName, guiVar, surveyUploadDir.FullName, dataPath, ReplacementDirectory.Replacement.survey);
                            ApplyDynamicVariables(prefs, group.GroupName, guiVar, communityUploadDir.FullName, dataPath, ReplacementDirectory.Replacement.community);
                            break;
                    }
                }
            }
        }


        // Updating Portal and Survey Skin with Dynamic Variable Values by Optimus
        private static void ApplyDynamicVariables(PanelPreferences preferences, string groupName, GuiVariable guiVar, string skinFolderPath,
            string dataPath, ReplacementDirectory.Replacement replacementDirectory)
        {
            switch (guiVar.UiComponent)
            {
                // Modified by K.G for phase2 task 'Support upload/processing of non-image files (e.g. pdf)'.
                case "FileUpload()":
                    SkinManagerHelper.UpdateDynamicFile(preferences.DynamicGuiVariables[guiVar.ComponentName],
                        skinFolderPath, dataPath, guiVar.PropertyName, replacementDirectory, guiVar.PathToUpload);
                    break;
                case "ChoiceSelector()":
                    var replaceValues = guiVar.PropertyName.Split(',');
                    if (replaceValues.Length != 2) throw new Exception("Choice Selections must provide values for selected and unselected in PropertyName (e.g. 'block,none')");
                    var replacementValue = preferences.DynamicGuiVariables[guiVar.ComponentName] == "true" ? replaceValues[0] : replaceValues[1];
                    SkinManagerHelper.UpdateSkinDynamicVariable(replacementValue, skinFolderPath, guiVar.Substitution, guiVar.PropertyName, replacementDirectory);
                    break;
                default:
                    SkinManagerHelper.UpdateSkinDynamicVariable(preferences.DynamicGuiVariables[guiVar.ComponentName],
                        skinFolderPath, guiVar.Substitution, guiVar.PropertyName, replacementDirectory);
                    break;
            }
        }
        // Modified by K.G for phase2 tasks returned only one link of SkinPackage zip.
        public string CompressNewSkins(string newFolderName)
        {
            var offlineLinks = new OfflineLinks
                                    {
                                        portalLink = SkinManagerHelper.CompressFolder(portalUploadDir.FullName, skinRootDir.FullName, newFolderName, Res.NewPortalSkinPath),
                                        surveyLink = SkinManagerHelper.CompressFolder(surveyUploadDir.FullName, skinRootDir.FullName, newFolderName, Res.NewSurveySkinPath)
                                    };
            string communityzipPath = SkinManagerHelper.CompressFolder(communityUploadDir.FullName, skinRootDir.FullName, newFolderName, Res.NewCommunitySkinPath); //Added by K.G(24-11-2011) TO Support a third zip package called CommunitySkin.zip
            FileInfo[] portalSurveyZipFiles = { new FileInfo(offlineLinks.portalLink), new FileInfo(offlineLinks.surveyLink), new FileInfo(communityzipPath) };

            //Copied upadted Zipfiles to Skin Package.
            foreach (var zipFile in portalSurveyZipFiles)
            {
                if (zipFile.Length > 0)
                {
                    if (File.Exists(Path.Combine(skinPackageUploadDir.ToString(), zipFile.Name)))
                    {
                        File.Delete(Path.Combine(skinPackageUploadDir.ToString(), zipFile.Name));
                    }
                    zipFile.CopyTo(Path.Combine(skinPackageUploadDir.ToString(), zipFile.Name));
                }
            }
            string SkinPackageLink = SkinManagerHelper.CompressFolder(skinPackageUploadDir.FullName, skinRootDir.FullName, Res.SkinPackageToUploadPath, Res.NewSkinPackagePath);
            FileInfo SkinPackageFile = new FileInfo(SkinPackageLink);

            // Done K.G(25/11/2011) copied newly created zip package to portal skin so that it can also be uploaded in the folder under PortalStaging where the rest of the portal skin is uploaded
            SkinPackageFile.CopyTo(Path.Combine(portalUploadDir.ToString(),SkinPackageFile.Name));
            
            offlineLinks.portalLink = SkinManagerHelper.CompressFolder(portalUploadDir.FullName, skinRootDir.FullName, newFolderName, Res.NewPortalSkinPath);
            return SkinPackageLink;
        }
    }
}