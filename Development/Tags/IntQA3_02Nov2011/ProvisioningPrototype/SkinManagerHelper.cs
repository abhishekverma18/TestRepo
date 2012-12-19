using System;
using System.IO;
using System.Text;
using Ionic.Zip;

namespace ProvisioningPrototype
{
    public class SkinManagerHelper
    {
        /*--------------------Portal Skin Updates-----------------*/
        public static bool UpdatePortalNewsletterVisibility(bool visible, string skinFolderPath)
        {
            if (visible)
            {
                return true; //By default its visible
            }

            string memberCssPath = Path.Combine(skinFolderPath, Res.PortalMemberCSSPath);
            Exists(memberCssPath);

            TextReader reader = new StreamReader(memberCssPath);
            string memberCss = reader.ReadToEnd();
            reader.Close();

            if (memberCss.Contains(Res.NewsletterVisibilityVariable))
            {
                memberCss = memberCss.Replace(Res.NewsletterVisibilityVariable, "display: none;");
            }
            else
            {
                throw new Exception("Newsletter Visibility Variable not found in member.css");
            }

            TextWriter writer = new StreamWriter(memberCssPath);
            writer.Write(memberCss);
            writer.Close();

            return true;

        }

        public static bool UpdatePortalQuickPollsVisibility(bool visible, string skinFolderPath)
        {
            if (visible)
            {
                return true; //By default its visible
            }

            string memberCssPath = Path.Combine(skinFolderPath, Res.PortalMemberCSSPath);
            Exists(memberCssPath);

            TextReader reader = new StreamReader(memberCssPath);
            string memberCss = reader.ReadToEnd();
            reader.Close();

            if (memberCss.Contains(Res.QuickPollVisibilityVariable))
            {
                memberCss = memberCss.Replace(Res.QuickPollVisibilityVariable, "display: none;");
            }
            else
            {
                throw new Exception("QuickPoll Visibility Variable not found in member.css");
            }

            TextWriter writer = new StreamWriter(memberCssPath);
            writer.Write(memberCss);
            writer.Close();

            return true;

        }

        public static bool UpdatePortalContentBackgroundColor(string hexCode, string skinFolderPath)
        {
            if (null == hexCode)
            {
                return true;
            }

            if (hexCode.Length != 6)
            {
                throw new Exception("Invalid Color Hex Code");
            }

            //Update the Login.css
            string cssPath = Path.Combine(skinFolderPath, Res.PortalLoginCSSPath);
            Exists(cssPath);

            TextReader reader = new StreamReader(cssPath);
            string css = reader.ReadToEnd();
            reader.Close();

            if (css.Contains(Res.ContentBackgroundColorVariable))
            {
                css = css.Replace(Res.ContentBackgroundColorVariable, "background-color: #" + hexCode + ";");
            }
            else
            {
                throw new Exception("ContentBackgroundColorVariable not found in login.css");
            }

            TextWriter writer = new StreamWriter(cssPath);
            writer.Write(css);
            writer.Close();

            //Update the Member.css
            cssPath = Path.Combine(skinFolderPath, Res.PortalMemberCSSPath);
            Exists(cssPath);

            reader = new StreamReader(cssPath);
            css = reader.ReadToEnd();
            reader.Close();

            if (css.Contains(Res.ContentBackgroundColorVariable))
            {
                css = css.Replace(Res.ContentBackgroundColorVariable, "background-color: #" + hexCode + ";");
            }
            else
            {
                throw new Exception("ContentBackgroundColorVariable not found in login.css");
            }

            writer = new StreamWriter(cssPath);
            writer.Write(css);
            writer.Close();

            return true;
        }

        public static bool UpdatePortalPrimaryTextColor(string hexCode, string skinFolderPath)
        {
            if (null == hexCode)
            {
                return true;
            }

            if (hexCode.Length != 6)
            {
                throw new Exception("Invalid Color Hex Code");
            }



            //Update the Member.css
            string cssPath = Path.Combine(skinFolderPath, Res.PortalMemberCSSPath);
            Exists(cssPath);

            TextReader reader = new StreamReader(cssPath);
            string css = reader.ReadToEnd();
            reader.Close();

            if (css.Contains(Res.PrimaryTextColorVariable))
            {
                css = css.Replace(Res.PrimaryTextColorVariable, "color: #" + hexCode + ";");
            }
            else
            {
                throw new Exception("PrimaryTextColorVariable not found in Login.css");
            }

            TextWriter writer = new StreamWriter(cssPath);
            writer.Write(css);
            writer.Close();

            //Update the Common.css
            cssPath = Path.Combine(skinFolderPath, Res.PortalCommonCSSPath);
            Exists(cssPath);

            reader = new StreamReader(cssPath);
            css = reader.ReadToEnd();
            reader.Close();

            if (css.Contains(Res.PrimaryTextColorVariable))
            {
                css = css.Replace(Res.PrimaryTextColorVariable, "color: #" + hexCode + ";");
            }
            else
            {
                throw new Exception("PrimaryTextColorVariable not found in Common.css");
            }

            writer = new StreamWriter(cssPath);
            writer.Write(css);
            writer.Close();

            //Update Login.css
            cssPath = Path.Combine(skinFolderPath, Res.PortalLoginCSSPath);
            Exists(cssPath);

            reader = new StreamReader(cssPath);
            css = reader.ReadToEnd();
            reader.Close();

            if (css.Contains(Res.PrimaryTextColorVariable))
            {
                css = css.Replace(Res.PrimaryTextColorVariable, "color: #" + hexCode + ";");
            }
            else
            {
                throw new Exception("PrimaryTextColorVariable not found in Common.css");
            }

            writer = new StreamWriter(cssPath);
            writer.Write(css);
            writer.Close();

            return true;
        }

        public static bool UpdatePortalSecondaryTextColor(string hexCode, string skinFolderPath)
        {
            if (null == hexCode)
            {
                return true;
            }

            if (hexCode.Length != 6)
            {
                throw new Exception("Invalid Color Hex Code");
            }


            //Update the Member.css
            string cssPath = Path.Combine(skinFolderPath, Res.PortalMemberCSSPath);
            Exists(cssPath);

            TextReader reader = new StreamReader(cssPath);
            string css = reader.ReadToEnd();
            reader.Close();

            if (css.Contains(Res.SecondaryTextColorVariable))
            {
                css = css.Replace(Res.SecondaryTextColorVariable, "color: #" + hexCode + ";");
            }
            else
            {
                throw new Exception("PrimaryTextColorVariable not found in Login.css");
            }

            TextWriter writer = new StreamWriter(cssPath);
            writer.Write(css);
            writer.Close();



            return true;
        }

        public static bool UpdatePortalPageBackgroundColor(string hexCode, string skinFolderPath)
        {
            if (null == hexCode)
            {
                return true;
            }

            if (hexCode.Length != 6)
            {
                throw new Exception("Invalid Color Hex Code");
            }



            string commonCssPath = Path.Combine(skinFolderPath, Res.PortalCommonCSSPath);
            Exists(commonCssPath);

            TextReader reader = new StreamReader(commonCssPath);
            string commonCss = reader.ReadToEnd();
            reader.Close();

            if (commonCss.Contains(Res.PageBackgroundColorVariable))
            {
                commonCss = commonCss.Replace(Res.PageBackgroundColorVariable, "background-color: #" + hexCode + ";");
            }
            else
            {
                throw new Exception("BackgroundColorVariable not found in common.css");
            }

            TextWriter writer = new StreamWriter(commonCssPath);
            writer.Write(commonCss);
            writer.Close();

            return true;
        }


        public static bool UpdatePortalLogo(string logoFileName, string skinFolderPath, string dataPath)
        {
            if (null == logoFileName)
            {
                return true;
            }

            if (logoFileName.Length == 0)
            {
                return true;
            }

            if (!logoFileName.ToLower().Contains(".jpg"))
            {
                throw new Exception("Logo file is not of type jpg");
            }

            string logoImagePath = Path.Combine(skinFolderPath, Res.LogoImageFilePath);

            if (File.Exists(logoImagePath))
            {
                File.Delete(logoImagePath);
            }

            string newLogoImagePath = Path.Combine(dataPath, @"Uploads\" + logoFileName);
            Exists(newLogoImagePath);

            File.Copy(newLogoImagePath, logoImagePath);

            Exists(logoImagePath);

            return true;
        }

        public static bool UpdatePortalHeader(string headerFileName, string skinFolderPath, string dataPath)
        {
            if (null == headerFileName)
            {
                return true;
            }

            if (headerFileName.Length == 0)
            {
                return true;
            }

            if (!headerFileName.ToLower().Contains(".jpg"))
            {
                throw new Exception("Header file is not of type jpg");
            }

            string headerImagePath = Path.Combine(skinFolderPath, Res.PortalHeaderImageFilePath);

            if (File.Exists(headerImagePath))
            {
                File.Delete(headerImagePath);
            }

            string newHeaderImagePath = Path.Combine(dataPath, @"Uploads\" + headerFileName);
            Exists(newHeaderImagePath);

            File.Copy(newHeaderImagePath, headerImagePath);

            Exists(headerImagePath);

            return true;
        }

        /*--------------------Survey Skin Updates-----------------*/

        public static bool UpdateSurveyPrimaryTextColor(string hexCode, string skinFolderPath)
        {
            if (null == hexCode)
            {
                return true;
            }

            if (hexCode.Length != 6)
            {
                throw new Exception("Invalid Color Hex Code");
            }

            string layoutCssPath = Path.Combine(skinFolderPath, Res.SurveyLayoutCSSPath);
            Exists(layoutCssPath);

            TextReader reader = new StreamReader(layoutCssPath);
            string layoutCss = reader.ReadToEnd();
            reader.Close();

            if (layoutCss.Contains(Res.PrimaryTextColorVariable))
            {
                layoutCss = layoutCss.Replace(Res.PrimaryTextColorVariable, "color: #" + hexCode + ";");
            }
            else
            {
                throw new Exception("ContentBackgroundColorVariable not found in SurveyLayout.css");
            }

            TextWriter writer = new StreamWriter(layoutCssPath);
            writer.Write(layoutCss);
            writer.Close();

            return true;

        }

        public static bool UpdateSurveyContentBackgroundColor(string hexCode, string skinFolderPath)
        {
            if (null == hexCode)
            {
                return true;
            }

            if (hexCode.Length != 6)
            {
                throw new Exception("Invalid Color Hex Code");
            }

            string layoutCssPath = Path.Combine(skinFolderPath, Res.SurveyLayoutCSSPath);
            Exists(layoutCssPath);

            TextReader reader = new StreamReader(layoutCssPath);
            string layoutCss = reader.ReadToEnd();
            reader.Close();

            if (layoutCss.Contains(Res.ContentBackgroundColorVariable))
            {
                layoutCss = layoutCss.Replace(Res.ContentBackgroundColorVariable, "background-color: #" + hexCode + ";");
            }
            else
            {
                throw new Exception("ContentBackgroundColorVariable not found in SurveyLayout.css");
            }

            TextWriter writer = new StreamWriter(layoutCssPath);
            writer.Write(layoutCss);
            writer.Close();

            return true;
        }

        public static bool UpdateSurveyPageBackgroundColor(string hexCode, string skinFolderPath)
        {
            if (null == hexCode)
            {
                return true;
            }

            if (hexCode.Length != 6)
            {
                throw new Exception("Invalid Color Hex Code");
            }

            string layoutCssPath = Path.Combine(skinFolderPath, Res.SurveyLayoutCSSPath);
            Exists(layoutCssPath);

            TextReader reader = new StreamReader(layoutCssPath);
            string layoutCss = reader.ReadToEnd();
            reader.Close();

            if (layoutCss.Contains(Res.PageBackgroundColorVariable))
            {
                layoutCss = layoutCss.Replace(Res.PageBackgroundColorVariable, "background-color: #" + hexCode + ";");
            }
            else
            {
                throw new Exception("BackgroundColorVariable not found in colors.css");
            }

            TextWriter writer = new StreamWriter(layoutCssPath);
            writer.Write(layoutCss);
            writer.Close();

            return true;
        }

        public static bool UpdateSurveyHeader(string headerFileName, string skinFolderPath, string dataPath)
        {
            if (null == headerFileName)
            {
                return true;
            }

            if (headerFileName.Length == 0)
            {
                return true;
            }

            if (!headerFileName.ToLower().Contains(".jpg"))
            {
                throw new Exception("Header file is not of type jpg");
            }

            string headerImagePath = Path.Combine(skinFolderPath, Res.SurveyHeaderImageFilePath);

            if (File.Exists(headerImagePath))
            {
                File.Delete(headerImagePath);
            }

            string newHeaderImagePath = Path.Combine(dataPath, @"Uploads\" + headerFileName);
            Exists(newHeaderImagePath);

            File.Copy(newHeaderImagePath, headerImagePath);

            Exists(headerImagePath);

            return true;
        }

        /*Start--------------------Portal and Survey Skin Updates for Dynamic Variables by Optimus-----------------*/

        public static bool UpdateSkinDynamicVariableColor(string hexCode, string skinFolderPath, string substitution,
            string propertyName, bool isPortal)
        {
            if (null == hexCode)
            {
                return true;
            }

            if (hexCode.Length != 6)
            {
                throw new Exception(Res.HexCodeException);
            }

            string cssPath = isPortal ? Res.PortalCommonCSSPath : Res.SurveyLayoutCSSPath;

            string combineCssPath = Path.Combine(skinFolderPath, cssPath);
            Exists(combineCssPath);

            TextReader reader = new StreamReader(combineCssPath);
            string commonCss = reader.ReadToEnd();
            reader.Close();

            if (commonCss.Contains(substitution))
            {
                commonCss = commonCss.Replace(substitution, String.Format("{0} : #{1};", propertyName, hexCode));
            }
            else
            {
                throw new Exception(String.Format("{0} not found in {1}", substitution, cssPath));

            }

            TextWriter writer = new StreamWriter(combineCssPath);
            writer.Write(commonCss);
            writer.Close();

            return true;
        }

        public static bool UpdateSkinVariableListChoiceText(string value, string skinFolderPath, string substitution,
            string propertyName, bool isPortal)
        {
            if (null == value)
            {
                return true;
            }

            string cssPath = isPortal ? Res.PortalCommonCSSPath : Res.SurveyLayoutCSSPath;

            string combineCssPath = Path.Combine(skinFolderPath, cssPath);
            Exists(combineCssPath);

            TextReader reader = new StreamReader(combineCssPath);
            string commonCss = reader.ReadToEnd();
            reader.Close();

            if (commonCss.Contains(substitution))
            {
                commonCss = commonCss.Replace(substitution, String.Format("{0} : {1};", propertyName, value));
            }
            else
            {
                throw new Exception(String.Format("{0} not found in {1}", substitution, cssPath));
            }

            TextWriter writer = new StreamWriter(combineCssPath);
            writer.Write(commonCss);
            writer.Close();

            return true;
        }

        public static bool UpdateSkinChoice(string value, string skinFolderPath, string substitution,
            string propertyName, bool isPortal)
        {
            if (Convert.ToBoolean(value) || !isPortal)
            {
                return true; //By default its visible
            }

            string cssPath = Res.PortalMemberCSSPath;

            string combineCssPath = Path.Combine(skinFolderPath, cssPath);
            Exists(combineCssPath);

            TextReader reader = new StreamReader(combineCssPath);
            string commonCss = reader.ReadToEnd();
            reader.Close();

            if (commonCss.Contains(substitution))
            {
                commonCss = commonCss.Replace(substitution, String.Format("{0};", propertyName));
            }
            else
            {
                throw new Exception(String.Format("{0} not found in {1}", substitution, cssPath));
            }

            TextWriter writer = new StreamWriter(combineCssPath);
            writer.Write(commonCss);
            writer.Close();

            return true;
        }

        public static bool UpdateDynamicImage(string fileName, string skinFolderPath, string dataPath, string propertyName, bool isPortal)
        {
            if (null == fileName)
            {
                return true;
            }

            if (fileName.Length == 0)
            {
                return true;
            }

            if (!fileName.ToLower().Contains(Constants.JpgExtension))
            {
                throw new Exception(Res.ImageExtensionException);
            }

            string imgFilePath = isPortal ? Res.PortalImageFilePath : Res.SurveyImageFilePath;

            StringBuilder imageFilePath = new StringBuilder();
            imageFilePath.Append(imgFilePath);
            imageFilePath.Append(propertyName);
            imageFilePath.Append(Constants.JpgExtension);


            string combinedImagePath = Path.Combine(skinFolderPath, imageFilePath.ToString());

            if (File.Exists(combinedImagePath))
            {
                File.Delete(combinedImagePath);
            }

            string newImagePath = Path.Combine(dataPath,
                                               String.Format("{0}{1}", Res.UploadsFolder, Path.GetFileName(fileName)));
            Exists(newImagePath);

            File.Copy(newImagePath, combinedImagePath);

            Exists(combinedImagePath);

            return true;
        }

        /*End--------------------Portal and Survey Skin Updates for Dynamic Variables by Optimus-----------------*/

        public static string CompressFolder(string inputFolderPath, string skinFolderPath, string newFolderName, string zipFileName)
        {
            Exists(inputFolderPath);

            using (var zip = new ZipFile())
            {
                zip.AddDirectory(inputFolderPath, newFolderName);
                zip.Save(skinFolderPath + @"\" + zipFileName);
            }
            //Added  for returning skin folder path by Optimus
            return skinFolderPath + @"\" + zipFileName;
        }

        public static bool UnZipFile(string inputZipFile, string outputFolderPath)
        {
            Exists(inputZipFile);

            using (ZipFile zip = ZipFile.Read(inputZipFile))
            {
                if (zip.Count == 0)
                {
                    throw new Exception("No files found to extract");
                }

                foreach (ZipEntry e in zip)
                {
                    e.Extract(outputFolderPath, ExtractExistingFileAction.OverwriteSilently);
                }
            }

            return true;
        }

        public static bool Exists(string path)
        {
            if (!File.Exists(path) & !Directory.Exists(path))
            {
                throw new FileNotFoundException("File not found at path: " + path);
            }

            return true;
        }
    }
}