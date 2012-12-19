using System.Web;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace ProvisioningPrototype.Web_Automation
{
    public class Communication
    {
        public static string AddNewEmailTemplate(PanelPreferences preferences, EmailTemplate emailTemplate)
        {
            var communicationViewUrl = new Uri(preferences.PanelAdminUrl + "CommunicationsManagementView.aspx");
            string communicationViewFormParams = GetCommunicationViewFormParams(emailTemplate);
            var bytes = Encoding.ASCII.GetBytes(communicationViewFormParams);
            var communicationsRequest = AutomationHelper.CreatePost(communicationViewUrl, preferences.CookieJar);
            communicationsRequest.Referer = preferences.PanelAdminUrl + "CommunicationsManagementView.aspx";
            communicationsRequest.ContentLength = bytes.Length;
            using (Stream os = communicationsRequest.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }
            var response = (HttpWebResponse)communicationsRequest.GetResponse();
            string pageSource = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                pageSource = reader.ReadToEnd();
            }
            string cookies = response.Headers["Set-Cookie"];
            preferences.CookieJar.VcAuthentication = AutomationHelper.GetVcAuthentication(cookies);
            preferences.CookieJar.UniqueRequestId = AutomationHelper.GetReqId(cookies);
            response.Close();
            return pageSource;
        }

        public static void EditNewEmailTemplate(PanelPreferences preferences, EmailTemplate emailTemplate, string eid)
        {
            var communicationViewUrl = new Uri(preferences.PanelAdminUrl + "EmailWindow.aspx");
            string communicationViewFormParams = GetEmailEditViewFormParams(emailTemplate, eid);
            var bytes = Encoding.ASCII.GetBytes(communicationViewFormParams);
            var communicationsRequest = AutomationHelper.CreatePost(communicationViewUrl, preferences.CookieJar);
            communicationsRequest.Referer = preferences.PanelAdminUrl + "EmailWindow.aspx";
            communicationsRequest.ContentLength = bytes.Length;
            using (Stream os = communicationsRequest.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }
            var response = (HttpWebResponse)communicationsRequest.GetResponse();
            string pageSource = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                pageSource = reader.ReadToEnd();
            }
            response.Close();

        }

        public static void SaveEmailTemplate(PanelPreferences preferences, EmailTemplate emailTemplate, string eid)
        {
            var communicationViewUrl = new Uri(preferences.PanelAdminUrl + "EmailWindow.aspx");
            string communicationViewFormParams = GetCommunicationSaveEmailViewFormParams(emailTemplate, eid);
            var bytes = Encoding.ASCII.GetBytes(communicationViewFormParams);
            var communicationsRequest = AutomationHelper.CreatePost(communicationViewUrl, preferences.CookieJar);
            communicationsRequest.Referer = preferences.PanelAdminUrl + "EmailWindow.aspx";
            communicationsRequest.ContentLength = bytes.Length;
            using (Stream os = communicationsRequest.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }
            var response = (HttpWebResponse)communicationsRequest.GetResponse();
            string pageSource = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                pageSource = reader.ReadToEnd();
            }
            response.Close();
        }

        public static void CommunicationPostToAssetManager(PanelPreferences preferences)
        {
            var communicationViewUrl = new Uri(preferences.PanelAdminUrl + "CommunicationsManagementView.aspx");
            string panelSettingsViewFormParams = GetCommunicationToAssetManagerFormParams();
            var bytes = Encoding.ASCII.GetBytes(panelSettingsViewFormParams);

            var homeViewRequest = AutomationHelper.CreatePost(communicationViewUrl, preferences.CookieJar);
            homeViewRequest.Referer = preferences.PanelAdminUrl + "CommunicationsManagementView.aspx";
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
        private static string GetCommunicationToAssetManagerFormParams()
        {
            const string eventTarget = "ctl02$NM";
            const string eventArgument = "p_AssetManager";
            const string vcViewState = "23";
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
        private static string GetCommunicationViewFormParams(EmailTemplate emailTemplate)
        {
            const string eventTarget = "AddEmailTemplateButton";
            string eventArgument = emailTemplate.EmailTemplateName;
            const string vcViewState = "15";
            string viewState = String.Empty;
            const string ctlCurrentXPos = "0";
            const string ctlCurrentYPos = "0";
            const string loggingOut = "false";
            string oPersistObject_FormElement = String.Empty;
            string nmPick = String.Empty;
            string ctl02_NM_ContextData = String.Empty;
            string ctl02_OnlineHelpCtxMenu_ContextData = String.Empty;
            string formParameters = String.Format("__EVENTTARGET={0}&__EVENTARGUMENT={1}&__VisionCriticalVIEWSTATE={2}&__VIEWSTATE={3}&ctl02%24CurrentXPos={4}&ctl02%24CurrentXPos={5}&LoggingOut={6}&oPersistObject_FormElement={7}&nmPick={8}&ctl02_NM_ContextData={9}&ctl02_OnlineHelpCtxMenu_ContextData={10}", eventTarget, eventArgument, vcViewState, viewState, ctlCurrentXPos, ctlCurrentYPos, loggingOut, oPersistObject_FormElement, nmPick, ctl02_NM_ContextData, ctl02_OnlineHelpCtxMenu_ContextData);
            return formParameters;
        }

        private static string GetEmailEditViewFormParams(EmailTemplate emailTemplate, string eid)
        {
            string callBackMethod = "ServerSideLoadEmailById";
            string Cart_Callback_Method_Param = eid;
            string formParameters = String.Format("Cart_Callback_Method={0}&Cart_Callback_Method_Param={1}", callBackMethod, Cart_Callback_Method_Param);
            return formParameters;
        }

        private static string GetCommunicationSaveEmailViewFormParams(EmailTemplate emailTemplate, string eid)
        {
            string callBackMethod = "ServerSideSaveEmail";
            string callBackMethodParam = "{\"Id\":" + eid + ",\"Name\":\"" + emailTemplate.EmailTemplateName + "\",\"IsTemplate\":true,\"ReadOnly\":false,\"MimeType\":\"multipart/alternative\",\"To\":\"\",\"From\":\"\",\"Subject1\":\"" + emailTemplate.EmailSubject + "\",\"Subject2\":\"\",\"Subject3\":\"\",\"Subject4\":\"\",\"Subject5\":\"\",\"Subject6\":\"\",\"Subject7\":\"\",\"Subject8\":\"\",\"Subject9\":\"\",\"Subject10\":\"\",\"Subject11\":\"\",\"Subject12\":\"\",\"Subject13\":\"\",\"Subject14\":\"\",\"Subject15\":\"\",\"Subject16\":\"\",\"Text1\":\"" + emailTemplate.EmailPlainText + "\",\"Text2\":\"\",\"Text3\":\"\",\"Text4\":\"\",\"Text5\":\"\",\"Text6\":\"\",\"Text7\":\"\",\"Text8\":\"\",\"Text9\":\"\",\"Text10\":\"\",\"Text11\":\"\",\"Text12\":\"\",\"Text13\":\"\",\"Text14\":\"\",\"Text15\":\"\",\"Text16\":\"\",\"Html1\":" + emailTemplate.EmailRichText + "\",\"Html2\":\"\",\"Html3\":\"\",\"Html4\":\"\",\"Html5\":\"\",\"Html6\":\"\",\"Html7\":\"\",\"Html8\":\"\",\"Html9\":\"\",\"Html10\":\"\",\"\"Html11\":\"\",\"Html12\":\"\",\"Html13\":\"\",\"Html14\":\"\",\"Html15\":\"\",\"Html16\":\"\",\"LanguageCulture1\":\"" + emailTemplate.EmailLanguage + "\",\"LanguageCulture2\":\"\",\"LanguageCulture3\":\"\",\"LanguageCulture4\":\"\",\"LanguageCulture5\":\"\",\"LanguageCulture6\":\"\",\"LanguageCulture7\":\"\",\"LanguageCulture8\":\"\",\"LanguageCulture9\":\"\",\"LanguageCulture10\":\"\",\"LanguageCulture11\":\"\",\"LanguageCulture12\":\"\",\"LanguageCulture13\":\"\",\"LanguageCulture14\":\"\",\"LanguageCulture15\":\"\",\"LanguageCulture16\":\"\",\"LanguageSpellCheck1\":\"True\",\"LanguageSpellCheck2\":\"False\",\"LanguageSpellCheck3\":\"False\",\"LanguageSpellCheck4\":\"False\",\"LanguageSpellCheck5\":\"False\",\"LanguageSpellCheck6\":\"False\",\"LanguageSpellCheck7\":\"False\",\"LanguageSpellCheck8\":\"False\",\"LanguageSpellCheck9\":\"False\",\"LanguageSpellCheck10\":\"False\",\"LanguageSpellCheck11\":\"False\",\"LanguageSpellCheck12\":\"False\",\"LanguageSpellCheck13\":\"False\",\"LanguageSpellCheck14\":\"False\",\"LanguageSpellCheck15\":\"False\",\"LanguageSpellCheck16\":\"False\",\"Subject\":\"\",\"Html\":\"\",\"Text\":\"\"}";
            string formParameters = String.Format("Cart_Callback_Method={0}&Cart_Callback_Method_Param={1}", callBackMethod, callBackMethodParam);
            return formParameters;
        }



    }
}