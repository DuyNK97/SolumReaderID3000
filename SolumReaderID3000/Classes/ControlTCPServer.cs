using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MSFactoryDLL;

namespace SolumReaderID3000.Classes
{
    public class ControlTCPServer
    {
        //public delegate void DataReceivedEventHandler(object sender, string message);
        //public event DataReceivedEventHandler DataReceived;

        //IPEndPoint IP;
        //Socket server;
        //Socket client;  // Giữ một kết nối client duy nhất

        //public ControlTCPServer()
        //{
        //    Connect();
        //}

        //void Connect()
        //{
        //    IP = new IPEndPoint(IPAddress.Parse("192.168.1.26"), 2024);
        //    server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

        //    try
        //    {
        //        server.Bind(IP);
        //        Thread Listen = new Thread(() =>
        //        {
        //            server.Listen(50);
        //            Console.WriteLine("Server is listening...");
        //            // Chỉ chấp nhận một client duy nhất
        //            client = server.Accept();  // Chờ client kết nối
        //            Console.WriteLine("Client connected.");

        //            // Sau khi client kết nối, không cho phép thêm kết nối nữa
        //            server.Close();

        //            // Bắt đầu nhận dữ liệu từ client
        //            Thread receive = new Thread(() => Receive());
        //            receive.IsBackground = true;
        //            receive.Start();
        //        });
        //        Listen.IsBackground = true;
        //        Listen.Start();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error during server setup: {ex.Message}");
        //    }
        //}

        //public void Close()
        //{
        //    server.Close();
        //    client?.Close();
        //}

        //void Send(Socket client, string message)
        //{
        //    if (client != null && !string.IsNullOrEmpty(message) && client.Connected)
        //    {
        //        byte[] data = Encoding.UTF8.GetBytes(message); // Chuyển đổi string thành byte[]
        //        client.Send(data);
        //    }
        //}

        //void Receive()
        //{
        //    try
        //    {
        //        while (client.Connected)
        //        {
        //            byte[] buffer = new byte[1024]; // Dùng buffer nhỏ hơn
        //            int bytesRead = client.Receive(buffer);

        //            if (bytesRead > 0)
        //            {
        //                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead); // Chuyển đổi byte[] thành string
        //                if (string.IsNullOrEmpty(message))
        //                {
        //                    client.Close();
        //                    break;
        //                }
        //                AnalyzeData(message); // Phân tích dữ liệu và gửi kết quả lại cho client
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error in receiving data: {ex.Message}");
        //        client.Close();
        //    }
        //}

        //private void AnalyzeData(string message)
        //{
        //    // Kiểm tra tính hợp lệ của dữ liệu từ client
        //    bool isValid = message.Contains("SOLUM") && message.Length >= 30;

        //    // Gửi lại kết quả cho client (OK hoặc NG)
        //    SendMessage(isValid ? "OK" : "NG");

        //    // Gọi sự kiện nhận dữ liệu
        //    OnDataReceived(message);
        //}

        //protected virtual void OnDataReceived(string message)
        //{
        //    DataReceived?.Invoke(this, message);
        //}

        //// Gửi tin nhắn đến client duy nhất
        //public void SendMessage(string message)
        //{
        //    Send(client, message);
        //}



        public delegate void DataReceivedEventHandler(object sender, string message);
        public event DataReceivedEventHandler DataReceived;
        public readonly string serverIp = "107.105.42.59";
        public readonly int serverPort = 2024;
        IPEndPoint IP;
        Socket server;
        List<Socket> clientList;

        public ControlTCPServer()
        {
            Connect();
        }

        void Connect()
        {
            clientList = new List<Socket>();
            IP = new IPEndPoint(IPAddress.Parse("107.105.42.59"), 2024);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            try
            {
                server.Bind(IP);
                Thread Listen = new Thread(() =>
                {
                    while (true)
                    {
                        server.Listen(50);
                        Socket client = server.Accept();
                        //if (clientList.Count > 0)
                        //    for (int i = 0; i < clientList.Count; i++)
                        //    {
                        //        if (clientList[i].Connected && clientList.Count > 0)
                        //        {
                        try
                        {
                            if (clientList.Count > 0)
                                clientList[clientList.Count - 1].Close();
                        }
                        catch (Exception ex)
                        {
                            ClassCommon.Common.SaveLogString(eSAVING_LOG_TYPE.EXCEPTION, "Exception when close last Client: " + ex.Message);
                        }
                        
                        //clientList[i].Dispose();
                        //            clientList.Remove(clientList[i]);
                        //        }
                        //    }
                        clientList.Add(client);
                        //ClassCommon.Common.SaveLogString(eSAVING_LOG_TYPE.PROGRAM, "====Number Client ====" + clientList.Count.ToString() + " ==== " + client.RemoteEndPoint);
                        Console.WriteLine("====Number Client ====" + clientList.Count.ToString() + " ==== " + client.RemoteEndPoint);
                        Thread receive = new Thread(() => Receive(client));
                        receive.IsBackground = true;
                        receive.Start();
                    }
                });
                Listen.IsBackground = true;
                Listen.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during server setup: {ex.Message}");
            }
        }

        public void Close()
        {
            server.Close();
        }
        //public void Send(Socket client, string message)
        //{
        //    if (client != null && !string.IsNullOrEmpty(message) && client.Connected)
        //    {
        //        byte[] data = Encoding.UTF8.GetBytes(message); // Chuyển đổi string thành byte[]
        //        client.Send(data);
        //    }
        //}

        void Receive(Socket client)
        {
            try
            {
                while (client.Connected)
                {
                    byte[] buffer = new byte[128];
                    int bytesRead = client.Receive(buffer);

                    if (bytesRead > 0)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead); // Chuyển đổi byte[] thành string
                        if (string.IsNullOrEmpty(message))
                        {
                            clientList.Remove(client);
                            client.Close();
                            break;
                        }
                        AnalyzeData(message, client);
                    }
                }

                Task.Delay(10).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in receiving data: {ex.Message}");
                clientList.Remove(client);
                client.Close();
            }
        }

        private void AnalyzeData(string message, Socket client)
        {
            //bool isValid = message.Contains("SOLUM") && message.Length >= 30;

            //SendMessage(isValid ? "OK" : "NG");

            OnDataReceived(message);
        }

        protected virtual void OnDataReceived(string message)
        {
            DataReceived?.Invoke(this, message);
        }

        //public void SendMessage(string message)
        //{
        //    //sTask.Delay(1000).Wait();
        //    foreach (Socket item in clientList)
        //    {
        //        Send(item, message);
        //    }
        //}
    }
}
