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

        public SkinManager(String skinRootDir)
        {
            this.skinRootDir = new DirectoryInfo(skinRootDir);
            this.portalUploadDir = new DirectoryInfo(Path.Combine(skinRootDir, Res.PortalSkinToUploadPath));
            this.surveyUploadDir = new DirectoryInfo(Path.Combine(skinRootDir, Res.SurveySkinToUploadPath));
            this.templatesDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + Res.LocalTemplatePath);
            this.skinPackageUploadDir = new DirectoryInfo(Path.Combine(skinRootDir, Res.SkinPackageToUploadPath));
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
        }

        // Code modified for Dynamic GUI template by Optimus
        // TODO this shouldn't rely on enumeration order being consistent - selection should be 
        //  the folder name in case UI order doesn't match "EnumerateDirectories" order. 
        public bool CreateDirectoriesAndUnzipFiles(string selectedLayoutName)
        {
            // Modified by khushbu for Phase 2 Tasks now the template will be selected by name rather than index.
            var templateDirPath = string.Empty;
            foreach (var subDir in templatesDir.EnumerateDirectories())
            {
                if (subDir.Name.Equals(selectedLayoutName))
                {
                    templateDirPath = subDir.FullName;
                }
            }
            // var templateDirPath = templateDirectories[Convert.ToInt32(selectedLayoutIndex) - 1].ToString();
            SkinManagerHelper.Copy(templateDirPath, skinPackageUploadDir.ToString());
            // unzip the survey template to upload folder
            string surveyZip = Path.Combine(templateDirPath, "SurveySkin.zip");
            surveyUploadDir.Create();
            SkinManagerHelper.UnZipFile(surveyZip, surveyUploadDir.FullName);

            string portalZip = Path.Combine(templateDirPath, "PortalSkin.zip");
            portalUploadDir.Create();
            SkinManagerHelper.UnZipFile(portalZip, portalUploadDir.FullName);
            /*
            string surveyZip = Path.Combine(templateDirPath, "SurveySkin.zip");
            surveyUploadDir.Create();
            SkinManagerHelper.UnZipFile(surveyZip, surveyUploadDir.FullName);

            // unzip the portal template to upload folder
            string portalZip = Path.Combine(templateDirPath, "PortalSkin.zip");
            portalUploadDir.Create();
            SkinManagerHelper.UnZipFile(portalZip, portalUploadDir.FullName);
             * */

            return true;
        }

        // Added by khushbu for phase 2 task: To create CreateSkinPackageDirectory with in the skin folder
        public bool CreateSkinPackageDirectoryAndUnzipFiles()
        {
            string skinFolderPath = skinRootDir.ToString();
            string skinPackageZip = Path.Combine(skinFolderPath, "SkinPackage.zip");
            skinPackageUploadDir.Create();
            SkinManagerHelper.UnZipFile(skinPackageZip, skinFolderPath);
            return true;
        }

        // Added by khushbu for phase 2 task: To update substitution file
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
                            ApplyDynamicVariables(prefs, group.GroupName, guiVar, portalUploadDir.FullName, dataPath, true);
                            break;
                        case Constants.Survey:
                            ApplyDynamicVariables(prefs, group.GroupName, guiVar, surveyUploadDir.FullName, dataPath, false);
                            break;
                        case Constants.Both:
                            ApplyDynamicVariables(prefs, group.GroupName, guiVar, portalUploadDir.FullName, dataPath, true);
                            ApplyDynamicVariables(prefs, group.GroupName, guiVar, surveyUploadDir.FullName, dataPath, false);
                            break;
                    }
                }
            }
        }


        // Updating Portal and Survey Skin with Dynamic Variable Values by Optimus
        private static void ApplyDynamicVariables(PanelPreferences preferences, string groupName, GuiVariable guiVar, string skinFolderPath,
            string dataPath, bool isPortal)
        {
            switch (guiVar.UiComponent)
            {
                // Modified by Khushbu for phase2 task 'Support upload/processing of non-image files (e.g. pdf)'.
                case "FileUpload()":
                    SkinManagerHelper.UpdateDynamicFile(preferences.DynamicGuiVariables[guiVar.ComponentName],
                        skinFolderPath, dataPath, guiVar.PropertyName, isPortal, guiVar.PathToUpload);
                    break;
                case "ChoiceSelector()":
                    var replaceValues = guiVar.PropertyName.Split(',');
                    if (replaceValues.Length != 2) throw new Exception("Choice Selections must provide values for selected and unselected in PropertyName (e.g. 'block,none')");
                    var replacementValue = preferences.DynamicGuiVariables[guiVar.ComponentName] == "true" ? replaceValues[0] : replaceValues[1];
                    SkinManagerHelper.UpdateSkinDynamicVariable(replacementValue, skinFolderPath, guiVar.Substitution, guiVar.PropertyName, isPortal);
                    break;
                default:
                    SkinManagerHelper.UpdateSkinDynamicVariable(preferences.DynamicGuiVariables[guiVar.ComponentName],
                        skinFolderPath, guiVar.Substitution, guiVar.PropertyName, isPortal);
                    break;
            }
        }
        // Modified by khushbu for phase2 tasks returned only one link of SkinPackage zip.
        public string CompressNewSkins(string newFolderName)
        {
            var offlineLinks = new OfflineLinks
                                    {
                                        portalLink = SkinManagerHelper.CompressFolder(portalUploadDir.FullName, skinRootDir.FullName, newFolderName, Res.NewPortalSkinPath),
                                        surveyLink = SkinManagerHelper.CompressFolder(surveyUploadDir.FullName, skinRootDir.FullName, newFolderName, Res.NewSurveySkinPath)
                                    };
            FileInfo[] portalSurveyZipFiles = { new FileInfo(offlineLinks.portalLink), new FileInfo(offlineLinks.surveyLink) };

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
            return SkinPackageLink;
        }
    }
}