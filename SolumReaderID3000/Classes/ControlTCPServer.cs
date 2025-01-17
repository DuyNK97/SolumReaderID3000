using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SolumReaderID3000.Classes
{
    public class ControlTCPServer
    {
        private TcpListener tcpServer;
        public readonly string serverIp;
        public readonly int serverPort;
        public bool IsRunning;

        public event Action<string> LotDataReceived; // Sự kiện nhận dữ liệu từ client

        public ControlTCPServer(string serverIp = "107.105.42.59", int serverPort = 2024)
        {
            this.serverIp = serverIp;
            this.serverPort = serverPort;
        }

        // Khởi động server
        public void Start()
        {
            try
            {
                IPAddress localAddr = IPAddress.Parse(serverIp);
                tcpServer = new TcpListener(localAddr, serverPort);
                tcpServer.Start();
                IsRunning = true;

                Console.WriteLine($"Server started on {serverIp}:{serverPort}");

                // Bắt đầu lắng nghe client trong một luồng riêng
                Thread acceptClientsThread = new Thread(AcceptClients);
                acceptClientsThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting server: {ex.Message}");
            }
        }

        // Dừng server
        public void Stop()
        {
            try
            {
                IsRunning = false;
                tcpServer?.Stop();
                Console.WriteLine("Server stopped.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error stopping server: {ex.Message}");
            }
        }

        // Chấp nhận kết nối từ client
        private void AcceptClients()
        {
            while (IsRunning)
            {
                try
                {
                    Console.WriteLine("Waiting for a connection...");
                    TcpClient client = tcpServer.AcceptTcpClient(); // Chấp nhận kết nối từ client
                    Console.WriteLine("Client connected.");

                    // Xử lý dữ liệu từ client trong một luồng riêng
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.Start();

                    //if(client.Connected) return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error accepting client: {ex.Message}");
                }
            }
        }

        // Xử lý client
        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            try
            {
                while (client.Connected)
                {
                    // Nhận dữ liệu từ client
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead > 0)
                    {
                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"Received data: {receivedData}");

                        // Kích hoạt sự kiện nếu có dữ liệu "Lot:"
                        LotDataReceived?.Invoke(receivedData);

                        // Gửi phản hồi lại client
                        string response = $"Server received: {receivedData}";
                        byte[] responseData = Encoding.UTF8.GetBytes(response);
                        stream.Write(responseData, 0, responseData.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling client: {ex.Message}");
            }
            finally
            {
                //client.Close();
                //Console.WriteLine("Client disconnected.");
            }
        }
    }
}
