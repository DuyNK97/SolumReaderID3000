using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace SolumReaderID3000
{

    [Serializable]
    public class ClassifyResult : CreateClassByPath<ClassifyResult>, INotifyPropertyChanged
    {
        public string Model { get; set; }
        public string CreationTime { get; set; } = string.Empty;
        public string ApplicationName { get; set; } = "SOLUM GATHERING DATA";
        private string _ServerIP { get; set; } = "127.0.0.1";
        private bool _SaveRunImage= true;
        public bool SaveRunImage { get => _SaveRunImage; set => _SaveRunImage = value; }
        public string ServerIP { get => _ServerIP; set => _ServerIP = value; }    
        private int _RPort= 2024;
        private int _Sport= 2025;
        public int RPort { get => _RPort; set => _RPort = value; }
        public int Sport { get => _Sport; set => _Sport = value; }

        public int seri { get; set; } = 1;

        public string SerialPort { get; set; } = "COM4";
        public int baudrate { get; set; } = 115200;


        //public int TotalLot { get; set; }

        public enum eFinalResult
        {
            OK,
            NG,
        }
        public ClassifyResult() {
        

        }
        private eFinalResult _runResult;
        [XmlIgnore]
        public eFinalResult RunResult
        {
            get { return _runResult; }
            set
            {
                _runResult = value;
                switch (value)
                {
                    case eFinalResult.OK:
                        
                        OK++;
                        break;
                    case eFinalResult.NG:
                        NG++;
                        break;
                }
                ProperChanged("RunResult");
            }
        }
        public int OK { get; set; }
        public int NG { get; set; }

        [XmlIgnore]
        public int Total { get { return OK + NG; } }

        [XmlIgnore]
        public int TotalLot { get { return (int)(OK / (double)Global.Modelqty); } }


        [XmlIgnore]
        public double NGPercent
        {
            get
            {
                if (Total == 0) return 0;
                return NG * 100.0 / Total;
            }
        }
        [XmlIgnore]
        public double OKPercent
        {
            get
            {
                if (Total == 0) return 0;
                return OK * 100.0 / Total;
            }
        }
        
        public void ResetData()
        {
            OK = NG = 0;
        }

        public void ClearData()
        {
            OK = 0;
            NG = 0;            
            ProperChanged("RunResult");
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void ProperChanged([CallerMemberName] string caller = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
    }
}
