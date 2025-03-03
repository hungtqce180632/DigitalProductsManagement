namespace SubscriptionManagementWPF.Properties
{
    public sealed class Settings
    {
        private static readonly Settings defaultInstance = new Settings();
        public static Settings Default => defaultInstance;

        public string PageAccessToken { get; set; } = "";
        public string DefaultRecipientId { get; set; } = "";

        public void Save()
        {
            // ...saving logic here...
        }
    }
}
