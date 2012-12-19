namespace ProvisioningPrototype
{
    // Added for Offline and Advanced Mode
    public class LoginPreferences
    {
        public string PanelAdminEmail { get; set; }
        public string PanelPassword { get; set; }
        public string PanelAdminUrl { get; set; }
        public bool OfflineMode { get; set; }
    }
}
