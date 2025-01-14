using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SolumReaderID3000.Classes
{
    public class ControlTCP
    {
        private TcpListener server;
        private bool isRunning;
        private readonly string serverIp;
        private readonly int serverPort;
        public event Action<string> LotDataReceived;

        public ControlTCP(string serverIp = "127.0.0.1", int serverPort = 2024)
        {
            this.serverIp = serverIp;
            this.serverPort = serverPort;
            StartServer(); 
        }

        public void StartServer()
        {
            try
            {
                server = new TcpListener(IPAddress.Parse(serverIp), serverPort);
                server.Start();
                isRunning = true;
                Console.WriteLine("Server started...");

                while (isRunning)
                {
                    
                    TcpClient connectedClient = server.AcceptTcpClient();
                    Console.WriteLine("Client connected!");

                    ProcessClient(connectedClient);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in server: " + ex.Message);
            }
        }

        private void ProcessClient(TcpClient client)
        {
            using (client)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];

                try
                {
                    while (client.Connected)
                    {
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead).TrimEnd('\0');
                            ProcessReceivedData(receivedData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error processing client: " + ex.Message);
                }
                finally
                {
                    Console.WriteLine("Client disconnected.");
                }
            }
        }

        private void ProcessReceivedData(string data)
        {
            if (data.Length > 1)
            {
                Console.WriteLine("Received data: " + data);
                LotDataReceived?.Invoke(data);
            }
        }

        public void StopServer()
        {
            isRunning = false;
            server?.Stop();
            Console.WriteLine("Server stopped.");
        }

        public void SendDataToClient(TcpClient client, string data)
        {
            if (client.Connected)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                stream.Write(buffer, 0, buffer.Length);
                Console.WriteLine("Data sent to client: " + data);
            }
        }
        public void SendMessageToClient(string message)
        {
            if (server != null)
            {
                TcpClient client = server.GetConnectedClient(); // Đây là một hàm giả định bạn có để lấy client đã kết nối
                if (client != null)
                {
                    server.SendDataToClient(client, message);
                }
            }
        }
    }
}
