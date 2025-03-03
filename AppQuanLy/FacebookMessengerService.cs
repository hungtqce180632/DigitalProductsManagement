using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SubscriptionManagementWPF
{
    public static class FacebookMessengerService
    {
        // These values are stored in MessengerConfig after configuration.
        // If you want to hard-code a token for testing, replace MessengerConfig.PageAccessToken.
        public static async Task<bool> SendMessageAsync(string recipientId, string messageText)
        {
            // Build the URL with the access token.
            string requestUrl = $"https://graph.facebook.com/v15.0/me/messages?access_token={MessengerConfig.PageAccessToken}";

            // Prepare the JSON payload.
            var payload = new
            {
                recipient = new { id = recipientId },
                message = new { text = messageText }
            };
            string jsonPayload = JsonSerializer.Serialize(payload);

            using (var client = new HttpClient())
            {
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(requestUrl, content);
                return response.IsSuccessStatusCode;
            }
        }

        // A sample method to “load” messages.
        // In a real integration you would call the appropriate endpoint and parse the messages.
        public static async Task<string> LoadMessagesAsync()
        {
            // This is a dummy implementation. In reality you might call:
            // GET https://graph.facebook.com/v15.0/me/conversations?access_token=...
            await Task.Delay(1000); // simulate network delay
            return "Loaded messages from Facebook (dummy data).";
        }
    }
}
