using System;
using System.Collections;
using System.IO;

namespace ProvisioningPrototype
{
    public class SkinManager
    {
        public static bool CopyGenericFilesToUploadFolders(string selectedLayoutIndex, string skinFolderPath)
        {

            var templatesDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + Res.LocalTemplatePath);

            var templateDirectories = new ArrayList();

            foreach (var subDir in templatesDir.EnumerateDirectories())
            {
                if (!subDir.Name.Equals(".svn"))
                {
                    templateDirectories.Add(subDir.FullName);
                }
            }
            var templateDirPath = templateDirectories[Convert.ToInt32(selectedLayoutIndex) - 1].ToString();
            CopyGenericPortalSkinToUploadFolder(templateDirPath, skinFolderPath);
            CopyGenericSurveySkinToUploadFolder(templateDirPath, skinFolderPath);
            return true;
        }

        private static void CopyGenericPortalSkinToUploadFolder(string templateDirPath, string skinFolderPath)
        {
            SkinManagerHelper.Exists(skinFolderPath);


            string genericPortalSkinPath = Path.Combine(templateDirPath, Res.PortalSkinFileName);
            SkinManagerHelper.Exists(genericPortalSkinPath);

            string portalSkinToUploadPath = Path.Combine(skinFolderPath, Res.PortalSkinZipToUploadPath);

            File.Copy(genericPortalSkinPath, portalSkinToUploadPath, true);//TODO WARN OF OVERWRITING
            SkinManagerHelper.Exists(portalSkinToUploadPath);
        }

        private static void CopyGenericSurveySkinToUploadFolder(string templateDirPath, string skinFolderPath)
        {
            SkinManagerHelper.Exists(skinFolderPath);

            string genericSurveySkinPath = Path.Combine(templateDirPath, Res.SurveySkinFileName);
            SkinManagerHelper.Exists(genericSurveySkinPath);

            string surveySkinToUploadPath = Path.Combine(skinFolderPath, Res.SurveySkinZipToUploadPath);

            File.Copy(genericSurveySkinPath, surveySkinToUploadPath, true);//TODO WARN OF OVERWRITING
            SkinManagerHelper.Exists(surveySkinToUploadPath);
        }

        public static bool ExtractUploadFolderZipFiles(string skinFolderPath)
        {
            DeleteTemporaryFiles(Path.Combine(skinFolderPath, Res.PortalSkinToUploadPath));
            DeleteTemporaryFiles(Path.Combine(skinFolderPath, Res.SurveySkinToUploadPath));
            ExtractPortalFolderZipFiles(skinFolderPath);
            ExtractSurveyFolderZipFiles(skinFolderPath);

            return true;
        }

        private static void DeleteTemporaryFiles(string dirPath)
        {
            SkinManagerHelper.Exists(dirPath);

            string[] filePaths = Directory.GetFiles(dirPath, "*.tmp",
                                         SearchOption.AllDirectories);
            foreach (var filePath in filePaths)
            {
                File.Delete(filePath);
            }
        }


        private static void ExtractPortalFolderZipFiles(string skinFolderPath)
        {
            string newPortalSkinPath = Path.Combine(skinFolderPath, Res.PortalSkinZipToUploadPath);
            SkinManagerHelper.Exists(newPortalSkinPath);

            SkinManagerHelper.UnZipFile(newPortalSkinPath, Path.Combine(skinFolderPath, Res.PortalSkinToUploadPath));
        }

        private static void ExtractSurveyFolderZipFiles(string skinFolderPath)
        {
            string newSurveySkinPath = Path.Combine(skinFolderPath, Res.SurveySkinZipToUploadPath);
            SkinManagerHelper.Exists(newSurveySkinPath);

            SkinManagerHelper.UnZipFile(newSurveySkinPath, Path.Combine(skinFolderPath, Res.SurveySkinToUploadPath));
        }

        public static bool DeleteUnusedZipFiles(string skinFolderpath)
        {
            File.Delete(Path.Combine(skinFolderpath, Res.PortalSkinZipToUploadPath));
            File.Delete(Path.Combine(skinFolderpath, Res.SurveySkinZipToUploadPath));
            return true;
        }

        public static void UpdateSkin(PanelPreferences preferences, string skinFolderPath, string dataPath)
        {
            //Changes added for Dynamic GUI creation
            foreach (GuiVariableGroup group in preferences.CurrentGuiTemplate.VariableGroups)
            {
                foreach (GuiVariable guiVar in group.Variables)
                {
                    switch (guiVar.ReplacementDirectory.ToLower())
                    {
                        case "portal":
                            UpdateSkinWithDynamicVariables(preferences, group.GroupName, guiVar, skinFolderPath, dataPath, true);
                            break;
                        case "survey":
                            UpdateSkinWithDynamicVariables(preferences, group.GroupName, guiVar, skinFolderPath, dataPath, false);
                            break;
                        case "both":
                            UpdateSkinWithDynamicVariables(preferences, group.GroupName, guiVar, skinFolderPath, dataPath, true);
                            UpdateSkinWithDynamicVariables(preferences, group.GroupName, guiVar, skinFolderPath, dataPath, false);
                            break;
                    }
                }
            }
            //UpdatePortalSkin(preferences, skinFolderPath, dataPath);
            //UpdateSurveySkin(preferences, skinFolderPath, dataPath);
        }

        private static bool UpdatePortalSkin(PanelPreferences preferences, string skinFolderPath, string dataPath)
        {
            SkinManagerHelper.UpdatePortalHeader(preferences.HeaderFileName, skinFolderPath, dataPath);
            SkinManagerHelper.UpdatePortalLogo(preferences.LogoFileName, skinFolderPath, dataPath);

            SkinManagerHelper.UpdatePortalPageBackgroundColor(preferences.PageBackgroundHexCode, skinFolderPath);
            SkinManagerHelper.UpdatePortalContentBackgroundColor(preferences.ContentBackgroundHexCode, skinFolderPath);
            SkinManagerHelper.UpdatePortalPrimaryTextColor(preferences.PrimaryTextHexCode, skinFolderPath);
            SkinManagerHelper.UpdatePortalSecondaryTextColor(preferences.SecondaryTextHexCode, skinFolderPath);

            SkinManagerHelper.UpdatePortalNewsletterVisibility(preferences.NewsletterVisible, skinFolderPath);
            SkinManagerHelper.UpdatePortalQuickPollsVisibility(preferences.QuickPollVisible, skinFolderPath);

            //UpdateSkinWithDynamicVariables(preferences, skinFolderPath, dataPath, true);

            return true;
        }

        private static bool UpdateSurveySkin(PanelPreferences preferences, string skinFolderPath, string dataPath)
        {
            SkinManagerHelper.UpdateSurveyPageBackgroundColor(preferences.PageBackgroundHexCode, skinFolderPath);
            SkinManagerHelper.UpdateSurveyHeader(preferences.HeaderFileName, skinFolderPath, dataPath);

            SkinManagerHelper.UpdateSurveyContentBackgroundColor(preferences.ContentBackgroundHexCode, skinFolderPath);
            SkinManagerHelper.UpdateSurveyPrimaryTextColor(preferences.PrimaryTextHexCode, skinFolderPath);

            //UpdateSkinWithDynamicVariables(preferences, skinFolderPath, dataPath, false);

            return true;
        }

        // Updating Portal and Survey Skin with Dynamic Variable Values
        private static void UpdateSkinWithDynamicVariables(PanelPreferences preferences, string groupName, GuiVariable guiVar, string skinFolderPath,
            string dataPath, bool isPortal)
        {
            switch (groupName)
            {
                case "TemplateColors":
                    SkinManagerHelper.UpdateSkinDynamicVariableColor(preferences.DynamicGuiVariables[guiVar.ComponentName],
                        skinFolderPath, guiVar.Substitution, guiVar.PropertyName, isPortal);
                    break;
                case "List":
                case "Choices":
                case "Text":
                    SkinManagerHelper.UpdateSkinVariableListChoiceText(preferences.DynamicGuiVariables[guiVar.ComponentName],
                        skinFolderPath, guiVar.Substitution, guiVar.PropertyName, isPortal);
                    break;
                case "Images":
                    SkinManagerHelper.UpdateDynamicImage(preferences.DynamicGuiVariables[guiVar.ComponentName],
                        skinFolderPath, dataPath, guiVar.PropertyName, isPortal);
                    break;
            }
        }

        public static OfflineLinks CompressSkinFolder(string skinFolderPath, string newFolderName)
        {
            var offlinLinks = new OfflineLinks
                                           {
                                               portalLink = CompressPortalSkinFolder(skinFolderPath, newFolderName),
                                               surveyLink = CompressSurveySkinFolder(skinFolderPath, newFolderName)
                                           };
            return offlinLinks;
        }

        //Added for returning Compress folder path
        private static string CompressPortalSkinFolder(string skinFolderPath, string newFolderName)
        {
            string updatedSkinPath = Path.Combine(skinFolderPath, Res.PortalSkinToUploadPath);
            SkinManagerHelper.Exists(updatedSkinPath);
            return SkinManagerHelper.CompressFolder(updatedSkinPath, skinFolderPath, newFolderName, Res.NewPortalSkinPath);
        }

        private static string CompressSurveySkinFolder(string skinFolderPath, string newFolderName)
        {
            string updatedSkinPath = Path.Combine(skinFolderPath, Res.SurveySkinToUploadPath);
            SkinManagerHelper.Exists(updatedSkinPath);
            return SkinManagerHelper.CompressFolder(updatedSkinPath, skinFolderPath, newFolderName, Res.NewSurveySkinPath);
        }
    }
}