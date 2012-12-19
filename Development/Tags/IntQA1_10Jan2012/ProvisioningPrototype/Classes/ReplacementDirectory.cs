using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProvisioningPrototype
{
    //Added by K.G.(24-11-2011) to support multi paths for 'Support upload/processing of non-image files (e.g. pdf)' module
    public class ReplacementDirectory
    {
        public enum Replacement
        {
            portal = 1,
            survey = 2,
            community = 3
        }

        public static string GetReplacementDirectory(Replacement directoryName)
        {
            return Enum.GetName(typeof(Replacement), directoryName);
        }
    }
}