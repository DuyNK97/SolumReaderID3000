using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolumReaderID3000.Classes
{
    public class ControlTCP
    {
        private TcpClient tcpClient;
        private NetworkStream stream;
        private readonly string serverIp;
        private readonly int serverPort;
        public event Action<string> LotDataReceived;

        public bool IsConnected => tcpClient != null && tcpClient.Connected;

        public ControlTCP(string serverIp = "107.105.30.100", int serverPort = 2024)
        {
            this.serverIp = serverIp;
            this.serverPort = serverPort;
        }

        // Kết nối tới server
        public void Connect()
        {
            try
            {
                tcpClient = new TcpClient();
                tcpClient.Connect(serverIp, serverPort);
                stream = tcpClient.GetStream();
                Console.WriteLine("Connected to server.");
                StartReceiving();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to server: {ex.Message}");
            }
        }

        // Gửi dữ liệu tới server
        public void Send(string message)
        {
            if (!IsConnected)
            {
                Console.WriteLine("Client is not connected to server.");
                return;
            }

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Data sent to server.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending data: {ex.Message}");
            }
        }

        // Bắt đầu nhận dữ liệu
        private void StartReceiving()
        {
            Thread receivingThread = new Thread(() =>
            {
                while (IsConnected)
                {
                    Receive();
                }
            });
            receivingThread.Start();
        }

        // Nhận dữ liệu từ server
        public void Receive()
        {
            if (!IsConnected)
            {
                Console.WriteLine("Client is not connected to server.");
                return;
            }

            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Data received from server: {receivedData}");

                    //if (receivedData.Contains("Lot:"))
                    //{
                        LotDataReceived?.Invoke(receivedData); // Kích hoạt sự kiện nếu có dữ liệu "Lot:"
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving data: {ex.Message}");
            }
        }

        // Đóng kết nối
        public void Disconnect()
        {
            try
            {
                stream?.Close();
                tcpClient?.Close();
                Console.WriteLine("Disconnected from server.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error disconnecting: {ex.Message}");
            }
        }
    }
}
