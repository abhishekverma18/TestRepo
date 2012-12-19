using System;
using System.IO;
using System.Net;
using System.Text;

namespace ProvisioningPrototype.Web_Automation
{
    public class PanelSettingsManagement
    {
        public static void PanelSettingsUpdatePost(ContextCollection collection, PanelPreferences preferences)
        {
            var panelSettingsViewUrl = new Uri(preferences.PanelAdminUrl + "PanelSettingsManagementView.aspx");
            string panelSettingsViewFormParams = GetPanelSettingsViewFormParams(collection.GetFormString());
            var bytes = Encoding.ASCII.GetBytes(panelSettingsViewFormParams);

            var panelSettingsRequest = AutomationHelper.CreatePost(panelSettingsViewUrl, preferences.CookieJar);
            panelSettingsRequest.Referer = preferences.PanelAdminUrl + "PanelSettingsManagementView.aspx";
            panelSettingsRequest.ContentLength = bytes.Length;

            using (Stream os = panelSettingsRequest.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }


            var response = (HttpWebResponse)panelSettingsRequest.GetResponse();
            string cookies = response.Headers["Set-Cookie"];
            preferences.CookieJar.VcAuthentication = AutomationHelper.GetVcAuthentication(cookies);
            preferences.CookieJar.UniqueRequestId = AutomationHelper.GetReqId(cookies);

            response.Close();
        }

        private static string GetPanelSettingsViewFormParams(string formString)
        {
            const string eventTarget = "SaveButton";
            string eventArgument = String.Empty;
            const string vcViewState = "2";
            string viewState = String.Empty;
            const string ctlCurrentXPos = "0";
            const string ctlCurrentYPos = "0";
            const string loggingOut = "false";
            string oPersistObject_FormElement = String.Empty;
            string nmPick = String.Empty;
            string ctl02_NM_ContextData = String.Empty;
            string ctl02_OnlineHelpCtxMenu_ContextData = String.Empty;

            string formParameters = String.Format("__EVENTTARGET={0}&__EVENTARGUMENT={1}&__VisionCriticalVIEWSTATE={2}&__VIEWSTATE={3}&ctl02%24CurrentXPos={4}&ctl02%24CurrentXPos={5}&LoggingOut={6}&oPersistObject_FormElement={7}&nmPick={8}&ctl02_NM_ContextData={9}&ctl02_OnlineHelpCtxMenu_ContextData={10}", eventTarget, eventArgument, vcViewState, viewState, ctlCurrentXPos, ctlCurrentYPos, loggingOut, oPersistObject_FormElement, nmPick, ctl02_NM_ContextData, ctl02_OnlineHelpCtxMenu_ContextData);
            formParameters += "&" + formString;
            return formParameters;
        }

        public static void PanelSettingsPostToAssetManager(PanelPreferences preferences)
        {
            var panelSettingsViewUrl = new Uri(preferences.PanelAdminUrl + "PanelSettingsManagementView.aspx");
            string panelSettingsViewFormParams = GetPanelSettingsToAssetManagerFormParams();
            var bytes = Encoding.ASCII.GetBytes(panelSettingsViewFormParams);

            var homeViewRequest = AutomationHelper.CreatePost(panelSettingsViewUrl, preferences.CookieJar);
            homeViewRequest.Referer = preferences.PanelAdminUrl + "PanelSettingsManagementView.aspx";
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

            preferences.CookieJar.UniqueRequestId = AutomationHelper.GetReqId(cookies);

            response.Close();
        }

        private static string GetPanelSettingsToAssetManagerFormParams()
        {
            const string eventTarget = "ctl02%24NM";
            const string eventArgument = "p_AssetManager";
            const string vcViewState = "5";
            string viewState = String.Empty;
            const string ctlCurrentXPos = "0";
            const string ctlCurrentYPos = "0";
            const string loggingOut = "false";
            string oPersistObject_FormElement = String.Empty;
            const string nmPick = "System%2FAssetManager";
            string ctl_NM_ContextData = String.Empty;
            string ctl_OnlineHelpCtxMenu_ContextData = String.Empty;

            string formParameters = String.Format("__EVENTTARGET={0}&__EVENTARGUMENT={1}&__VisionCriticalVIEWSTATE={2}&__VIEWSTATE={3}&ctl02%24CurrentXPos={4}&ctl02%24CurrentYPos={5}&LoggingOut={6}&oPersistObject_FormElement={7}&nmPick={8}&ctl02_NM_ContextData={9}&ctl02_OnlineHelpCtxMenu_ContextData={10}", eventTarget, eventArgument, vcViewState, viewState, ctlCurrentXPos, ctlCurrentYPos, loggingOut, oPersistObject_FormElement, nmPick, ctl_NM_ContextData, ctl_OnlineHelpCtxMenu_ContextData);
            return formParameters;
        }


        private static string GetPanelSettingsToHomeViewFormParams()
        {
            const string eventTarget = "ctl02%24NM";
            const string eventArgument = "p0";
            const string vcViewState = "5";
            string viewState = String.Empty;
            const string ctlCurrentXPos = "0";
            const string ctlCurrentYPos = "0";
            const string loggingOut = "false";
            string oPersistObject_FormElement = String.Empty;
            const string nmPick = "Home";
            string ctl_NM_ContextData = String.Empty;
            string ctl_OnlineHelpCtxMenu_ContextData = String.Empty;

            string formParameters = String.Format("__EVENTTARGET={0}&__EVENTARGUMENT={1}&__VisionCriticalVIEWSTATE={2}&__VIEWSTATE={3}&ctl02%24CurrentXPos={4}&ctl02%24CurrentYPos={5}&LoggingOut={6}&oPersistObject_FormElement={7}&nmPick={8}&ctl02_NM_ContextData={9}&ctl02_OnlineHelpCtxMenu_ContextData={10}", eventTarget, eventArgument, vcViewState, viewState, ctlCurrentXPos, ctlCurrentYPos, loggingOut, oPersistObject_FormElement, nmPick, ctl_NM_ContextData, ctl_OnlineHelpCtxMenu_ContextData);
            return formParameters;
        }



        public static void PanelSettingsPostToHome(PanelPreferences preferences)
        {
            var homeViewUrl = new Uri(preferences.PanelAdminUrl + "PanelSettingsManagementView.aspx");
            string panelSettingsViewFormParams = GetPanelSettingsToHomeViewFormParams();
            var bytes = Encoding.ASCII.GetBytes(panelSettingsViewFormParams);

            var homeViewRequest = AutomationHelper.CreatePost(homeViewUrl, preferences.CookieJar);
            homeViewRequest.Referer = preferences.PanelAdminUrl + "PanelSettingsManagementView.aspx";
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

            preferences.CookieJar.UniqueRequestId = AutomationHelper.GetReqId(cookies);

            response.Close();
            
        }
    }
}