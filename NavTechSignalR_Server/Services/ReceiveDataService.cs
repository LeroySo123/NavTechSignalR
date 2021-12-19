using Microsoft.AspNetCore.SignalR.Client;

namespace NavTechSignalR_Server.Services
{
    public class ReceiveDataService
    {
        public static async void SendToAPIByxml(string message)
        {
            using (var client = new HttpClient())
            {
                var stringContent = new StringContent("");
                var response = await client.PostAsync("https://localhost:7271/api/Message/PostXml?value=" + message, stringContent);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Send to api cannot success");
                }
            }
        }
    }
}
