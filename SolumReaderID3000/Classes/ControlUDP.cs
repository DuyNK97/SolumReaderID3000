using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace SolumReaderID3000.Classes
{
    public class ControlUDP
    {
        private UdpClient udpClient;
        private IPEndPoint localEndPoint;      // Địa chỉ IP và cổng lắng nghe của server
        private IPEndPoint remoteEndPoint;     // Địa chỉ IP và cổng của client
        private List<IPEndPoint> clientList;   // Danh sách các client đang kết nối (UDP không có khái niệm "kết nối")

        public ControlUDP()
        {
            localEndPoint = new IPEndPoint(IPAddress.Parse(ClassifyResult.Instance.ServerIP), ClassifyResult.Instance.RPort);
            remoteEndPoint = new IPEndPoint(IPAddress.Parse(ClassifyResult.Instance.ServerIP), ClassifyResult.Instance.Sport);  // Sử dụng địa chỉ IP của máy tính này nếu chạy trên cùng một PC

            udpClient = new UdpClient(ClassifyResult.Instance.RPort);  // Khởi tạo UdpClient để lắng nghe trên cổng receivePort
            clientList = new List<IPEndPoint>();
        }

        // Bắt đầu lắng nghe các gói UDP
        public void StartReceiving()
        {
            Thread receiveThread = new Thread(ReceiveMessages);
            receiveThread.IsBackground = true;  // Đảm bảo thread nhận không cản trở chương trình chính
            receiveThread.Start();
        }

        // Gửi tin nhắn tới client
        public void SendMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            udpClient.Send(data, data.Length, remoteEndPoint);  // Gửi dữ liệu tới remoteEndPoint (client)
            Console.WriteLine($"Gửi: {message}");
        }

        // Hàm nhận tin nhắn
        private void ReceiveMessages()
        {
            while (true)
            {
                try
                {
                    // Nhận dữ liệu từ bất kỳ địa chỉ IP nào (dùng IPEndPoint.Any)
                    byte[] data = udpClient.Receive(ref localEndPoint);  // Nhận dữ liệu và lấy địa chỉ client gửi
                    string message = Encoding.UTF8.GetString(data);

                    // Xử lý tin nhắn nhận được
                    if (!string.IsNullOrEmpty(message))
                    {
                        Console.WriteLine($"Nhận từ {localEndPoint.Address}:{localEndPoint.Port} - {message}");
                        // Gọi sự kiện hoặc phương thức phân tích dữ liệu
                        AnalysisReceiveData(message, localEndPoint);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi nhận dữ liệu: " + ex.Message);
                }
            }
        }

        // Phân tích dữ liệu nhận được từ client
        private void AnalysisReceiveData(string message, IPEndPoint clientEndPoint)
        {
            if (message.Contains("RS"))
            {
                string[] listFrame = message.Split(new string[] { "xxx" }, StringSplitOptions.None);
                for (int i = 0; i < listFrame.Length; i++)
                {
                    if (string.IsNullOrEmpty(listFrame[i])) continue;
                    string[] listData = listFrame[i].Split(',');
                    if (listData[0] == "RS")
                    {
                        UpdateDataResult(listFrame[i]);
                        AddMessage($"Client {clientEndPoint.ToString()} gửi: {listFrame[i]}");
                    }
                }
            }
            else if (message.Contains("ST"))
            {
                string[] listFrame = message.Split(new string[] { "xxx" }, StringSplitOptions.None);
                for (int i = 0; i < listFrame.Length; i++)
                {
                    if (string.IsNullOrEmpty(listFrame[i])) continue;
                    string[] listData = listFrame[i].Split(',');
                    if (listData[0] == "ST")
                    {
                        UpdateStatus(listFrame[i]);
                    }
                }
            }
        }

        // Cập nhật kết quả nhận được
        private void UpdateDataResult(string message)
        {
            Console.WriteLine($"Cập nhật dữ liệu: {message}");
        }

        // Cập nhật trạng thái nhận được
        private void UpdateStatus(string message)
        {
            Console.WriteLine($"Cập nhật trạng thái: {message}");
        }

        // Hàm ghi lại tin nhắn (đối với việc debug hoặc logging)
        private void AddMessage(string s)
        {
            Console.WriteLine(s);  // Chỉ đơn giản in ra console, có thể thay bằng log thực tế
        }

        // Đóng kết nối UDP
        public void Close()
        {
            udpClient.Close();
        }

        // Đóng kết nối và dọn dẹp khi kết thúc
        ~ControlUDP()
        {
            udpClient.Close();
        }
    }
}
