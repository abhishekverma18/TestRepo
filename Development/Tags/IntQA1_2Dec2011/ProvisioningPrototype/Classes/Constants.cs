using System.Net.Mime;

namespace ProvisioningPrototype
{
    public class Constants
    {
        public const string FilesGroup = "Files"; // Modified  by K.G for phase 2 tasks : To make Image group to fileGroup
        public const string ChoiceGroup = "Choices";
        public const string SVN = ".svn";
        public const string TempExtension = "*.tmp";
        public const string Portal = "portal";
        public const string Survey = "survey";
        public const string Community = "community";

        //Start- Added by K.G.(24-11-2011) to support multi paths for 'Support upload/processing of non-image files (e.g. pdf)' module
        public const string PortalSurvey = "portalsurvey";
        public const string SurveyPortal = "surveyportal";
        public const string PortalCommunity = "portalcommunity";
        public const string CommunityPortal = "communityportal";
        public const string SurveyCommunity = "surveycommunity";
        public const string CommunitySurvey = "communitysurvey";
        public const string All = "all";
        // End

        public const string TemplateColors = "TemplateColors";
        public const string Choices = "Choices";
        public const string List = "List";
        public const string Text = "Text";
        public const string Images = "Images";
        public const string JpgExtension = ".jpg";
    }
}
