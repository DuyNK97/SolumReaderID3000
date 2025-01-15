using MSFactoryDLL;
using SolumReaderID3000.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolumReaderID3000
{
    // <summary>
    /// Tao 1 class bat ky. chi duy nhat no duoc tao ra khong new duoc
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CreateClass<T> where T : new()
    {
        private static T _instance;

        static object _lockObj = new object();
        public static T Instance
        {
            get
            {
                lock (_lockObj)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }
                }
                return _instance;
            }
        }
    }
    public class CreateClassByPath<T> where T : new()
    {
        private static T _instance;

        static object _lockObj = new object();
        public static T Instance
        {
            get
            {
                lock (_lockObj)
                {
                    if (_instance == null)
                    {
                        _instance = ClassCommon.ReadAndWriteXml.ReadFileFromXml<T>(SystemFilePath.GetPath(typeof(T)));
                    }
                }
                return _instance;
            }
        }

        public void Save()
        {
            ClassCommon.ReadAndWriteXml.WriteFileToXml<T>(SystemFilePath.GetPath(typeof(T)), _instance);
        }
        public void ReLoad()
        {
            lock (_lockObj)
            {
                string filePath = SystemFilePath.GetPath(typeof(T));
                _instance = ClassCommon.ReadAndWriteXml.ReadFileFromXml<T>(filePath);
            }
        }

    }


    public class SystemFilePath
    {
        public static string PathNaviAIInspection()
        {

            return string.Format(@"Recipe\Navi\NaviAI.dat");

        }
        public static string GetPath(Type type)
        {
            
            if (type == typeof(ClassifyResult))
                return string.Format($"{Application.StartupPath}\\UserConfig\\ClassifyResult.xml");
            if (type == typeof(SettingParams))
                return string.Format($"{Application.StartupPath}\\UserConfig\\SettingParams.xml");         
            return null;
        }
    }
}
