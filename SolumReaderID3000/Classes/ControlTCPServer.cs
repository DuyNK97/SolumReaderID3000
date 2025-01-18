using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace SolumReaderID3000.Classes
{
    public class ControlTCPServer
    {
        public delegate void DataReceivedEventHandler(object sender, string message);
        public event DataReceivedEventHandler DataReceived;
        public readonly string serverIp = "107.105.42.59";
        public readonly int serverPort= 2024;
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
                        clientList.Add(client);

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
                    byte[] buffer = new byte[1000000];
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
