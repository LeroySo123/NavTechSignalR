using System.Net;
using System.Net.Sockets;

namespace NavTechSignalR_Server.Services
{
    public class ConnectionService
    {
        string server = IPAddress.Loopback.ToString();
        Int32 port = 10000;
        int timeout = 5000;
        TcpClient _client = null;
        NetworkStream _stream = null;

        public async void Connect()
        {
            try
            {
                using (_client = new TcpClient())
                {
                    // Start to connect simulator.
                    await _client.ConnectAsync(server, port);

                    if (_client.Connected)
                    {
                        using (_stream = _client.GetStream())
                        {

                            // Buffer to store the response bytes.
                            Byte[] data = new Byte[_client.ReceiveBufferSize];

                            _stream.ReadTimeout = timeout;

                            //For send out heartbeat
                            HeartbeatReports();

                            while (true)
                            {
                                // String to store the response ASCII representation.
                                String responseData = String.Empty;
                                // Read the first batch of the TcpServer response bytes.
                                Int32 bytes = await _stream.ReadAsync(data, 0, data.Length);

                                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                                // send the data to Signal
                                DecodeProtocolService.DecodeXML(responseData);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
                //waiting for 10s
                Thread.Sleep(10000);
                ReConnect();
            }
        }

        private void ReConnect()
        {
            //if cannot connect to simulator try to reconnect
            if (!_client.Connected)
                Connect();
        }

        private async void HeartbeatReports()
        {
            //if connected to simulator
            try
            {
                if (_client.Connected)
                {
                    while (true)
                    {
                        //wait 10s to send heartbeat
                        await Task.Delay(10000);
                        //send out the heartbeat
                        Byte[] sendData = System.Text.Encoding.UTF8.GetBytes("AA55\u0001\u0001\0\0\0\0");
                        await _stream.WriteAsync(sendData, 0, sendData.Length);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
        }
    }
}
