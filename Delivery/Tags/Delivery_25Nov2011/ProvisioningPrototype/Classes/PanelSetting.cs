using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace ProvisioningPrototype
{
    public class PanelSetting
    {
        public string PanelSettingName { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public PanelSetting(string title, string panelSettingName, string name, string value)
        {
            PanelSettingName = panelSettingName;
            Title = title;
            Name = name;
            Value = value;
        }

        public static List<PanelSetting> GetPanelSettings(string source)
        {
            var panelSettings = new List<PanelSetting>();

            source = RemoveNBSP(source);
            
            var panelSettingsRegex = new Regex(@"title=""(.*?)"".*?class=""property"".*?>(.*?)</td>.*?name=""(.*?)""(.*?)</td>", RegexOptions.Singleline);//Value will not be present for empty Value fields

            MatchCollection matches = panelSettingsRegex.Matches(source);

            if (matches.Count == 0)
            {
                Console.WriteLine(source);
                throw new Exception("Unable to parse panel settings");
            }

            foreach (Match match in matches)
            {
                string title = match.Groups[1].Value;
                string panelSettingName = match.Groups[2].Value;
                string name = match.Groups[3].Value;
                string value = GetValue(match.Groups[4].Value);

                title = HttpUtility.HtmlDecode(title);
                panelSettingName = HttpUtility.HtmlDecode(panelSettingName);
                name = HttpUtility.HtmlDecode(name);
                value = HttpUtility.HtmlDecode(value);

                title = HttpUtility.UrlEncode(title);
                panelSettingName = HttpUtility.UrlEncode(panelSettingName);
                name = HttpUtility.UrlEncode(name);
                value = HttpUtility.UrlEncode(value);

                title = ReplaceAtSymbol(title);
                panelSettingName = ReplaceAtSymbol(panelSettingName);
                name = ReplaceAtSymbol(name);
                value = ReplaceAtSymbol(value);

                var setting = new PanelSetting(title, panelSettingName, name, value);
                panelSettings.Add(setting);
            }

            return panelSettings;
        }

        //TODO WOULD MAKE MORE SENSE TO HAVE THE INCOMING STRING ALREADY BE ENCODED PROPERLY

        private static string GetValue(string input)
        {
            var valueRegex = new Regex(@"value=""(.*?)""");
            Match match = valueRegex.Match(input);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return String.Empty;
        }

        private static string ReplaceAtSymbol(string input)
        {
            return input.Replace("%40", "@");
        }

        private static string RemoveNBSP(string source)
        {
            var nbspRemoverRegex = new Regex(@"\&nbsp;");
            source = nbspRemoverRegex.Replace(source, String.Empty);
            return source;
        }
    }
}