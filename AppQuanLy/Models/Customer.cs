using System;

namespace SubscriptionManagementWPF
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubscriptionPackage { get; set; }
        public DateTime RegisterDay { get; set; }
        public DateTime SubscriptionExpiry { get; set; }
        public DateTime LastActivity { get; set; }
        public string Note { get; set; } // Used to store account email
    }
}
