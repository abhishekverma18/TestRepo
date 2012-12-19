using System;
using System.IO;
using System.Net;
using System.Text;

namespace ProvisioningPrototype.Web_Automation
{
    public class StudyDeployment
    {
        public static CookieJar DeploymentsPostToAnonymousLinkView(CookieJar cookieJar, PanelPreferences preferences)
        {
            var studyDeploymentViewUrl = new Uri(preferences.PanelAdminUrl + "StudyDeploymentView.aspx");
            string deploymentToAnonymousFormParams = GetDeploymentToAnonymousFormParams();
            var bytes = Encoding.ASCII.GetBytes(deploymentToAnonymousFormParams);

            var questionnaireViewToDeployRequest = AutomationHelper.CreatePost(studyDeploymentViewUrl,cookieJar);
            questionnaireViewToDeployRequest.Referer = preferences.PanelAdminUrl + "HomeView.aspx";
            questionnaireViewToDeployRequest.ContentLength = bytes.Length;
            questionnaireViewToDeployRequest.Headers.Add("Cookie", "ASP.NET_SessionId=" + cookieJar.AspNetSessionId + "; .VCPanelAuth=" + cookieJar.VcAuthentication + "; " + ".reqid=" + cookieJar.UniqueRequestId + "; " + " .vcmach=" + cookieJar.MachineId);

            using (Stream os = questionnaireViewToDeployRequest.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }

            var response = (HttpWebResponse)questionnaireViewToDeployRequest.GetResponse();
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

        private static string GetDeploymentToAnonymousFormParams()
        {
            const string eventTarget = "AnonymousGrid%24ctl02%24ctl00";
            string eventArgument = String.Empty;
            string lastFocus = String.Empty;
            const string vcViewState = "77";
            string viewState = String.Empty;
            const string ctlCurrentXPos = "0";
            const string ctlCurrentYPos = "0";
            const string loggingOut = "false";
            string oPersistObject_FormElement = String.Empty;
            string nmPick = String.Empty;
            string ctl_NM_ContextData = String.Empty;
            string ctl_onlineHelpMenu = String.Empty;
            const string stabs = "%7B%22State%22%3A%7B%7D%2C%22TabState%22%3A%7B%22sTabs_ctl06%22%3A%7B%22Selected%22%3Atrue%7D%7D%7D";
            const string input = "Panel Sample";
            const string value = "Panel";
            const string text = "Panel Sample";
            string width = String.Empty;
            string height = String.Empty;

            StringBuilder builder = new StringBuilder();
            builder.Append("__EVENTTARGET=" + eventTarget);
            builder.Append("&__EVENTARGUMENT=" + eventArgument);
            builder.Append("&__LASTFOCUS=" + lastFocus);
            builder.Append("&__VisionCriticalVIEWSTATE=" + vcViewState);
            builder.Append("&__VIEWSTATE=" + viewState);
            builder.Append("&ctl09%24CurrentXPos=" + ctlCurrentXPos);
            builder.Append("&ctl09%24CurrentYPos=" + ctlCurrentYPos);
            builder.Append("&LogginOut=" + loggingOut);
            builder.Append("&oPersistObject_FormElement=" + oPersistObject_FormElement);
            builder.Append("&nmPick=" + nmPick);
            builder.Append("&ctl09_NM_ContextData=" + ctl_NM_ContextData);
            builder.Append("&ctl09_OnlineHelpCtxMenu_ContextData=" + ctl_onlineHelpMenu);
            builder.Append("&sTabs=" + stabs);
            builder.Append("&SampleTypeDropDown_Input" + input);
            builder.Append("&SampleTypeDropDown_value" + value);
            builder.Append("&SampleTypeDropDown_text" + text);
            builder.Append("&SampleTypeDropDown_clientWidth" + width);
            builder.Append("&SampleTypeDropDown_clientHeight" + height);


            return builder.ToString();
        }
    }
}