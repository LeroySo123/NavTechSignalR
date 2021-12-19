using Microsoft.AspNetCore.SignalR.Client;
using System.Xml.Linq;

namespace NavTechSignalR_Server.Services
{
    public class DecodeProtocolService
    {
        public static async void DecodeXML(string responseData)
        {
            //create the client connection to hub
            var connection = new HubConnectionBuilder().WithUrl("https://localhost:7271/MessageHub").Build();
            //start the connection
            await connection.StartAsync();

            //Split the message to string array
            string[] arrResponseData = responseData.Split("AA55\u0001").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            foreach (string str in arrResponseData)
            {
                //For display the heartbeat received
                if (str.StartsWith('\u0001'))
                {
                    string message = "Heartbeat received.";
                    await connection.InvokeAsync("SendMessage", message);
                }
                else
                {
                    //for get the xml
                    string tempStr = str.Substring(5);
                    XDocument xdoc = XDocument.Parse(tempStr);
                    //remove the xml head
                    xdoc.Declaration = null;
                    string message = xdoc.ToString();
                    await connection.InvokeAsync("SendMessage", message);
                }
            }

        }
    }
}
