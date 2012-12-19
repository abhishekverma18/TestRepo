using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProvisioningPrototype
{
    public class PxmlManager
    {
        public string GetPxml(string pxmlFolder, string name)
        {
            if (!Directory.Exists(pxmlFolder))
            {
                throw new DirectoryNotFoundException("PQs folder not found at: " + pxmlFolder);
            }

            var pxmlFileList = Directory.GetFiles(pxmlFolder);

            if (pxmlFileList.Count() == 0)
            {
                throw new Exception("No profiling questionnaires found in directory: " + pxmlFolder);
            }
            
            foreach (var pxmlFile in pxmlFileList)
            {
                string pxmlName = Path.GetFileNameWithoutExtension(pxmlFile);

                if (pxmlName != null)
                    if(pxmlName.Equals(name))
                    {
                        return pxmlFile;
                    }
            }

            throw new Exception("Pxml with name '" + name + "' not found in folder '" + pxmlFolder + "'");
        }

        public List<Pxml> GetPxmlList(string pxmlFolder)
        {
            if(!Directory.Exists(pxmlFolder))
            {
                throw new DirectoryNotFoundException("PQs folder not found at: " + pxmlFolder);
            }

            var pxmlFileList = Directory.GetFiles(pxmlFolder);

            if(pxmlFileList.Count() == 0)
            {
                throw new Exception("No profiling questionnaires found in directory: " + pxmlFolder);
            }

            var list = new List<Pxml>();

            foreach(var pxmlFile in pxmlFileList)
            {
                string pxmlName = Path.GetFileNameWithoutExtension(pxmlFile); 
                list.Add(new Pxml(pxmlName));
            }

            return list;
        }
    }
}