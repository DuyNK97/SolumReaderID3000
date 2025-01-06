using MSFactoryDLL;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace SolumReaderID3000.Classes
{
    public class ControlUDP
    {
        private UdpClient _udpClient;
        private IPEndPoint _serverEndPoint;
        private Thread _receiveThread;
        private bool _isRunning;

        public delegate void MessageReceivedHandler(string message, IPEndPoint sender);
        public event MessageReceivedHandler MessageReceivedEvent;

        public ControlUDP(string serverIP, int serverPort)
        {
            _serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
            _udpClient = new UdpClient();
            _udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            _isRunning = true;

            // Start receiving thread
            _receiveThread = new Thread(ReceiveMessages);
            _receiveThread.IsBackground = true;
            _receiveThread.Start();
        }

        // Send a message to the server
        public void SendMessage(string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                _udpClient.Send(data, data.Length, _serverEndPoint);
                Console.WriteLine($"Message sent to {_serverEndPoint}: {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }

        // Receive messages in a background thread
        private void ReceiveMessages()
        {
            try
            {
                while (_isRunning)
                {
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] data = _udpClient.Receive(ref remoteEndPoint);

                    string message = Encoding.UTF8.GetString(data);
                    MessageReceivedEvent?.Invoke(message, remoteEndPoint);

                    Console.WriteLine($"Message received from {remoteEndPoint}: {message}");
                }
            }
            catch (SocketException ex) when (ex.SocketErrorCode == SocketError.Interrupted)
            {
                Console.WriteLine("UDP receiving thread stopped.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving message: {ex.Message}");
            }
        }

        // Stop the UDP client
        public void Close()
        {
            try
            {
                _isRunning = false;
                _udpClient.Close();
                _receiveThread?.Join();
                Console.WriteLine("UDP client closed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing UDP client: {ex.Message}");
            }
        }
        void AddMessage(string s)
        {
            ClassCommon.Common.SaveLogString(eSAVING_LOG_TYPE.PROGRAM, s);
        }

    }
}
