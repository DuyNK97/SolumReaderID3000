using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace SolumReaderID3000.Classes
{
    public class ControlTCPClient
    {
        private TcpClient tcpClient;
        private NetworkStream stream;
        public readonly string serverIp;
        public readonly int serverPort;
        public event Action<string> LotDataReceived;

        public bool IsConnected = false;

        public ControlTCPClient(string serverIp = "107.105.42.220", int serverPort = 2024)
        {
            this.serverIp = serverIp;
            this.serverPort = serverPort;
        }
        public bool IsConnect()
        {
            try
            {
                if (tcpClient != null && tcpClient.Connected)
                {
                    IsConnected = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                Disconnect();
                return false;
            }
            
        }
        public void Connect()
        {
            while (!IsConnect())
            {
                try
                {
                    tcpClient = new TcpClient();
                    tcpClient.Connect(serverIp, serverPort);
                    stream = tcpClient.GetStream();
                    Console.WriteLine("Connected to server.");
                    StartReceiving();
                    IsConnected = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error connecting to server: {ex.Message}");
                    Console.WriteLine("Retrying in 5 seconds...");
                    Thread.Sleep(5000); // Thử lại sau 5 giây
                }
            }
        }

        static Stopwatch stopwatchI0 = new Stopwatch();

        public void Send(string message)
        {
            if (!IsConnect())
            {
                Console.WriteLine("Client is not connected to server.");
                return;
            }

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Data sent to server.");

                stopwatchI0.Restart();
                stopwatchI0.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending data: {ex.Message}");
            }
        }

        private void StartReceiving()
        {
            Thread receivingThread = new Thread(() =>
            {
                while (IsConnect())
                {
                    Receive();
                }
            });
            receivingThread.IsBackground = true;
            receivingThread.Start();
        }

        public void Receive()
        {
            if (!IsConnect())
            {
                Console.WriteLine("Client is not connected to server.");
                Reconnect();
                return;
            }

            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                StringBuilder receivedDataBuilder = new StringBuilder();

                while (IsConnected)
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead > 0)
                    {
                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        receivedDataBuilder.Append(receivedData);

                        Console.WriteLine($"Data received from server: {receivedData} Time: {stopwatchI0.ElapsedMilliseconds}");

                        if (!string.IsNullOrEmpty(receivedData))
                        {
                            string completeMessage = receivedDataBuilder.ToString().Trim();
                            var elapsedTime = stopwatchI0.ElapsedMilliseconds.ToString();
                            stopwatchI0.Stop();
                            LotDataReceived?.Invoke(completeMessage + "\n Time: " + elapsedTime);

                            receivedDataBuilder.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data received from server.");
                        break;
                    }
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Socket error: {ex.Message}");
                Reconnect();
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine($"Stream has been closed: {ex.Message}");
                Reconnect();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving data: {ex.Message}");
                Reconnect();
            }
        }

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

        private void Reconnect()
        {
            Console.WriteLine("Attempting to reconnect to the server...");
            Disconnect();
            Connect();
        }
    }
}
