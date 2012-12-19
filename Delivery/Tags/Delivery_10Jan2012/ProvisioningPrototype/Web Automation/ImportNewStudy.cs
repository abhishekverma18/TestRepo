using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace ProvisioningPrototype.Web_Automation
{
    public class ImportNewStudy
    {
        public static CookieJar ImportNewPxmlStudy(CookieJar cookieJar, string selectedStudyFile, PanelPreferences preferences)
        {
            var fileData = new FileStream(selectedStudyFile, FileMode.Open, FileAccess.Read);

            //Added for Panel settings will be fecthed from UI not from WebConfig
            var request = AutomationHelper.CreatePost(new Uri(preferences.PanelAdminUrl + "ImportNewStudyView.aspx"), cookieJar);
            string contentType = String.Empty;

            request.Referer = preferences.PanelAdminUrl + "ImportNewStudyView.aspx";

            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture);
            request.ContentType = "multipart/form-data; boundary=" + boundary;

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("--{0}\r\n", boundary);
            stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", "__EVENTTARGET", "ImportStudyFromXmlButton");
            stringBuilder.AppendFormat("--{0}\r\n", boundary);
            stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", "__EVENTARGUMENT", "");
            stringBuilder.AppendFormat("--{0}\r\n", boundary);
            stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", "__VisionCriticalVIEWSTATE", "93");
            stringBuilder.AppendFormat("--{0}\r\n", boundary);
            stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", "__VIEWSTATE", "");
            stringBuilder.AppendFormat("--{0}\r\n", boundary);
            stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", "ctl02$CurrentXPos", "0");
            stringBuilder.AppendFormat("--{0}\r\n", boundary);
            stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", "ctl02$CurrentYPos", "0");
            stringBuilder.AppendFormat("--{0}\r\n", boundary);
            stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", "LoggingOut", "false");
            stringBuilder.AppendFormat("--{0}\r\n", boundary);
            stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", "oPersistObject_FormElement", "");
            stringBuilder.AppendFormat("--{0}\r\n", boundary);
            stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", "nmPick", "");
            stringBuilder.AppendFormat("--{0}\r\n", boundary);
            stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", "ctl02_NM_ContextData", "");
            stringBuilder.AppendFormat("--{0}\r\n", boundary);
            stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", "ctl02_OnlineHelpCtxMenu_ContextData", "");
            stringBuilder.AppendFormat("--{0}\r\n", boundary);
            stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\";filename=\"{1}\"\r\n", "XmlFileInput", Path.GetFileName(selectedStudyFile));//something.xml/.zip
            stringBuilder.AppendFormat("Content-Type: {0}\r\n\r\n", "text/xml");

            byte[] header = Encoding.UTF8.GetBytes(stringBuilder.ToString());

            var footerBuilder = new StringBuilder();
            footerBuilder.AppendFormat("\r\n--{0}\r\n", boundary);
            footerBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", "XmlStudyName", Path.GetFileNameWithoutExtension(selectedStudyFile) + "_" + DateTime.Now);//just the name of the study TODO account for multiple studies with the same name
            footerBuilder.AppendFormat("--" + boundary + "--\r\n");
            byte[] footer = Encoding.ASCII.GetBytes(footerBuilder.ToString());

            long contentLength = header.Length + fileData.Length + footer.Length;

            request.ContentLength = contentLength;
            string cookies = String.Empty;
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
                cookies = response.Headers["Set-Cookie"];
            }

            cookieJar.VcAuthentication = AutomationHelper.GetVcAuthentication(cookies);
            cookieJar.UniqueRequestId = AutomationHelper.GetReqId(cookies);
            return cookieJar;
        }//Can be changed easily to upload zip instead
    }
}