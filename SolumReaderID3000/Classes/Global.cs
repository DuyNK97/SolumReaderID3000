using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolumReaderID3000
{
    public class Global
    {
        public static ReaderControl readerControl;
        public static int length { get; set; } = 20;
        public static string format = "";
        public static bool IsExits;

        public static string Model;
        public static int Modelqty;


        public static List<string> modelList = new List<string>();

        public static bool CheckDuplicateAndAdd(string newItem)
        {
            // Kiểm tra xem phần tử đã tồn tại trong danh sách chưa
            if (modelList.Contains(newItem))
            {
                return false; // Nếu có, không thêm và trả về false
            }
            else
            {
                // Nếu không trùng, thêm phần tử vào danh sách
                if (modelList.Count < Global.Modelqty)
                {
                    modelList.Add(newItem);
                    return true; // Thêm thành công và trả về true
                }
                else
                {
                    return false; // Nếu danh sách đã đầy, không thêm
                }
            }
        }


    }
}
