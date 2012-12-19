using System;
using System.IO;
using System.Net;
using System.Text;

namespace ProvisioningPrototype.Web_Automation
{
    public class AnonymousLink
    {
        public static CookieJar UpdateLanguageAndSkinPost(CookieJar cookieJar, string environment, PanelPreferences preferences)
        {
            if (environment.Length == 0 | environment == null)
            {
                throw new Exception("Invalid environment: " + environment);
            }

            var anonymousLinkViewUrl = new Uri(preferences.PanelAdminUrl + "AnonymousLinkView.aspx");

            //Passed new parameter preferences of PanelPreferences type for Language selection
            string anonymousLinkViewFormParams = GetUpdateLanguageAndSkinPostForms(environment, preferences);
            var bytes = Encoding.ASCII.GetBytes(anonymousLinkViewFormParams);

            var homeViewRequest = AutomationHelper.CreatePost(anonymousLinkViewUrl, cookieJar);
            homeViewRequest.Referer = preferences.PanelAdminUrl + "AnonymousLinkView.aspx";
            homeViewRequest.ContentLength = bytes.Length;


            using (Stream os = homeViewRequest.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }

            var response = (HttpWebResponse)homeViewRequest.GetResponse();
            string cookies = response.Headers["Set-Cookie"];
            string pageSource;
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


        private static string GetUpdateLanguageAndSkinPostForms(string environment, PanelPreferences preferences)
        {
            const string eventTarget = "ApplyButton";
            string eventArgument = String.Empty;
            const string vcViewState = "13";
            string viewState = String.Empty;
            const string ctlCurrentXPos = "0";
            const string ctlCurrentYPos = "0";
            const string loggingOut = "false";
            string oPersistObject_FormElement = String.Empty;
            string nmPick = String.Empty;
            string ctl_NM_ContextData = String.Empty;
            string ctl_onlineHelpMenu = String.Empty;
            const string stabs = "%7B%22State%22%3A%7B%7D%2C%22TabState%22%3A%7B%22sTabs_ctl06%22%3A%7B%22Selected%22%3Atrue%7D%7D%7D";
            string openDateValue = String.Empty;
            string closeDateValue = String.Empty;
            const string languageDropDownInput = "English+%28US%29+%28default%29";

            //Edited  for Language Selection 
            //string languageDropDownValue = "en-CA";           
            string languageDropDownValue = preferences.Language;
            const string languageDropDownText = "English+%28US%29+%28default%29";
            string languageDropDownClientWidth = String.Empty;
            string languageDropDownClientHeight = String.Empty;
            string environmentDropDownInput = environment;
            string environmentDropDownValue = String.Empty;
            string environmentDropDownText = environment;
            string environmentDropDownClientWidth = String.Empty;
            string environmentDropDownClientHeight = String.Empty;

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
            builder.Append("&OpenDate%24DateValue=" + openDateValue);
            builder.Append("&CloseDate%24DateValue=" + closeDateValue);
            builder.Append("&CascadingContext%24LanguageDropDown_Input=" + languageDropDownInput);
            builder.Append("&CascadingContext%24LanguageDropDown_value=" + languageDropDownValue);
            builder.Append("&CascadingContext%24LanguageDropDown_text=" + languageDropDownText);
            builder.Append("&CascadingContext%24LanguageDropDown_clientWidth=" + languageDropDownClientWidth);
            builder.Append("&CascadingContext%24LanguageDropDown_clientHeight=" + languageDropDownClientHeight);
            builder.Append("&CascadingContext%24EnvironmentDropDown_Input=" + environmentDropDownInput);
            builder.Append("&CascadingContext%24EnvironmentDropDown_value=" + environmentDropDownValue);
            builder.Append("&CascadingContext%24EnvironmentDropDown_text=" + environmentDropDownText);
            builder.Append("&CascadingContext%24EnvironmentDropDown_clientWidth=" + environmentDropDownClientWidth);
            builder.Append("&CascadingContext%24EnvironmentDropDown_clientHeight=" + environmentDropDownClientHeight);


            return builder.ToString();
        }
    }
}