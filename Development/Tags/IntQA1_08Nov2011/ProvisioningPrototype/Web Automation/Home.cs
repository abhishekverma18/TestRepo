using System;
using System.IO;
using System.Net;
using System.Text;

namespace ProvisioningPrototype.Web_Automation
{
    public class Home
    {
        public static CookieJar HomeViewGet(CookieJar cookieJar, PanelPreferences preferences)
        {
            var client = new WebClient();

            client.Headers.Add("Pragma", "no-cache");
            client.Headers.Add("Accept", "text/html, application/xhtml+xml, */*");
            client.Headers.Add("Accept-Encoding", "gzip, deflate");
            client.Headers.Add("Accept-Language", "en-US");
            client.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
            client.Headers.Add("Referer", preferences.PanelAdminUrl + "HomeView.aspx");

            client.Headers.Add("Cookie", "ASP.NET_SessionId=" + cookieJar.AspNetSessionId + "; .VCPanelAuth=" + cookieJar.VcAuthentication + ";");

            var source = client.DownloadString(preferences.PanelAdminUrl + "ChangePasswordView.aspx");

            string cookies = client.ResponseHeaders["Set-Cookie"];


            cookieJar.MachineId = AutomationHelper.GetVcMach(cookies);
            cookieJar.VcAuthentication = AutomationHelper.GetVcAuthentication(cookies);
            cookieJar.UniqueRequestId = AutomationHelper.GetReqId(cookies);

            client.Dispose();

            return cookieJar;
        }

        public static CookieJar HomeViewPostToPanelSettingsManager(CookieJar cookieJar, PanelPreferences preferences)
        {
            //Added for Panel settings will be fecthed from UI not from WebConfig
            var homeViewUrl = new Uri(preferences.PanelAdminUrl + "HomeView.aspx");
            string homeViewFormParams = GetHomeViewFormParams();
            var bytes = Encoding.ASCII.GetBytes(homeViewFormParams);

            var homeViewRequest = AutomationHelper.CreatePost(homeViewUrl, cookieJar);
            homeViewRequest.Referer = preferences.PanelAdminUrl + "HomeView.aspx";
            homeViewRequest.ContentLength = bytes.Length;

            using (Stream os = homeViewRequest.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }

            var response = (HttpWebResponse)homeViewRequest.GetResponse();
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

        private static string GetHomeViewFormParams()
        {
            const string eventTarget = "ctl03%24NM";
            const string eventArgument = "p_PanelSettings";
            const string vcViewState = "1";
            string viewState = String.Empty;
            const string ctlCurrentXPos = "0";
            const string ctlCurrentYPos = "0";
            const string loggingOut = "true";
            string oPersistObject_FormElement = String.Empty;
            const string nmPick = "System%2FPanelSettings";
            string ctl_NM_ContextData = String.Empty;
            string ctl_OnlineHelpCtxMenu_ContextData = String.Empty;
            const string studyStateHiddenField = "7";
            const string projectManagerDropDown_Input = "All";
            string projectManagerDropDown_Value = String.Empty;
            const string projectManagerDropDown_text = "All";
            string pmDropDown_clientWidth = String.Empty;
            string pmDropDown_clientHeight = String.Empty;
            const string searchBoxdHf = "True";
            const string searchBoxsTb = "Search+for+Studies+...";
            string cv0 = String.Empty;
            string cv1 = String.Empty;
            string cv2 = String.Empty;
            string cvMenu = String.Empty;

            string formParameters = String.Format("__EVENTTARGET={0}&__EVENTARGUMENT={1}&__VisionCriticalVIEWSTATE={2}&__VIEWSTATE={3}&ctl03%24CurrentXPos={4}&ctl03%24CurrentYPos={5}&LoggingOut={6}&oPersistObject_FormElement={7}&nmPick={8}&ctl03_NM_ContextData={9}&ctl03_OnlineHelpCtxMenu_ContextData={10}&StudyStateHiddenField={11}&ProjectManagerDropDown_Input={12}&ProjectManagerDropDown_value={13}&ProjectManagerDropDown_text={14}&ProjectManagerDropDown_clientWidth={15}&ProjectManagerDropDown_clientHeight={16}&SearchBox%24dHf={17}&SearchBox%24sTb={18}&cv_cv_ctl00_cvi_ClientState={19}&cv_cv_ctl01_cvi_ClientState={20}&cv_cv_ctl02_cvi_ClientState={21}&cv_ctxMenu_ContextData={22}", eventTarget, eventArgument, vcViewState, viewState, ctlCurrentXPos, ctlCurrentYPos, loggingOut, oPersistObject_FormElement, nmPick, ctl_NM_ContextData, ctl_OnlineHelpCtxMenu_ContextData, studyStateHiddenField, projectManagerDropDown_Input, projectManagerDropDown_Value, projectManagerDropDown_text, pmDropDown_clientWidth, pmDropDown_clientHeight, searchBoxdHf, searchBoxsTb, cv0, cv1, cv2, cvMenu);
            return formParameters;
        }

        public static CookieJar HomeViewPostToImportNewStudyView(CookieJar cookieJar, PanelPreferences preferences)
        {
            //Added for Panel settings will be fecthed from UI not from WebConfig    
            var homeViewUrl = new Uri(preferences.PanelAdminUrl + "HomeView.aspx");
            string homeViewFormParams = GetHomeViewToNewStudyFormParams();
            var bytes = Encoding.ASCII.GetBytes(homeViewFormParams);

            var homeViewRequest = AutomationHelper.CreatePost(homeViewUrl, cookieJar);

            //Added for Panel settings will be fecthed from UI not from WebConfig 
            homeViewRequest.Referer = preferences.PanelAdminUrl + "HomeView.aspx";
            homeViewRequest.ContentLength = bytes.Length;
            homeViewRequest.Headers.Add("Cookie", "ASP.NET_SessionId=" + cookieJar.AspNetSessionId + "; .VCPanelAuth=" + cookieJar.VcAuthentication + "; " + ".reqid=" + cookieJar.UniqueRequestId + "; " + " .vcmach=" + cookieJar.MachineId);

            using (Stream os = homeViewRequest.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }

            var response = (HttpWebResponse)homeViewRequest.GetResponse();
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

        private static string GetHomeViewToNewStudyFormParams()
        {
            const string eventTarget = "ImportStudyButton";
            const string eventArgument = "";
            const string vcViewState = "53";
            string viewState = String.Empty;
            const string ctlCurrentXPos = "0";
            const string ctlCurrentYPos = "0";
            const string loggingOut = "false";
            string oPersistObject_FormElement = String.Empty;
            string nmPick = String.Empty;
            string ctl_NM_ContextData = String.Empty;
            string ctl_OnlineHelpCtxMenu_ContextData = String.Empty;
            const string studyStateHiddenField = "7";
            const string projectManagerDropDown_Input = "All";
            string projectManagerDropDown_Value = String.Empty;
            const string projectManagerDropDown_text = "All";
            string pmDropDown_clientWidth = String.Empty;
            string pmDropDown_clientHeight = String.Empty;
            const string searchBoxdHf = "True";
            const string searchBoxsTb = "Search+for+Studies+...";
            string cv0 = String.Empty;
            string cv1 = String.Empty;
            string cv2 = String.Empty;
            string cvMenu = String.Empty;

            string formParameters = String.Format("__EVENTTARGET={0}&__EVENTARGUMENT={1}&__VisionCriticalVIEWSTATE={2}&__VIEWSTATE={3}&ctl03%24CurrentXPos={4}&ctl03%24CurrentYPos={5}&LoggingOut={6}&oPersistObject_FormElement={7}&nmPick={8}&ctl03_NM_ContextData={9}&ctl03_OnlineHelpCtxMenu_ContextData={10}&StudyStateHiddenField={11}&ProjectManagerDropDown_Input={12}&ProjectManagerDropDown_value={13}&ProjectManagerDropDown_text={14}&ProjectManagerDropDown_clientWidth={15}&ProjectManagerDropDown_clientHeight={16}&SearchBox%24dHf={17}&SearchBox%24sTb={18}&cv_cv_ctl00_cvi_ClientState={19}&cv_cv_ctl01_cvi_ClientState={20}&cv_cv_ctl02_cvi_ClientState={21}&cv_ctxMenu_ContextData={22}", eventTarget, eventArgument, vcViewState, viewState, ctlCurrentXPos, ctlCurrentYPos, loggingOut, oPersistObject_FormElement, nmPick, ctl_NM_ContextData, ctl_OnlineHelpCtxMenu_ContextData, studyStateHiddenField, projectManagerDropDown_Input, projectManagerDropDown_Value, projectManagerDropDown_text, pmDropDown_clientWidth, pmDropDown_clientHeight, searchBoxdHf, searchBoxsTb, cv0, cv1, cv2, cvMenu);
            return formParameters;
        }
    }
}