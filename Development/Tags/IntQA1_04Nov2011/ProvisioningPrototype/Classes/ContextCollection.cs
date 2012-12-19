using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProvisioningPrototype
{
    public class ContextCollection
    {
        public string PropertiesEditorTable { get; set; }
        public List<Context> ContextList { get; set; }

        public ContextCollection(string pageSource)
        {
            PropertiesEditorTable = ParsePropertiesEditorTable(pageSource);

            int contextCount = GetContextCount(PropertiesEditorTable);


            List<string> contextSourceList = ParseContexts(PropertiesEditorTable, contextCount);
            ContextList = GetContexts(contextSourceList);
        }

        private static string ParsePropertiesEditorTable(string pageSource)
        {
            var propertiesEditorTableRegex = new Regex(@"(<table id=""PropertiesEditor1"" .*?</table>)", RegexOptions.Singleline);
            Match match = propertiesEditorTableRegex.Match(pageSource);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            throw new Exception("Properties Editor Table not found");

        }

        private static int GetContextCount(string source)
        {
            var contextCountRegex = new Regex(@"title=""ContextCollection\[\d+\]""");
            MatchCollection matches = contextCountRegex.Matches(source);

            if (matches.Count >= 1)
            {
                return matches.Count;
            }

            throw new Exception("No Contexts Found");

        }

        private static List<string> ParseContexts(string source, int contextCount)
        {
            var contextSourceList = new List<string>();

            string contextRegex = String.Empty;

            for (int i = 0; i < contextCount; i++)
            {
                contextRegex += @"title=""ContextCollection\[" + i + @"\]""(.*)";
            }

            var contextSourceRegex = new Regex(contextRegex, RegexOptions.Singleline);
            Match match = contextSourceRegex.Match(source);

            if (!match.Success)
            {
                throw new Exception("Unable To Parse Contexts");
            }

            for (int i = 1; i <= contextCount; i++)
            {
                string contextSource = match.Groups[i].Value;
                contextSourceList.Add(contextSource);
            }

            return contextSourceList;
        }

        private static List<Context> GetContexts(List<string> contextSourceList)
        {
            var contextList = new List<Context>();
            Console.WriteLine(contextSourceList.Count());
            int index = 0;
            foreach (var contextSource in contextSourceList)
            {
                var context = new Context(contextSource, index);
                contextList.Add(context);
                index++;
            }

            return contextList;
        }


        public string GetFormString()
        {
            string formString = String.Empty;

            foreach (var context in ContextList)
            {
                foreach (var contextItem in context.ContextItems)
                {
                    foreach (var notification in contextItem.Notifications)
                    {
                        formString = notification.NotificationSettings.Aggregate(formString, (current, panelSetting) => current + (panelSetting.Name + "=" + panelSetting.Value + "&"));
                    }

                    formString = contextItem.PanelSettings.Aggregate(formString, (current, panelSetting) => current + (panelSetting.Name + "=" + panelSetting.Value + "&"));
                }
            }

            return formString;
        }

        //Search and Replace


        // <param name="name">This is the name which appears in the Panel Settings Management aspx page</param>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextIndex"></param>
        /// <param name="notificationIndex">Index of the notification if there is one, use a negative integer as input if it is not required</param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool UpdateFormValue(int contextIndex, int notificationIndex, string name, string value)
        {
            bool replaced = false;

            foreach (var context in ContextList)
            {

                if (context.ContextIndex == contextIndex)
                {
                    foreach (var contextItem in context.ContextItems)
                    {
                        foreach (var notification in contextItem.Notifications)
                        {
                            if (notification.NotificationIndex == notificationIndex)
                            {
                                foreach (var panelSetting in notification.NotificationSettings)
                                {
                                    if (panelSetting.PanelSettingName == name)
                                    {
                                        panelSetting.Value = value;
                                        replaced = true;
                                    }
                                }
                            }
                        }//If its a notification

                        foreach (var panelSetting in contextItem.PanelSettings)
                        {
                            if (panelSetting.PanelSettingName == name)
                            {
                                panelSetting.Value = value;
                                replaced = true;
                            }

                        }
                    }
                }
            }
            return replaced;
        }

        // Added for Available context module by Optimus
        public ContextInfo GetContextInformation(List<PanelSetting> pathsPanelSettings, List<PanelSetting> generalPanelSettings, int contextIndex)
        {
            return GetContextInfo(pathsPanelSettings, generalPanelSettings, contextIndex);
        }

        public ContextInfo FindAvailableContext()
        {
            foreach (var context in ContextList)
            {
                foreach (var contextItem in context.ContextItems)
                {
                    if (contextItem.PanelSettings.Any(panelSetting => panelSetting.PanelSettingName.Equals("OpenPortalSkinFolder") && panelSetting.Value.Equals(String.Empty)))
                    {
                        return GetContextInfo(contextItem.PanelSettings, context.ContextItems[2].PanelSettings, context.ContextIndex);
                    }
                }
            }

            throw new Exception("No available context found");
        }//If the OpenPortalSkinFolder does not have a folder assigned to it, then it is available to use


        private ContextInfo GetContextInfo(List<PanelSetting> pathsPanelSettings, List<PanelSetting> generalPanelSettings, int contextIndex)
        {
            var info = new ContextInfo { ContextIndex = contextIndex };

            if (pathsPanelSettings[3].PanelSettingName.Equals("OpenPortalLiveBaseUrl"))
            {
                info.OpenPortalLiveBaseUrl = pathsPanelSettings[3].Value;
            }
            else
            {
                throw new Exception("OpenPortalLiveBaseUrl not found at Index 3 of Paths Panel Settings");
            }

            if (pathsPanelSettings[5].PanelSettingName.Equals("OpenPortalTestBaseUrl"))
            {
                info.OpenPortalTestBaseUrl = pathsPanelSettings[5].Value;
            }
            else
            {
                throw new Exception("OpenPortalTestBaseUrl not found at index 5 of Paths Panel Settings");
            }
            // Added for Available context module by Optimus
            if (pathsPanelSettings[6].PanelSettingName.Equals("PortalBaseUrl"))
            {
                info.PortalBaseUrl = pathsPanelSettings[6].Value;
            }
            else
            {
                throw new Exception("PortalBaseUrl not found at index 5 of Paths Panel Settings");
            }

            info.SubDomain = ParseSubDomain(info.OpenPortalLiveBaseUrl);

            //General 

            if (generalPanelSettings[0].PanelSettingName.Equals("Culture"))
            {
                info.Culture = generalPanelSettings[0].Value;
            }

            if (generalPanelSettings[2].PanelSettingName.Equals("Environment"))
            {
                info.Environment = generalPanelSettings[2].Value;
            }

            // Added for Available context module by Optimus
            if (generalPanelSettings[5].PanelSettingName.Equals("Name"))
            {
                info.Name = generalPanelSettings[5].Value;
            }

            return info;

        }

        private string ParseSubDomain(string input)
        {
            var domainRegex = new Regex(@"https?%3a%2f%2f(.*?)\.", RegexOptions.IgnoreCase);
            Match match = domainRegex.Match(input);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            throw new Exception("Error parsing domain from OpenPOrtalLiveBaseUrl");
        }

    }
}