using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SubscriptionManagementWPF;
using SubscriptionManagementWPF.Properties;

namespace SubscriptionManagementWPF
{
    // Renamed class to avoid ambiguity
    public static class FacebookMessengerConfig
    {
        public static string PageAccessToken { get; set; }
        public static string DefaultRecipientId { get; set; }
        
        // Save configuration to settings
        public static void SaveConfig()
        {
            Properties.Settings.Default.PageAccessToken = PageAccessToken;
            Properties.Settings.Default.DefaultRecipientId = DefaultRecipientId;
            Properties.Settings.Default.Save();
        }
        
        // Load configuration from settings
        public static void LoadConfig()
        {
            PageAccessToken = Properties.Settings.Default.PageAccessToken;
            DefaultRecipientId = Properties.Settings.Default.DefaultRecipientId;
        }
    }
}

namespace AppQuanLy.Services
{
    public static class FacebookMessengerService
    {
        private static readonly HttpClient client = new HttpClient();
        private const string ApiUrl = "https://graph.facebook.com/v13.0/me/messages";

        public static async Task<bool> SendMessageAsync(string recipientId, string messageText)
        {
            if (string.IsNullOrEmpty(FacebookMessengerConfig.PageAccessToken))
            {
                return false;
            }

            string messageJson = $@"{{
                ""recipient"": {{
                    ""id"": ""{recipientId}""
                }},
                ""message"": {{
                    ""text"": ""{messageText}""
                }}
            }}";

            try
            {
                // This is a simulated call - in a real app, you'd uncomment this to send to FB API
                /*
                var content = new StringContent(messageJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{ApiUrl}?access_token={FacebookMessengerConfig.PageAccessToken}", content);
                return response.IsSuccessStatusCode;
                */

                // For demo, just simulate a successful message send
                await Task.Delay(500); // Simulate network delay
                
                // Also add the message to our local messages collection for the demo
                // Add it asynchronously to prevent UI freezing
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Messenger.SimulateIncomingMessage($"[Sent to {recipientId}]: {messageText}");
                });
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending FB message: {ex.Message}");
                return false;
            }
        }

        public static async Task<string> LoadMessagesAsync()
        {
            // Simulate fetching messages from Facebook
            await Task.Delay(800); // Simulate network delay
            
            // Add some test messages
            Random rand = new Random();
            int messageCount = rand.Next(1, 4);
            
            Application.Current.Dispatcher.Invoke(() =>
            {
                for (int i = 0; i < messageCount; i++)
                {
                    Messenger.SimulateIncomingMessage($"Simulated FB message #{i + 1} at {DateTime.Now}");
                }
            });
            
            return $"Successfully loaded {messageCount} messages.";
        }
        
        // Send a broadcast message to multiple recipients
        public static async Task<(int Succeeded, int Failed)> SendBroadcastAsync(string[] recipientIds, string messageText)
        {
            int succeeded = 0;
            int failed = 0;
            
            foreach (var id in recipientIds)
            {
                bool success = await SendMessageAsync(id, messageText);
                if (success)
                    succeeded++;
                else
                    failed++;
                
                // Slight delay between sends to avoid rate limiting
                await Task.Delay(200);
            }
            
            return (succeeded, failed);
        }
    }
}
