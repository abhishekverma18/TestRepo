using System;
using System.IO;
using System.Net;
using System.Text;

namespace ProvisioningPrototype.Web_Automation
{
    public class StudyQuestionnaire
    {
        public static CookieJar StudyQuestionnairePostToDeployments(CookieJar cookieJar, PanelPreferences preferences)
        {
            var studyQuestionnaireViewUrl = new Uri(preferences.PanelAdminUrl + "StudyQuestionnaireView.aspx");

            string questionnaireViewToDeployFormParams = GetQuestionnaireViewToDeployFormParams(studyQuestionnaireViewUrl, cookieJar.SourceCode);
            var bytes = Encoding.ASCII.GetBytes(questionnaireViewToDeployFormParams);

            var questionnaireViewToDeployRequest = AutomationHelper.CreatePost(studyQuestionnaireViewUrl, cookieJar);
            questionnaireViewToDeployRequest.Referer = preferences.PanelAdminUrl + "HomeView.aspx";
            questionnaireViewToDeployRequest.ContentLength = bytes.Length;

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


            cookieJar.VcAuthentication = AutomationHelper.GetVcAuthentication(cookies);
            cookieJar.UniqueRequestId = AutomationHelper.GetReqId(cookies);

            response.Close();
            return cookieJar;
        }

        private static string GetQuestionnaireViewToDeployFormParams(Uri url, string source)
        {
            StringBuilder builder = new StringBuilder();
            const string eventTarget = "sTabs";
            const string eventArgument = "sTabs%24ctl06";
            const string vcViewState = "71";
            string viewState = String.Empty;
            const string currentXPos = "0";
            const string currentYPos = "0";
            const string loggingOut = "false";
            string oPersistObject_FormElement = String.Empty;
            string nmPick = String.Empty;
            string nmContextData = String.Empty;
            string onlineHelpCtxMenu = String.Empty;
            string sTabs = Res.sTabs_Deploy;
            string radEContentTextarea = String.Empty;
            string eeR = String.Empty;
            string eeE2 = String.Empty;
            string pipesTreeData = AutomationHelper.GetPipesTreeData(url);
            string pipesTreeProperties = AutomationHelper.GetPipesTreeProperties();
            string pipesTreeSelectedNode = String.Empty;
            const string pipesTreeScrollData = "0%2C0";
            string ehid = String.Empty;
            string treeToolBarItemStorage = AutomationHelper.GetItemStorage();
            string treeToolBarProperties = AutomationHelper.GetProperties();
            string treeToolBarCheckedItems = String.Empty;
            string libraryMenu = String.Empty;
            string stepMenu = String.Empty;
            string duplicateMenu = String.Empty;
            string deleteMenu = String.Empty;
            string quickTestMenu = String.Empty;
            const string lddValue = "1";
            const string lddText = "English+%28US%29+-+Default";
            string lddWidth = String.Empty;
            string lddHeight = String.Empty;
            string addordionClientState = String.Empty;
            const string searchBoxHf = "True";
            const string searchBoxTb = "Search+for+Questions+...";
            string wTtData = AutomationHelper.GetwTtData(source);
            string wTtProperties = AutomationHelper.GetWTtProperties();
            string wTtSelectedNode = String.Empty;
            const string wTtScrollData = "0%2C0";
            string csTreeCommand = String.Empty;
            string wCMsContextData = String.Empty;
            string wMMContextData = String.Empty;
            string wfTreeClientCommand = String.Empty;
            string qTreeClientCommand = String.Empty;
            string libraryDivHiddenLibrary = String.Empty;
            string libraryITData = AutomationHelper.GetLibraryLTData();
            string libraryITProperties = AutomationHelper.GetLibraryLTProperties();
            string libraryITSelectedNode = String.Empty;
            const string libraryITScrollData = "0%2C0";
            string librarywLCM = String.Empty;
            string batchEditorTB = String.Empty;
            string workFlowTree = String.Empty;
            string imagePickerDialogImageCropperDialog = String.Empty;
            string imagePickerDialogDialogOpener = String.Empty;
            string imagePickerDialogDialogOpenerClientState = String.Empty;
            string documentPickerWindow = String.Empty;
            string documentPicker = String.Empty;

            builder.Append("__EVENTTARGET=" + eventTarget + "&");
            builder.Append("__EVENTARGUMENT=" + eventArgument + "&");
            builder.Append("__VisionCriticalVIEWSTATE=" + vcViewState + "&");
            builder.Append("__VIEWSTATE=" + viewState + "&");
            builder.Append("ctl08%24CurrentXPos=" + currentXPos + "&");
            builder.Append("ctl08%24CurrentYPos=" + currentYPos + "&");
            builder.Append("LoggingOut=" + loggingOut + "&");
            builder.Append("oPersistObject_FormElement=" + oPersistObject_FormElement + "&");
            builder.Append("nmPick=" + nmPick + "&");
            builder.Append("ctl08_NM_ContextData=" + nmContextData + "&");
            builder.Append("ctl08_OnlineHelpCtxMenu_ContextData=" + onlineHelpCtxMenu + "&");
            builder.Append("sTabs=" + sTabs + "&");
            builder.Append("RadEContentTextareae_eR=" + radEContentTextarea + "&");
            builder.Append("e%24eR=" + eeR + "&");
            builder.Append("e%24eE2=" + eeE2 + "&");
            builder.Append("e_pipesDiv_pipesTree_Data=" + pipesTreeData + "&");
            builder.Append("e_pipesDiv_pipesTree_Properties=" + pipesTreeProperties + "&");
            builder.Append("e_pipesDiv_pipesTree_SelectedNode=" + pipesTreeSelectedNode + "&");
            builder.Append("e_pipesDiv_pipesTree_ScrollData=" + pipesTreeScrollData + "&");
            builder.Append("e%24ehid=" + ehid + "&");
            builder.Append("treeToolBar_ItemStorage=" + treeToolBarItemStorage + "&");
            builder.Append("treeToolBar_Properties=" + treeToolBarProperties + "&");
            builder.Append("treeToolBar_CheckedItems=" + treeToolBarCheckedItems + "&");
            builder.Append("libraryMenu=" + libraryMenu + "&");
            builder.Append("stepMenu=" + stepMenu + "&");
            builder.Append("duplicateMenu=" + duplicateMenu + "&");
            builder.Append("deleteMenu=" + deleteMenu + "&");
            builder.Append("QuickTestMenu=" + quickTestMenu + "&");
            builder.Append("LanguageDropDown_value=" + lddValue + "&");
            builder.Append("LanguageDropDown_text=" + lddText + "&");
            builder.Append("LanguageDropDown_clientWidth=" + lddWidth + "&");
            builder.Append("LanguageDropDown_clientHeight=" + lddHeight + "&");
            builder.Append("accordion_AccordionExtender_ClientState=" + addordionClientState + "&");
            builder.Append("ctl10%24SearchBox%24dHf=" + searchBoxHf + "&");
            builder.Append("ctl10%24SearchBox%24sTb=" + searchBoxTb + "&");
            builder.Append("ctl10_wT_wTt_Data=" + wTtData + "&");
            builder.Append("ctl10_wT_wTt_Properties=" + wTtProperties + "&");
            builder.Append("ctl10_wT_wTt_SelectedNode=" + wTtSelectedNode + "&");
            builder.Append("ctl10_wT_wTt_ScrollData=" + wTtScrollData + "&");
            builder.Append("ctl10%24wT%24ClientSideTreeCommand_ctl10%24wT%24ctl00=" + csTreeCommand + "&");
            builder.Append("ctl10_wT_wCMs_ContextData=" + wCMsContextData + "&");
            builder.Append("ctl10_wT_wMM_ContextData=" + wMMContextData + "&");
            builder.Append("workflowTreeClientCommand=" + wfTreeClientCommand + "&");
            builder.Append("questionTreeClientCommand=" + qTreeClientCommand + "&");
            builder.Append("LibraryDiv%24hiddenLibraryClickEvent=" + libraryDivHiddenLibrary + "&");
            builder.Append("LibraryDiv_lT_Data=" + libraryITData + "&");
            builder.Append("LibraryDiv_lT_Properties=" + libraryITProperties + "&");
            builder.Append("LibraryDiv_lT_SelectedNode=" + libraryITSelectedNode + "&");
            builder.Append("LibraryDiv_lT_ScrollData=" + libraryITScrollData + "&");
            builder.Append("LibraryDiv_wLCM_ContextData=" + librarywLCM + "&");
            builder.Append("batchEditorTextBox=" + batchEditorTB + "&");
            builder.Append("WorkflowTreeShowHidden=" + workFlowTree + "&");
            builder.Append("imagePickerDialog_imageCropperDialog_ClientState=" + imagePickerDialogImageCropperDialog + "&");
            builder.Append("imagePickerDialog_dialogOpener_Window_ClientState=" + imagePickerDialogDialogOpener + "&");
            builder.Append("imagePickerDialog_dialogOpener_ClientState=" + imagePickerDialogDialogOpenerClientState + "&");
            builder.Append("documentPickerDialog_dialogOpener_Window_ClientState=" + documentPickerWindow + "&");
            builder.Append("documentPickerDialog_dialogOpener_ClientState=" + documentPicker);
            return builder.ToString();
        }

        public static CookieJar ValidateStudyPost(CookieJar cookieJar, PanelPreferences preferences)
        {
            var studyQuestionnaireViewUrl = new Uri(preferences.PanelAdminUrl + "StudyQuestionnaireView.aspx");
            string questionnaireViewFormParams = GetQuestionnaireViewFormParams(studyQuestionnaireViewUrl, cookieJar.SourceCode);
            var bytes = Encoding.ASCII.GetBytes(questionnaireViewFormParams);

            var questionnaireViewRequest = AutomationHelper.CreatePost(studyQuestionnaireViewUrl, cookieJar);
            questionnaireViewRequest.Referer = preferences.PanelAdminUrl + "HomeView.aspx";
            questionnaireViewRequest.ContentLength = bytes.Length;

            questionnaireViewRequest.Headers.Add("Cookie", "ASP.NET_SessionId=" + cookieJar.AspNetSessionId + "; .VCPanelAuth=" + cookieJar.VcAuthentication + "; " + ".reqid=" + cookieJar.UniqueRequestId + "; " + " .vcmach=" + cookieJar.MachineId);

            using (Stream os = questionnaireViewRequest.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }

            var response = (HttpWebResponse)questionnaireViewRequest.GetResponse();
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

        private static string GetQuestionnaireViewFormParams(Uri url, string source)
        {
            StringBuilder builder = new StringBuilder();
            const string eventTarget = "compile";
            string eventArgument = String.Empty;
            const string vcViewState = "35";
            string viewState = String.Empty;
            const string currentXPos = "0";
            const string currentYPos = "0";
            const string loggingOut = "false";
            string oPersistObject_FormElement = String.Empty;
            string nmPick = String.Empty;
            string nmContextData = String.Empty;
            string onlineHelpCtxMenu = String.Empty;
            string sTabs = AutomationHelper.GetSTabs();
            string radEContentTextarea = String.Empty;
            string eeR = String.Empty;
            string eeE2 = String.Empty;
            string pipesTreeData = AutomationHelper.GetPipesTreeData(url);
            string pipesTreeProperties = AutomationHelper.GetPipesTreeProperties();
            string pipesTreeSelectedNode = String.Empty;
            const string pipesTreeScrollData = "0%2C0";
            string ehid = String.Empty;
            string treeToolBarItemStorage = AutomationHelper.GetItemStorage();
            string treeToolBarProperties = AutomationHelper.GetProperties();
            string treeToolBarCheckedItems = String.Empty;
            string libraryMenu = String.Empty;
            string stepMenu = String.Empty;
            string duplicateMenu = String.Empty;
            string deleteMenu = String.Empty;
            const string lddValue = "1";
            const string lddText = "English+%28US%29+-+Default";  //TODO check if this will screw things up later
            string lddWidth = String.Empty;
            string lddHeight = String.Empty;
            string addordionClientState = String.Empty;
            const string searchBoxHf = "True";
            const string searchBoxTb = "Search+for+Questions+...";
            string wTtData = AutomationHelper.GetwTtData(source);
            string wTtProperties = AutomationHelper.GetWTtProperties();
            string wTtSelectedNode = String.Empty;
            const string wTtScrollData = "0%2C0";
            string csTreeCommand = String.Empty;
            string wCMsContextData = String.Empty;
            string wMMContextData = String.Empty;
            string wfTreeClientCommand = String.Empty;
            string qTreeClientCommand = String.Empty;
            string libraryDivHiddenLibrary = String.Empty;
            string libraryITData = AutomationHelper.GetLibraryLTData();
            string libraryITProperties = AutomationHelper.GetLibraryLTProperties();
            string libraryITSelectedNode = String.Empty;
            const string libraryITScrollData = "0%2C0";
            string librarywLCM = String.Empty;
            string batchEditorTB = String.Empty;
            string workFlowTree = String.Empty;
            string imagePickerDialogImageCropperDialog = String.Empty;
            string imagePickerDialogDialogOpener = String.Empty;
            string imagePickerDialogDialogOpenerClientState = String.Empty;
            string documentPickerWindow = String.Empty;
            string documentPicker = String.Empty;

            builder.Append("__EVENTTARGET=" + eventTarget + "&");
            builder.Append("__EVENTARGUMENT=" + eventArgument + "&");
            builder.Append("__VisionCriticalVIEWSTATE=" + vcViewState + "&");
            builder.Append("__VIEWSTATE=" + viewState + "&");
            builder.Append("ctl08%24CurrentXPos=" + currentXPos + "&");
            builder.Append("ctl08%24CurrentYPos=" + currentYPos + "&");
            builder.Append("LoggingOut=" + loggingOut + "&");
            builder.Append("oPersistObject_FormElement=" + oPersistObject_FormElement + "&");
            builder.Append("nmPick=" + nmPick + "&");
            builder.Append("ctl08_NM_ContextData=" + nmContextData + "&");
            builder.Append("ctl08_OnlineHelpCtxMenu_ContextData=" + onlineHelpCtxMenu + "&");
            builder.Append("sTabs=" + sTabs + "&");
            builder.Append("RadEContentTextareae_eR=" + radEContentTextarea + "&");
            builder.Append("e%24eR=" + eeR + "&");
            builder.Append("e%24eE2=" + eeE2 + "&");
            builder.Append("e_pipesDiv_pipesTree_Data=" + pipesTreeData + "&");
            builder.Append("e_pipesDiv_pipesTree_Properties=" + pipesTreeProperties + "&");
            builder.Append("e_pipesDiv_pipesTree_SelectedNode=" + pipesTreeSelectedNode + "&");
            builder.Append("e_pipesDiv_pipesTree_ScrollData=" + pipesTreeScrollData + "&");
            builder.Append("e%24ehid=" + ehid + "&");
            builder.Append("treeToolBar_ItemStorage=" + treeToolBarItemStorage + "&");
            builder.Append("treeToolBar_Properties=" + treeToolBarProperties + "&");
            builder.Append("treeToolBar_CheckedItems=" + treeToolBarCheckedItems + "&");
            builder.Append("libraryMenu=" + libraryMenu + "&");
            builder.Append("stepMenu=" + stepMenu + "&");
            builder.Append("duplicateMenu=" + duplicateMenu + "&");
            builder.Append("deleteMenu=" + deleteMenu + "&");
            builder.Append("LanguageDropDown_value=" + lddValue + "&");
            builder.Append("LanguageDropDown_text=" + lddText + "&");
            builder.Append("LanguageDropDown_clientWidth=" + lddWidth + "&");
            builder.Append("LanguageDropDown_clientHeight=" + lddHeight + "&");
            builder.Append("accordion_AccordionExtender_ClientState=" + addordionClientState + "&");
            builder.Append("ctl10%24SearchBox%24dHf=" + searchBoxHf + "&");
            builder.Append("ctl10%24SearchBox%24sTb=" + searchBoxTb + "&");
            builder.Append("ctl10_wT_wTt_Data=" + wTtData + "&");
            builder.Append("ctl10_wT_wTt_Properties=" + wTtProperties + "&");
            builder.Append("ctl10_wT_wTt_SelectedNode=" + wTtSelectedNode + "&");
            builder.Append("ctl10_wT_wTt_ScrollData=" + wTtScrollData + "&");
            builder.Append("ctl10%24wT%24ClientSideTreeCommand_ctl10%24wT%24ctl00=" + csTreeCommand + "&");
            builder.Append("ctl10_wT_wCMs_ContextData=" + wCMsContextData + "&");
            builder.Append("ctl10_wT_wMM_ContextData=" + wMMContextData + "&");
            builder.Append("workflowTreeClientCommand=" + wfTreeClientCommand + "&");
            builder.Append("questionTreeClientCommand=" + qTreeClientCommand + "&");
            builder.Append("LibraryDiv%24hiddenLibraryClickEvent=" + libraryDivHiddenLibrary + "&");
            builder.Append("LibraryDiv_lT_Data=" + libraryITData + "&");
            builder.Append("LibraryDiv_lT_Properties=" + libraryITProperties + "&");
            builder.Append("LibraryDiv_lT_SelectedNode=" + libraryITSelectedNode + "&");
            builder.Append("LibraryDiv_lT_ScrollData=" + libraryITScrollData + "&");
            builder.Append("LibraryDiv_wLCM_ContextData=" + librarywLCM + "&");
            builder.Append("batchEditorTextBox=" + batchEditorTB + "&");
            builder.Append("WorkflowTreeShowHidden=" + workFlowTree + "&");
            builder.Append("imagePickerDialog_imageCropperDialog_ClientState=" + imagePickerDialogImageCropperDialog + "&");
            builder.Append("imagePickerDialog_dialogOpener_Window_ClientState=" + imagePickerDialogDialogOpener + "&");
            builder.Append("imagePickerDialog_dialogOpener_ClientState=" + imagePickerDialogDialogOpenerClientState + "&");
            builder.Append("documentPickerDialog_dialogOpener_Window_ClientState=" + documentPickerWindow + "&");
            builder.Append("documentPickerDialog_dialogOpener_ClientState=" + documentPicker);



            return builder.ToString();
        }

    }
}