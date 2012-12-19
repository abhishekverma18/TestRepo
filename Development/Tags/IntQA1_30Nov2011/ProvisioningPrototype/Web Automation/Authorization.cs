using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using ProvisioningPrototype.Utility;


namespace ProvisioningPrototype.Web_Automation
{
    public class Authorization
    {
        public static string GetViewState(PanelPreferences preferences)
        {
            var client = new WebClient();

            client.Headers.Add("Pragma", "no-cache");
            client.Headers.Add("Accept", "text/html, application/xhtml+xml, */*");
            client.Headers.Add("Accept-Encoding", "gzip, deflate");
            client.Headers.Add("Accept-Language", "en-US");
            client.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");

            var buffer = client.DownloadData(preferences.PanelAdminUrl + "AuthenticationView.aspx");
            string viewState = ParseViewState(Decompress(buffer));
            client.Dispose();

            return viewState;
        }

        private static string ParseViewState(string pageSource)
        {
            var viewStateValueRegex = new Regex(RegexResource.ViewStateRegex);
            Match match = viewStateValueRegex.Match(pageSource);

            if (match.Success)
            {
                return "%2F" + HttpUtility.UrlEncode(match.Groups[1].Value);
            }

            throw new Exception("__VIEWSTATE Value Not Found.");
        }

        private static string Decompress(byte[] byteArray)
        {
            var memoryStream = new MemoryStream(byteArray);
            var stream = new GZipStream(memoryStream, CompressionMode.Decompress);

            byteArray = new byte[byteArray.Length];

            int readByte = stream.Read(byteArray, 0, byteArray.Length);

            var stringBuilder = new StringBuilder(readByte);

            for (int i = 0; i < readByte; i++)
            {
                stringBuilder.Append((char)byteArray[i]);
            }

            stream.Close();
            memoryStream.Close();
            stream.Dispose();
            memoryStream.Dispose();
            return stringBuilder.ToString();
        }

        public static CookieJar AuthenticationPost(string viewState, CookieJar cookieJar, PanelPreferences preferences)
        {
            var authenticationViewUrl = new Uri(preferences.PanelAdminUrl + "AuthenticationView.aspx");
            string authenticationFormParams = GetAuthFormParams(viewState, preferences);
            byte[] bytes = Encoding.ASCII.GetBytes(authenticationFormParams);

            var authenticationRequest = AutomationHelper.CreatePost(authenticationViewUrl, cookieJar);
            authenticationRequest.Headers.Remove("Cookie");
            authenticationRequest.Referer = preferences.PanelAdminUrl + "AuthenticationView.aspx";
            authenticationRequest.ContentLength = bytes.Length;
            authenticationRequest.AllowAutoRedirect = false;

            using (Stream os = authenticationRequest.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }

            var response = (HttpWebResponse)authenticationRequest.GetResponse();

            string cookies = response.Headers["Set-Cookie"];

            cookieJar.AspNetSessionId = AutomationHelper.GetSessionId(cookies);
            cookieJar.VcAuthentication = AutomationHelper.GetVcAuthentication(cookies);

            response.Close();

            return cookieJar;
        }

        private static string GetAuthFormParams(string viewState,PanelPreferences preferences)
        {
            const string eventTarget = "LoginBtn";
            const string eventArgument = "";
            string emailText = preferences.PanelAdminEmail;
            string passwordText =PasswordUtill.Decrypt(preferences.PanelPassword);
            string formParameters = String.Format("__EVENTTARGET={0}&__EVENTARGUMENT={1}&__VIEWSTATE={2}&eMailText={3}&PasswordText={4}", eventTarget, eventArgument, viewState, emailText, passwordText);
            return formParameters;
        }
    }
}