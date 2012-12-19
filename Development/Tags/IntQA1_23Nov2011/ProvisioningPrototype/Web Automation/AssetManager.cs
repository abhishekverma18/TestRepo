using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace ProvisioningPrototype.Web_Automation
{
    public class AssetManager
    {
        public static CookieJar AssetManagerGet(CookieJar cookieJar, PanelPreferences preferences)
        {
            var client = new WebClient();

            client.Headers.Add("Pragma", "no-cache");
            client.Headers.Add("Accept", "text/html, application/xhtml+xml, */*");
            client.Headers.Add("Accept-Encoding", "gzip, deflate");
            client.Headers.Add("Accept-Language", "en-US");
            client.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
            client.Headers.Add("Referer", preferences.PanelAdminUrl + "PanelSettingsManagement.aspx");

            client.Headers.Add("Cookie", "ASP.NET_SessionId=" + cookieJar.AspNetSessionId + "; .VCPanelAuth=" + cookieJar.VcAuthentication + "; " + ".reqid=" + cookieJar.UniqueRequestId + "; " + " .vcmach=" + cookieJar.MachineId);

            var source = client.DownloadString(preferences.PanelAdminUrl + "AssetManagerView.aspx");

            string cookies = client.ResponseHeaders["Set-Cookie"];

            cookieJar.VcAuthentication = AutomationHelper.GetVcAuthentication(cookies);
            cookieJar.UniqueRequestId = AutomationHelper.GetReqId(cookies);

            client.Dispose();

            return cookieJar;
        }

        public static CookieJar AssetManagerUploadNewPortalSkin(CookieJar cookieJar, string path, PanelPreferences preferences)
        {
            var fileData = new FileStream(path, FileMode.Open, FileAccess.Read);

            var request = (HttpWebRequest)WebRequest.Create(preferences.PanelAdminUrl + "upload.axd?up=p:?");

            request.Headers.Add("Pragma", "no-cache");
            request.Accept = "text/*";
            request.UserAgent = "Shockwave Flash";
            request.Headers.Add("Cookie", "ASP.NET_SessionId=" + cookieJar.AspNetSessionId + "; .VCPanelAuth=" + cookieJar.VcAuthentication + "; " + ".reqid=" + cookieJar.UniqueRequestId + "; " + " .vcmach=" + cookieJar.MachineId);
            request.Method = "POST";

            string boundary = "----------" + DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture);
            request.ContentType = "multipart/form-data; boundary=" + boundary;

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("--{0}\r\n", boundary);
            stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", "Filename", Res.NewPortalSkinPath);
            stringBuilder.AppendFormat("--{0}\r\n", boundary);
            stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\";filename=\"{1}\"\r\n", "Filedata", Res.NewPortalSkinPath);
            stringBuilder.AppendFormat("Content-Type: {0}\r\n\r\n", "application/octet-stream");

            byte[] header = Encoding.UTF8.GetBytes(stringBuilder.ToString());
            byte[] footer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            long contentLength = header.Length + fileData.Length + footer.Length;

            request.ContentLength = contentLength;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(header, 0, header.Length);

                var buffer = new byte[checked((uint)Math.Min(4096, (int)fileData.Length))];
                int bytesRead = 0;
                while ((bytesRead = fileData.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }

                requestStream.Write(footer, 0, footer.Length);
                var response = request.GetResponse();
            }

            return cookieJar;
        }

        public static CookieJar AssetManagerUploadNewSurveySkin(CookieJar cookieJar, string path, PanelPreferences preferences)
        {
            var fileData = new FileStream(path, FileMode.Open, FileAccess.Read);

            var request = (HttpWebRequest)WebRequest.Create(preferences.PanelAdminUrl + "upload.axd?up=s:portal");

            request.Headers.Add("Pragma", "no-cache");
            request.Accept = "text/*";
            request.UserAgent = "Shockwave Flash";
            request.Headers.Add("Cookie", "ASP.NET_SessionId=" + cookieJar.AspNetSessionId + "; .VCPanelAuth=" + cookieJar.VcAuthentication + "; " + ".reqid=" + cookieJar.UniqueRequestId + "; " + " .vcmach=" + cookieJar.MachineId);
            request.Method = "POST";

            string boundary = "----------" + DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture);
            request.ContentType = "multipart/form-data; boundary=" + boundary;

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("--{0}\r\n", boundary);
            stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", "Filename", Res.NewSurveySkinPath);
            stringBuilder.AppendFormat("--{0}\r\n", boundary);
            stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\";filename=\"{1}\"\r\n", "Filedata", Res.NewSurveySkinPath);
            stringBuilder.AppendFormat("Content-Type: {0}\r\n\r\n", "application/octet-stream");

            byte[] header = Encoding.UTF8.GetBytes(stringBuilder.ToString());
            byte[] footer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            long contentLength = header.Length + fileData.Length + footer.Length;

            request.ContentLength = contentLength;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(header, 0, header.Length);

                var buffer = new byte[checked((uint)Math.Min(4096, (int)fileData.Length))];
                int bytesRead;
                while ((bytesRead = fileData.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }

                requestStream.Write(footer, 0, footer.Length);
                var response = request.GetResponse();
            }

            return cookieJar;
        }

        public static CookieJar AssetManagerDecompressNewPortalSkin(CookieJar cookieJar, PanelPreferences preferences)
        {
            var assetManagerUrl = new Uri(preferences.PanelAdminUrl + "AssetManagerView.aspx");
            string assetManagerDecompressFormParams = "Cart_AssetManager_MenuCallBack_Callback_Param=unzip&Cart_AssetManager_MenuCallBack_Callback_Param=false|p:" + Res.NewPortalSkinPath.ToLower() + "|" + Res.NewPortalSkinPath;
            var bytes = Encoding.ASCII.GetBytes(assetManagerDecompressFormParams);

            var assetManagerRequest = AutomationHelper.CreatePost(assetManagerUrl, cookieJar);
            assetManagerRequest.Referer = preferences.PanelAdminUrl + "AssetManagerView.aspx";
            assetManagerRequest.ContentLength = bytes.Length;

            using (Stream os = assetManagerRequest.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }

            var response = (HttpWebResponse)assetManagerRequest.GetResponse();
            string cookies = response.Headers["Set-Cookie"];

            cookieJar.VcAuthentication = AutomationHelper.GetVcAuthentication(cookies);
            cookieJar.UniqueRequestId = AutomationHelper.GetReqId(cookies);

            response.Close();
            return cookieJar;
        }

        public static CookieJar AssetManagerDecompressNewSurveySkin(CookieJar cookieJar, PanelPreferences preferences)
        {
            var assetManagerUrl = new Uri(preferences.PanelAdminUrl + "AssetManagerView.aspx");
            string assetManagerDecompressFormParams = "Cart_AssetManager_MenuCallBack_Callback_Param=unzip&Cart_AssetManager_MenuCallBack_Callback_Param=false|s:portal?" + Res.NewSurveySkinPath.ToLower() + "|" + Res.NewSurveySkinPath;
            var bytes = Encoding.ASCII.GetBytes(assetManagerDecompressFormParams);

            var assetManagerRequest = AutomationHelper.CreatePost(assetManagerUrl, cookieJar);
            assetManagerRequest.Referer = preferences.PanelAdminUrl + "AssetManagerView.aspx";
            assetManagerRequest.ContentLength = bytes.Length;

            using (Stream os = assetManagerRequest.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }

            var response = (HttpWebResponse)assetManagerRequest.GetResponse();
            string cookies = response.Headers["Set-Cookie"];

            cookieJar.VcAuthentication = AutomationHelper.GetVcAuthentication(cookies);
            cookieJar.UniqueRequestId = AutomationHelper.GetReqId(cookies);

            response.Close();
            return cookieJar;
        }

        public static CookieJar AssetManagerDeleteNewPortalSkinZip(CookieJar cookieJar, PanelPreferences preferences)
        {
            var assetManagerUrl = new Uri(preferences.PanelAdminUrl + "AssetManagerView.aspx");
            string assetManagerDeleteFormParams = "Cart_AssetManager_MenuCallBack_Callback_Param=delete&Cart_AssetManager_MenuCallBack_Callback_Param=false|p:" + Res.NewPortalSkinPath.ToLower() + "|" + Res.NewPortalSkinPath;
            var bytes = Encoding.ASCII.GetBytes(assetManagerDeleteFormParams);

            var assetManagerRequest = AutomationHelper.CreatePost(assetManagerUrl, cookieJar);
            assetManagerRequest.Referer = preferences.PanelAdminUrl + "AssetManagerView.aspx";
            assetManagerRequest.ContentLength = bytes.Length;

            using (Stream os = assetManagerRequest.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }

            var response = (HttpWebResponse)assetManagerRequest.GetResponse();
            string cookies = response.Headers["Set-Cookie"];

            cookieJar.VcAuthentication = AutomationHelper.GetVcAuthentication(cookies);
            cookieJar.UniqueRequestId = AutomationHelper.GetReqId(cookies);

            response.Close();
            return cookieJar;
        }

        public static CookieJar AssetManagerDeleteNewSurveySkinZip(CookieJar cookieJar, PanelPreferences preferences)
        {
            var assetManagerUrl = new Uri(preferences.PanelAdminUrl + "AssetManagerView.aspx");
            string assetManagerDeleteFormParams = "Cart_AssetManager_MenuCallBack_Callback_Param=delete&Cart_AssetManager_MenuCallBack_Callback_Param=false|s:portal?" + Res.NewSurveySkinPath.ToLower() + "|" + Res.NewSurveySkinPath;
            var bytes = Encoding.ASCII.GetBytes(assetManagerDeleteFormParams);

            var assetManagerRequest = AutomationHelper.CreatePost(assetManagerUrl, cookieJar);
            assetManagerRequest.Referer = preferences.PanelAdminUrl + "AssetManagerView.aspx";
            assetManagerRequest.ContentLength = bytes.Length;

            using (Stream os = assetManagerRequest.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }

            var response = (HttpWebResponse)assetManagerRequest.GetResponse();
            string cookies = response.Headers["Set-Cookie"];

            cookieJar.VcAuthentication = AutomationHelper.GetVcAuthentication(cookies);
            cookieJar.UniqueRequestId = AutomationHelper.GetReqId(cookies);

            response.Close();
            return cookieJar;
        }

        public static CookieJar AssetManagerPostToHomeView(CookieJar cookieJar, PanelPreferences preferences)
        {
            var assetManagerViewUrl = new Uri(preferences.PanelAdminUrl + "AssetManagerView.aspx");
            string assetManagerViewFormParams = GetAssetManagerToHomeViewFormParams();
            var bytes = Encoding.ASCII.GetBytes(assetManagerViewFormParams);

            var homeViewRequest = AutomationHelper.CreatePost(assetManagerViewUrl, cookieJar);
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

            cookieJar.UniqueRequestId = AutomationHelper.GetReqId(cookies);
            cookieJar.VcAuthentication = AutomationHelper.GetVcAuthentication(cookies);

            response.Close();
            return cookieJar;
        }

        private static string GetAssetManagerToHomeViewFormParams()
        {
            const string eventTarget = "ctl02$NM";
            const string eventArgument = "p0";
            const string vcViewState = "2";
            string viewState = String.Empty;
            const string ctlCurrentXPos = "0";
            const string ctlCurrentYPos = "0";
            const string loggingOut = "false";
            string oPersistObject_FormElement = String.Empty;
            const string nmPick = "Home";
            string ctl_NM_ContextData = String.Empty;
            string ctl_OnlineHelpCtxMenu_ContextData = String.Empty;
            const string aMMainToolbar = "Horizontal%23upload%2C1%2C0%2C0%23refresh%2C1%2C0%2C0%23restartPortal%2C1%2C0%2C0%23reloadExtension%2C1%2C0%2C0";
            const string splitterResizeList = "AssetManager_Splitter1_pane_0+500+287%3BAssetManager_Splitter1_pane_1+500+668";
            string splitterCollapseChangeList = String.Empty;
            string treeCallBackParamField = String.Empty;
            string directoryTreeData = String.Empty;
            const string directoryTreeProperties = "%3Cr%3E%3Cc%3E%3Cr%3E%3Cc%3EApplicationPath%3C%2Fc%3E%3Cc%3E%2FAdmin%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EClientEvents%3C%2Fc%3E%3Cc%3E%5Bobject%20Object%5D%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EClientTemplates%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3E%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3ECollapseSlide%3C%2Fc%3E%3Cc%3E2%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3ECollapseDuration%3C%2Fc%3E%3Cc%3E200%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3ECollapseTransition%3C%2Fc%3E%3Cc%3E0%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EControlId%3C%2Fc%3E%3Cc%3EAssetManager%24DirectoryTree%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3ECssClass%3C%2Fc%3E%3Cc%3ETreeView%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EDefaultImageHeight%3C%2Fc%3E%3Cc%3E16%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EDefaultImageWidth%3C%2Fc%3E%3Cc%3E16%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EMarginImageHeight%3C%2Fc%3E%3Cc%3E0%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EMarginImageWidth%3C%2Fc%3E%3Cc%3E0%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EDragHoverExpandDelay%3C%2Fc%3E%3Cc%3E700%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EDropChildEnabled%3C%2Fc%3E%3Cc%3Etrue%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EDropRootEnabled%3C%2Fc%3E%3Cc%3Etrue%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EEnabled%3C%2Fc%3E%3Cc%3Etrue%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EEnableViewState%3C%2Fc%3E%3Cc%3Etrue%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EExpandSlide%3C%2Fc%3E%3Cc%3E2%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EExpandDuration%3C%2Fc%3E%3Cc%3E200%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EExpandTransition%3C%2Fc%3E%3Cc%3E0%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EExpandCollapseImageHeight%3C%2Fc%3E%3Cc%3E0%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EExpandCollapseImageWidth%3C%2Fc%3E%3Cc%3E0%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EExpandedParentNodeImageUrl%3C%2Fc%3E%3Cc%3Efolder_open.gif%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EExpandSelectedPath%3C%2Fc%3E%3Cc%3Etrue%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EExpandNodeOnSelect%3C%2Fc%3E%3Cc%3Etrue%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EFillContainer%3C%2Fc%3E%3Cc%3Etrue%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EHoverNodeCssClass%3C%2Fc%3E%3Cc%3EHoverTreeNode%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EHoverPopupNodeCssClass%3C%2Fc%3E%3Cc%3EHoverPopupTreeNode%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EHoverPopupEnabled%3C%2Fc%3E%3Cc%3Etrue%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EImagesBaseUrl%3C%2Fc%3E%3Cc%3E%2FAdmin%2Fs92853%2FImages%2FAssetManager%2F%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EItemSpacing%3C%2Fc%3E%3Cc%3E0%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3ELeafNodeImageUrl%3C%2Fc%3E%3Cc%3Efolder.gif%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3ELineImageHeight%3C%2Fc%3E%3Cc%3E20%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3ELineImageWidth%3C%2Fc%3E%3Cc%3E19%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3ELineImagesFolderUrl%3C%2Fc%3E%3Cc%3E%2FAdmin%2Fs92853%2FImages%2FAssetManager%2Flines%2F%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3ELoadingFeedbackText%3C%2Fc%3E%3Cc%3ELoading...%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EMarginWidth%3C%2Fc%3E%3Cc%3E32%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EMultipleSelectEnabled%3C%2Fc%3E%3Cc%3Etrue%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3ENodeCssClass%3C%2Fc%3E%3Cc%3ETreeNode%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3ENodeLabelPadding%3C%2Fc%3E%3Cc%3E3%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3ENodeIndent%3C%2Fc%3E%3Cc%3E16%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EParentNodeImageUrl%3C%2Fc%3E%3Cc%3Efolder.gif%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3EShowLines%3C%2Fc%3E%3Cc%3Etrue%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3Cc%3E%3Cr%3E%3Cc%3ESelectedNodeCssClass%3C%2Fc%3E%3Cc%3ESelectedTreeNode%3C%2Fc%3E%3C%2Fr%3E%3C%2Fc%3E%3C%2Fr%3E";
            const string directoryTreeSelectedNode = "p_m%3A%3F";
            const string directoryTreeScrollData = "0%2C0";
            string assetManagerGridCallBackParamField = String.Empty;
            string assetManagerFilelistGridEventList = String.Empty;
            string assetManagerFilelistGridData = String.Empty;
            string assetManagerContextMenuontextData = String.Empty;
            string assetManagerMenuCallBackParamField = String.Empty;

            string formParameters = String.Format("__EVENTTARGET={0}&__EVENTARGUMENT={1}&__VisionCriticalVIEWSTATE={2}&__VIEWSTATE={3}&ctl02%24CurrentXPos={4}&ctl02%24CurrentYPos={5}&LoggingOut={6}&oPersistObject_FormElement={7}&nmPick={8}&ctl02_NM_ContextData={9}&ctl02_OnlineHelpCtxMenu_ContextData={10}&AssetManager$MainToolbar_Hidden={11}&AssetManager_Splitter1_ResizeList={12}&AssetManager_Splitter1_CollapseChangeList={13}&AssetManager_TreeCallBack_ParamField={14}&AssetManager_DirectoryTree_Data={15}&AssetManager_DirectoryTree_Properties={16}&AssetManager_DirectoryTree_SelectedNode={17}&AssetManager_DirectoryTree_ScrollData={18}&AssetManager_GridCallBack_ParamField={19}&AssetManager_FilelistGrid_EventList={20}&AssetManager_FilelistGrid_Data={21}&AssetManager_ContextMenu_ContextData={22}&AssetManager_MenuCallBack_ParamField={23}", eventTarget, eventArgument, vcViewState, viewState, ctlCurrentXPos, ctlCurrentYPos, loggingOut, oPersistObject_FormElement, nmPick, ctl_NM_ContextData, ctl_OnlineHelpCtxMenu_ContextData, aMMainToolbar, splitterResizeList, splitterCollapseChangeList, treeCallBackParamField, directoryTreeData, directoryTreeProperties, directoryTreeSelectedNode, directoryTreeScrollData, assetManagerGridCallBackParamField, assetManagerFilelistGridEventList, assetManagerFilelistGridData, assetManagerContextMenuontextData, assetManagerMenuCallBackParamField);
            return formParameters;
        }

    }
}