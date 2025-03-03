using System;
using System.Collections.ObjectModel;

namespace SubscriptionManagementWPF
{
    public static class Messenger
    {
        public static ObservableCollection<Message> Messages { get; } = new ObservableCollection<Message>();
        private static int nextMessageId = 1;
        private static readonly string autoReplyMessage = "Thank you for reaching out. We will get back to you shortly.";

        public static void SimulateIncomingMessage(string content)
        {
            var msg = new Message
            {
                Id = nextMessageId++,
                Content = content,
                IsRead = false,
                ReceivedAt = DateTime.Now
            };
            Messages.Add(msg);
        }

        public static void SendAutoReply(string recipient)
        {
            var reply = new Message
            {
                Id = nextMessageId++,
                Content = $"Auto-reply to {recipient}: {autoReplyMessage}",
                IsRead = true,
                ReceivedAt = DateTime.Now
            };
            Messages.Add(reply);
        }

        public static void MarkAllAsRead()
        {
            foreach (var msg in Messages)
            {
                msg.IsRead = true;
            }
        }
    }
}
