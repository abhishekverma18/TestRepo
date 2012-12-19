using System;
using System.IO;
using System.Text;
using Ionic.Zip;
using System.Collections.Generic;

namespace ProvisioningPrototype
{
    public class SkinManagerHelper
    {
        private static String[] supportedTypes = {"css", "htm", "html", "ascx", "aspx", "js"};
        private static void FileSearch(string sDir, List<String> foundFiles)
        {
            foreach (string d in Directory.GetDirectories(sDir))
            {
                foreach (string extension in supportedTypes)
                {
                    foreach (string f in Directory.GetFiles(d, "*." + extension))
                    {
                        foundFiles.Add(f);
                    }
                }
                FileSearch(d, foundFiles);
            }
        }

        /*Start--------------------Portal and Survey Skin Updates for Dynamic Variables by Optimus-----------------*/

        public static bool UpdateSkinDynamicVariable(string replacementValue, string skinFolderPath, string substitution,
            string propertyName, bool isPortal)
        {
            List<String> files = new List<string>();
            FileSearch(skinFolderPath, files);

            foreach (string file in files)
            {
                if (!Exists(file)) continue;
                TextReader reader = new StreamReader(file);
                string contents = reader.ReadToEnd();
                reader.Close();
                if (contents.Contains(substitution))
                {
                    contents = contents.Replace(substitution, replacementValue);
                }
                TextWriter writer = new StreamWriter(file);
                writer.Write(contents);
                writer.Close();
            }
            return true;
        }

/*        public static bool UpdateSkinVariableListChoiceText(string value, string skinFolderPath, string substitution,
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
            TextWriter writer = new StreamWriter(combineCssPath);
            writer.Write(commonCss);
            writer.Close();

            return true;
        }
*/
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