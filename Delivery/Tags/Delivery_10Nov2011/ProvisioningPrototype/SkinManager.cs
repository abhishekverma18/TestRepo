using System;
using System.Collections;
using System.IO;

namespace ProvisioningPrototype
{
    public class SkinManager
    {
        private DirectoryInfo skinRootDir;
        private DirectoryInfo portalUploadDir;
        private DirectoryInfo surveyUploadDir;
        private DirectoryInfo templatesDir; 

        public SkinManager(String skinRootDir)
        {
            this.skinRootDir = new DirectoryInfo(skinRootDir);
            this.portalUploadDir = new DirectoryInfo(Path.Combine(skinRootDir, Res.PortalSkinToUploadPath));
            this.surveyUploadDir = new DirectoryInfo(Path.Combine(skinRootDir, Res.SurveySkinToUploadPath));
            this.templatesDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + Res.LocalTemplatePath);
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
        }

        // Code modified for Dynamic GUI template by Optimus
        // TODO this shouldn't rely on enumeration order being consistent - selection should be 
        //  the folder name in case UI order doesn't match "EnumerateDirectories" order. 
        public bool CreateDirectoriesAndUnzipFiles(string selectedLayoutIndex)
        {
            var templateDirectories = new ArrayList();

            foreach (var subDir in templatesDir.EnumerateDirectories())
            {
                if (!subDir.Name.Equals(Constants.SVN))
                {
                    templateDirectories.Add(subDir.FullName);
                }
            }
            var templateDirPath = templateDirectories[Convert.ToInt32(selectedLayoutIndex) - 1].ToString();
           
            // unzip the survey template to upload folder
            string surveyZip = Path.Combine(templateDirPath, "SurveySkin.zip");
            surveyUploadDir.Create();
            SkinManagerHelper.UnZipFile(surveyZip, surveyUploadDir.FullName);

            // unzip the portal template to upload folder
            string portalZip = Path.Combine(templateDirPath, "PortalSkin.zip");
            portalUploadDir.Create();
            SkinManagerHelper.UnZipFile(portalZip, portalUploadDir.FullName);

            return true;
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
                case "ImageUpload()":
                    SkinManagerHelper.UpdateDynamicImage(preferences.DynamicGuiVariables[guiVar.ComponentName],
                        skinFolderPath, dataPath, guiVar.PropertyName, isPortal);
                    break;
                case "ChoiceSelector()":
                    var replaceValues = guiVar.PropertyName.Split(',');
                    if (replaceValues.Length != 2) throw new Exception("Choice Selections must provide values for selected and unselected in PropertyName (e.g. 'block,none')");
                    var replacementValue = preferences.DynamicGuiVariables[guiVar.ComponentName] == "true" ? replaceValues[0]  : replaceValues[1];
                    SkinManagerHelper.UpdateSkinDynamicVariable(replacementValue, skinFolderPath, guiVar.Substitution, guiVar.PropertyName, isPortal);
                    break;
                default:
                    SkinManagerHelper.UpdateSkinDynamicVariable(preferences.DynamicGuiVariables[guiVar.ComponentName],
                        skinFolderPath, guiVar.Substitution, guiVar.PropertyName, isPortal);
                    break;
            }
        }

        public OfflineLinks CompressNewSkins(string newFolderName)
        {
            var offlineLinks = new OfflineLinks
                                    {
                                        portalLink = SkinManagerHelper.CompressFolder(portalUploadDir.FullName, skinRootDir.FullName, newFolderName, Res.NewPortalSkinPath),
                                        surveyLink = SkinManagerHelper.CompressFolder(surveyUploadDir.FullName, skinRootDir.FullName, newFolderName, Res.NewSurveySkinPath)
                                    };
            return offlineLinks;
        }
    }
}