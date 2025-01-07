using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System;

public class UDPControl
{
    private UdpClient udpClient;
    private int receivePort = 2025;   // Cổng nhận dữ liệu (dành cho server)
    private int sendPort = 2024;      // Cổng gửi dữ liệu (dành cho client)
    private string localIpAddress;
    private string remoteIpAddress = "192.168.1.14"; // Địa chỉ IP cố định

    public event Action<string, string, int> DataReceived;

    public UDPControl(string localIpAddress)
    {
        this.localIpAddress = localIpAddress;
        udpClient = new UdpClient(receivePort); // Khởi tạo UdpClient để lắng nghe cổng nhận
    }

    // Hàm bắt đầu lắng nghe (server)
    public void StartServer()
    {
        Thread receiveThread = new Thread(ReceiveMessages);
        receiveThread.IsBackground = true; // Đảm bảo luồng này không cản trở chương trình kết thúc
        receiveThread.Start();
    }

    // Gửi tin nhắn đến địa chỉ IP và cổng từ xa (client)
    public void SendMessage(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIpAddress), sendPort); // Cổng gửi là 2024
        udpClient.Send(data, data.Length, remoteEndPoint);
        Console.WriteLine($"Gửi: {message}");
    }

    private void ReceiveMessages()
    {
        //IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(localIpAddress), receivePort); // Lắng nghe trên cổng nhận 2025
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, receivePort); // Lắng nghe trên cổng nhận 2025
        Console.WriteLine("Chương trình đang chờ nhận tin nhắn...");

        while (true)
        {
            try
            {
                // Nhận dữ liệu từ bất kỳ địa chỉ nào
                byte[] data = udpClient.Receive(ref remoteEndPoint);
                string message = Encoding.UTF8.GetString(data);

                // Kiểm tra nếu địa chỉ IP nguồn không phải của chính máy mình và cổng không phải 2024
                if (remoteEndPoint.Port == sendPort)
                {
                    // Nếu có dữ liệu, gọi sự kiện DataReceived
                    DataReceived?.Invoke(message, remoteEndPoint.Address.ToString(), remoteEndPoint.Port);
                    Console.WriteLine($"Nhận: {message}");
                }
                else
                {
                    Console.WriteLine("Bỏ qua tin nhắn gửi lại từ chính mình");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong quá trình nhận dữ liệu: " + ex.Message);
            }
        }
    }

    // Đóng kết nối UDP
    public void Close()
    {
        udpClient.Close();
    }
}
