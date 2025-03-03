using System;

namespace SubscriptionManagementWPF
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime ReceivedAt { get; set; }
    }
}
