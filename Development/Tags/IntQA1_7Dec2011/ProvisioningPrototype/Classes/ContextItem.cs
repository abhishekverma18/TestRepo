using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProvisioningPrototype
{
    public class ContextItem
    {
         public List<PanelSetting> PanelSettings { get; set; }
        public List<Notification> Notifications { get; set; }
        public string ContextItemName { get; set; }

        public ContextItem(string parsedPageSource, string contextItemName)
        {
            ContextItemName = contextItemName;
            PanelSettings = new List<PanelSetting>();
            Notifications = new List<Notification>();

            int notificationCount = GetNotificationCount(parsedPageSource);
            string notificationsSource = ParseNotificationsSource(parsedPageSource);

            if (ContextItemName.Equals("Communication Dashboard"))
            {
                List<string> notificationsSourceList = ParseNotifications(notificationsSource, notificationCount);
                Notifications = GetNotifications(notificationsSourceList);
            }
            else if(ContextItemName.Equals("Media"))
            {
                //TODO Media Currently doesnt have the option to be edited...will need to look into this more
            }
            else
            {
                PanelSettings = PanelSetting.GetPanelSettings(parsedPageSource);
            }
        }

        private static List<Notification> GetNotifications(List<string> notificationSourceList)
        {
            var notificationList = new List<Notification>();
           
            int notificationIndex = 0;
            foreach (var notificationSource in notificationSourceList)
            {
                var notification = new Notification(notificationSource, notificationIndex);
                notificationList.Add(notification);
                notificationIndex++;
            }

            return notificationList;
        }


        private static List<string> ParseNotifications(string source, int notificationCount)
        {
            var notificationSourceList = new List<string>();

            string notificationRegex = String.Empty;

            for (int i = 0; i < notificationCount; i++)
            {
                notificationRegex += @"title=""Notifications\[" + i + @"\]""(.*)";
            }

            var notificationSourceRegex = new Regex(notificationRegex, RegexOptions.Singleline);
            Match match = notificationSourceRegex.Match(source);

            if (!match.Success)
            {
                return new List<string>();
            }

            for (int i = 1; i <= notificationCount; i++)
            {
                string notificationSource = match.Groups[i].Value;
                notificationSourceList.Add(notificationSource);
            }

            return notificationSourceList;
        }

        private static int GetNotificationCount(string source)
        {
            var notificationCountRegex = new Regex(@"title=""Notifications\[\d+\]""");
            MatchCollection matches = notificationCountRegex.Matches(source);

            if (matches.Count >= 1)
            {
                return matches.Count;
            }

            return 0;
        }

        private static string ParseNotificationsSource(string source)
        {
            //Console.WriteLine(source);
            var notificationsSourceRegex = new Regex(@"\[\+\] Notifications(.*)\[\+\] Contact Information", RegexOptions.Singleline);
            Match match = notificationsSourceRegex.Match(source);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            
            return String.Empty; //So if no notifications were found...then its likely that its not a communication Dashboard!
            //throw new Exception("Regex was unable to find Notifications");
        }
    }
    
}