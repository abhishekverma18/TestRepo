using System;
using System.IO;
using System.Text;
using Ionic.Zip;
using System.Collections.Generic;

namespace ProvisioningPrototype
{
    public class SkinManagerHelper
    {
        private static String[] supportedTypes = { "css", "htm", "html", "ascx", "aspx", "js" };
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

        // Modified by khushbu: To make image upload control to file upload control & Now Path is coming from substitution.xml.
        public static bool UpdateDynamicFile(string fileName, string skinFolderPath, string dataPath, string propertyName, bool isPortal, string pathToUpload)
        {
            if (null == fileName)
            {
                return true;
            }

            if (fileName.Length == 0)
            {
                return true;
            }
            string fileExtention = Path.GetExtension(fileName);

            //if (!fileName.ToLower().Contains(Constants.JpgExtension))
            //{
            //    throw new Exception(Res.ImageExtensionException);
            //}

            //string imgFilePath = isPortal ? Res.PortalImageFilePath : Res.SurveyImageFilePath;
            var newFilePath = string.Empty;
            if (!string.IsNullOrEmpty(pathToUpload))
            {
                newFilePath = pathToUpload;
            }
            StringBuilder filePath = new StringBuilder();
            filePath.Append(newFilePath);
            filePath.Append(propertyName);
            filePath.Append(fileExtention);


            string combinedFilePath = Path.Combine(skinFolderPath, filePath.ToString());

            if (File.Exists(combinedFilePath))
            {
                File.Delete(combinedFilePath);
            }

            string newFileSourcePath = Path.Combine(dataPath,
                                               String.Format("{0}{1}", Res.UploadsFolder, Path.GetFileName(fileName)));
            Exists(newFileSourcePath);

            File.Copy(newFileSourcePath, combinedFilePath);

            Exists(combinedFilePath);

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

        // Added by Optimus To generate a Skin Package
        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into it's new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                if (!fi.Extension.Equals(Constants.SVN) && !fi.Name.Equals("PortalSkin.zip") && !fi.Name.Equals("SurveySkin.zip"))
                {
                    fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
                }
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                if (!diSourceSubDir.Extension.Equals(Constants.SVN))
                {
                    DirectoryInfo nextTargetSubDir =
                        target.CreateSubdirectory(diSourceSubDir.Name);
                    CopyAll(diSourceSubDir, nextTargetSubDir);
                }
            }
        }
    }
}