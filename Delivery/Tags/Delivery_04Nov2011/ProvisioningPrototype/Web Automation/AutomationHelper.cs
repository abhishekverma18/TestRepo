using System;
using System.Text.RegularExpressions;
using System.Net;

namespace ProvisioningPrototype.Web_Automation
{
    public class AutomationHelper
    {
        public static HttpWebRequest CreatePost(Uri url, CookieJar cookieJar)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.Headers["Accept-Language"] = "en-US";
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";
            request.ContentType = "application/x-www-form-urlencoded";
            //homeViewRequest.Headers["Accept-Encoding"] = "gzip, deflate";
            request.Host = url.Host;
            request.Headers.Add("Cookie", "ASP.NET_SessionId=" + cookieJar.AspNetSessionId + "; .VCPanelAuth=" + cookieJar.VcAuthentication + "; " + ".reqid=" + cookieJar.UniqueRequestId + "; " + " .vcmach=" + cookieJar.MachineId);
            request.Headers["Pragma"] = "no-cache";
            request.Method = "POST";
            request.KeepAlive = true;

            return request;
        }

        public static string ExtractTestLink(string pageSource)
        {
            var testLinkRegex = new Regex(RegexResource.TestLinkRegex);
            var match = testLinkRegex.Match(pageSource);

            if (!match.Success)
            {
                throw new Exception("Unable to extract test link from AnonymousLinkView");
            }

            return match.Groups[1].Value;
        }

        public static string GetwTtData(string source)
        {
            var studyDataRegex = new Regex(RegexResource.ctl10_wT_wTt_DataRegex, RegexOptions.Singleline);
            var match = studyDataRegex.Match(source);

            if (!match.Success)
            {
                throw new Exception("Study information not found.  Profile Questionnaire may have errors/warnings or PQ with same name already uploaded"); //TODO Handle this error better
            }

            string studyData = match.Groups[1].Value;

            var replace1Regex = new Regex(RegexResource.Replace_1Regex);
            var match1 = replace1Regex.Match(studyData);
            var replace_1 = match1.Groups[1].Value;

            var replace2Regex = new Regex(RegexResource.Replace_2Regex);
            var match2 = replace2Regex.Match(replace_1);
            var replace_2 = match2.Groups[1].Value;

            var replace4Regex = new Regex(RegexResource.Replace_4Regex);
            var match4 = replace4Regex.Match(studyData);
            var replace_4 = match4.Groups[1].Value;

            var replace3Regex = new Regex(RegexResource.Replace_3Regex);
            var match3 = replace3Regex.Match(replace_4);
            var replace_3 = match3.Groups[1].Value;

            string result = Res.ctl10_wT_wTt_Data;

            result = result.Replace("[REPLACE_1]", replace_1);
            result = result.Replace("[REPLACE_2]", replace_2);
            result = result.Replace("[REPLACE_3]", replace_3);
            result = result.Replace("[REPLACE_4]", replace_4);
            return result;
        }

        public static string GetWTtProperties()
        {
            return Res.ctl10_wT_wTt_Properties;
        }

        public static string GetLibraryLTProperties()
        {
            return Res.LibraryDiv_lT_Properties;
        }

        public static string GetLibraryLTData()
        {
            return Res.LibraryDiv_lT_Data;
        }

        public static string GetProperties()
        {
            return Res.treeToolBar_Properties;
        }


        public static string GetItemStorage()
        {
            return Res.treeToolBar_ItemStorage;
        }

        public static string GetPipesTreeProperties()
        {
            return Res.e_pipesDiv_pipesTree_Properties;
        }

        public static string GetPipesTreeData(Uri url)
        {
            //TODO use source to get host??
            string data = Res.e_pipesDiv_pipesTree_Data;

            if (!data.Contains("[HOST]"))
            {
                throw new Exception("e_pipesDiv_pipesTree_Data [HOST] variable not found");
            }

            var hostRegex = new Regex(RegexResource.HostRegex);
            data = hostRegex.Replace(data, url.Host);
            return data;
        }

        public static string GetSTabs()
        {
            return Res.sTabs;
        }

        public static string GetVcMach(string cookies)
        {
            var vcMachRegex = new Regex(RegexResource.VcMachRegex);
            var match = vcMachRegex.Match(cookies);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            throw new Exception("VcMach Cookie Not Found");
        }

        public static string GetReqId(string cookies)
        {
            var reqIdRegex = new Regex(RegexResource.ReqIdRegex);
            var match = reqIdRegex.Match(cookies);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            throw new Exception("ReqId Cookie Not Found");
        }

        public static string GetVcAuthentication(string cookies)
        {
            var sessionIdRegex = new Regex(RegexResource.VcAuthenticationRegex);
            var match = sessionIdRegex.Match(cookies);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            throw new Exception("VCPanelAuth Cookie Not Found");
        }


        public static string GetSessionId(string cookies)
        {
            var sessionIdRegex = new Regex(RegexResource.SessionIdRegex);
            var match = sessionIdRegex.Match(cookies);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            throw new Exception("SessionId Cookie Not Found");
        }
    }
}