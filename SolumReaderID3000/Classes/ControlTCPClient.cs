//using System;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//using System.Diagnostics;
//using System.Windows.Forms;

//namespace SolumReaderID3000.Classes
//{
//    public class ControlTCPClient
//    {
//        private TcpClient tcpClient;
//        private NetworkStream stream;
//        public readonly string serverIp;
//        public readonly int serverPort;
//        public event Action<string> LotDataReceived;

//        public bool IsConnected = false;

//        public ControlTCPClient(string serverIp = "107.105.42.220", int serverPort = 2024)
//        {
//            this.serverIp = serverIp;
//            this.serverPort = serverPort;
//        }
//        public bool IsConnect()
//        {
//            try
//            {
//                if (tcpClient != null && tcpClient.Connected)
//                {
//                    IsConnected = true;
//                    return true;
//                }
//                else
//                {
//                    return false;
//                }
//            }
//            catch (Exception)
//            {
//                Disconnect();
//                return false;
//            }

//        }
//        public void Connect(int timeoutSeconds = 30, int maxRetries = 5)
//        {
//            int attempts = 0;
//            var startTime = DateTime.UtcNow;

//            while (!IsConnect())
//            {
//                if (attempts >= maxRetries ||
//                    (DateTime.UtcNow - startTime).TotalSeconds >= timeoutSeconds)
//                {
//                    MessageBox.Show("Can not connection to "+ serverIp + ":"+ serverPort +", please check Firewall");
//                    Console.WriteLine("Connect timeout or max retries reached!");
//                    break;
//                }

//                attempts++;

//                try
//                {
//                    tcpClient = new TcpClient();
//                    tcpClient.Connect(serverIp, serverPort);
//                    stream = tcpClient.GetStream();

//                    Console.WriteLine("Connected to server.");
//                    StartReceiving();
//                    IsConnected = true;
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Attempt {attempts}: {ex.Message}");
//                    Thread.Sleep(5000);
//                }
//            }
//        }


//        static Stopwatch stopwatchI0 = new Stopwatch();

//        public void Send(string message)
//        {
//            if (!IsConnect())
//            {
//                Console.WriteLine("Client is not connected to server.");
//                return;
//            }

//            try
//            {
//                byte[] data = Encoding.UTF8.GetBytes(message);
//                stream.Write(data, 0, data.Length);
//                Console.WriteLine("Data sent to server.");

//                stopwatchI0.Restart();
//                stopwatchI0.Start();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error sending data: {ex.Message}");
//            }
//        }

//        private void StartReceiving()
//        {
//            Thread receivingThread = new Thread(() =>
//            {
//                while (IsConnect())
//                {
//                    Receive();
//                }
//            });
//            receivingThread.IsBackground = true;
//            receivingThread.Start();
//        }

//        public void Receive()
//        {
//            if (!IsConnect())
//            {
//                Console.WriteLine("Client is not connected to server.");
//                Reconnect();
//                return;
//            }

//            try
//            {
//                byte[] buffer = new byte[1024];
//                int bytesRead = 0;
//                StringBuilder receivedDataBuilder = new StringBuilder();

//                while (IsConnected)
//                {
//                    bytesRead = stream.Read(buffer, 0, buffer.Length);

//                    if (bytesRead > 0)
//                    {
//                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
//                        receivedDataBuilder.Append(receivedData);

//                        Console.WriteLine($"Data received from server: {receivedData} Time: {stopwatchI0.ElapsedMilliseconds}");

//                        if (!string.IsNullOrEmpty(receivedData))
//                        {
//                            string completeMessage = receivedDataBuilder.ToString().Trim();
//                            var elapsedTime = stopwatchI0.ElapsedMilliseconds.ToString();
//                            stopwatchI0.Stop();
//                            LotDataReceived?.Invoke(completeMessage + "\n Time: " + elapsedTime);

//                            receivedDataBuilder.Clear();
//                        }
//                    }
//                    else
//                    {
//                        Console.WriteLine("No data received from server.");
//                        break;
//                    }
//                }
//            }
//            catch (SocketException ex)
//            {
//                Console.WriteLine($"Socket error: {ex.Message}");
//                Reconnect();
//            }
//            catch (ObjectDisposedException ex)
//            {
//                Console.WriteLine($"Stream has been closed: {ex.Message}");
//                Reconnect();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error receiving data: {ex.Message}");
//                Reconnect();
//            }
//        }

//        public void Disconnect()
//        {
//            try
//            {
//                stream?.Close();
//                tcpClient?.Close();
//                Console.WriteLine("Disconnected from server.");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error disconnecting: {ex.Message}");
//            }
//        }

//        public void Reconnect()
//        {
//            Console.WriteLine("Attempting to reconnect to the server...");
//            Disconnect();
//            Connect();
//        }
//    }
//}

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
        private Thread receiveThread;
        private CancellationTokenSource cancelToken;

        public readonly string ServerIp;
        public readonly int ServerPort;

        public bool IsConnected { get; private set; } = false;

        public event Action<string> DataReceived;

        static Stopwatch stopwatch = new Stopwatch();

        public ControlTCPClient(string serverIp = "107.105.42.220", int serverPort = 2024)
        {
            ServerIp = serverIp;
            ServerPort = serverPort;
        }

        // ----------------------------------------------------------
        // Kiểm tra trạng thái kết nối TCP chính xác
        // ----------------------------------------------------------
        private bool CheckSocketConnected()
        {
            try
            {
                if (tcpClient == null || !tcpClient.Connected)
                    return false;

                Socket socket = tcpClient.Client;

                bool readReady = socket.Poll(0, SelectMode.SelectRead);
                bool noData = socket.Available == 0;

                if (readReady && noData)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsConnect() => CheckSocketConnected();

        // ----------------------------------------------------------
        // Connect
        // ----------------------------------------------------------
        public bool Connect()
        {
            try
            {
                Disconnect(); // cleanup trước đó

                tcpClient = new TcpClient();
                tcpClient.Connect(ServerIp, ServerPort);

                stream = tcpClient.GetStream();
                IsConnected = true;

                cancelToken = new CancellationTokenSource();
                StartReceiving(cancelToken.Token);

                Console.WriteLine("Connected to server.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connect failed: " + ex.Message);
                IsConnected = false;
                return false;
            }
        }

        // ----------------------------------------------------------
        // StartReceiving
        // ----------------------------------------------------------
        private void StartReceiving(CancellationToken token)
        {
            receiveThread = new Thread(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    if (!IsConnect())
                    {
                        Console.WriteLine("Lost connection.");
                        IsConnected = false;
                        Reconnect();
                        return;
                    }

                    Receive();
                }
            });

            receiveThread.IsBackground = true;
            receiveThread.Start();
        }

        // ----------------------------------------------------------
        // Receive data
        // ----------------------------------------------------------
        private void Receive()
        {
            try
            {
                if (stream == null || !stream.CanRead)
                    return;

                byte[] buffer = new byte[2048];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                if (bytesRead <= 0)
                    return;

                string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                stopwatch.Stop();
                var elapsed = stopwatch.ElapsedMilliseconds;

                DataReceived?.Invoke(received.Trim() +
                    " | Time: " + elapsed.ToString());

                stopwatch.Reset();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Receive error: " + ex.Message);
                Reconnect();
            }
        }

        // ----------------------------------------------------------
        // Send data
        // ----------------------------------------------------------
        public void Send(string message)
        {
            if (!IsConnect())
            {
                Console.WriteLine("Not connected.");
                return;
            }

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);

                stopwatch.Restart();
                Console.WriteLine("Sent: " + message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Send error: " + ex.Message);
                Reconnect();
            }
        }

        // ----------------------------------------------------------
        // Graceful Disconnect
        // ----------------------------------------------------------
        public void Disconnect()
        {
            try
            {
                cancelToken?.Cancel();

                stream?.Close();
                tcpClient?.Close();

                IsConnected = false;
                Console.WriteLine("Disconnected.");
            }
            catch { }
        }

        // ----------------------------------------------------------
        // Reconnect with delay
        // ----------------------------------------------------------
        public void Reconnect()
        {
            Console.WriteLine("Reconnecting...");

            Disconnect();
            Thread.Sleep(2000);

            Connect();
        }
    }
}

