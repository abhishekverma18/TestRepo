using System;
using System.IO;
using System.Net;
using System.Text;

namespace ProvisioningPrototype.Web_Automation
{
    public class StudyStatus
    {
        public static CookieJar StudyStatusPostToStudyQuestionnaire(CookieJar cookieJar, PanelPreferences preferences)
        {
            var studyStatusViewUrl = new Uri(preferences.PanelAdminUrl + "StudyStatusView.aspx");
            string studyStatusViewFormParams = GetstudyStatusViewFormParams();
            var bytes = Encoding.ASCII.GetBytes(studyStatusViewFormParams);

            var studyStatusViewRequest = AutomationHelper.CreatePost(studyStatusViewUrl, cookieJar);
            studyStatusViewRequest.Referer = preferences.PanelAdminUrl + "HomeView.aspx";
            studyStatusViewRequest.ContentLength = bytes.Length;

            using (Stream os = studyStatusViewRequest.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }

            var response = (HttpWebResponse)studyStatusViewRequest.GetResponse();
            string cookies = response.Headers["Set-Cookie"];
            string pageSource = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                pageSource = reader.ReadToEnd();
            }

            cookieJar.SourceCode = pageSource;
            cookieJar.VcAuthentication = AutomationHelper.GetVcAuthentication(cookies);
            cookieJar.UniqueRequestId = AutomationHelper.GetReqId(cookies);

            response.Close();
            return cookieJar;
        }

        private static string GetstudyStatusViewFormParams()
        {
            const string eventTarget = "sTabs";
            const string eventArgument = "sTabs$ctl02";
            const string vcViewState = "1";
            string viewState = String.Empty;
            const string ctlCurrentXPos = "0";
            const string ctlCurrentYPos = "0";
            const string loggingOut = "false"; 
            string oPersistObject_FormElement = String.Empty;
            const string nmPick = "System%2FStudyQuestionnaire";
            string ctl_NM_ContextData = String.Empty;
            string ctl_onlineHelpMenu = String.Empty;
            const string stabs = "%7B%22State%22%3A%7B%22SelectedIndex%22%3A2%7D%2C%22TabState%22%3A%7B%22sTabs_ctl00%22%3A%7B%22Selected%22%3Afalse%7D%2C%22sTabs_ctl02%22%3A%7B%22Selected%22%3Atrue%7D%7D%7D";

            var builder = new StringBuilder();
            builder.Append("__EVENTTARGET=" + eventTarget);
            builder.Append("&__EVENTARGUMENT=" + eventArgument);
            builder.Append("&__VisionCriticalVIEWSTATE=" + vcViewState);
            builder.Append("&__VIEWSTATE=" + viewState);
            builder.Append("&ctl08%24CurrentXPos=" + ctlCurrentXPos);
            builder.Append("&ctl08%24CurrentYPos=" + ctlCurrentYPos);
            builder.Append("&LogginOut=" + loggingOut);
            builder.Append("&oPersistObject_FormElement=" + oPersistObject_FormElement);
            builder.Append("&nmPick=" + nmPick);
            builder.Append("&ctl08_NM_ContextData=" + ctl_NM_ContextData);
            builder.Append("&ctl08_OnlineHelpCtxMenu_ContextData=" + ctl_onlineHelpMenu);
            builder.Append("&sTabs=" + stabs);

            return builder.ToString();

        }
    }
}