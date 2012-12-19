using System.Collections.Generic;

namespace ProvisioningPrototype
{
    public class Notification
    {
        public List<PanelSetting> NotificationSettings { get; set; }
        public int NotificationIndex { get; set; }

        public Notification(string notificationSource, int notificationIndex)
        {
            NotificationIndex = notificationIndex;
            NotificationSettings = PanelSetting.GetPanelSettings(notificationSource);
        }

    }
}