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
        public enum eFinalResult
        {
            OK,
            NG,
        }
        public ClassifyResult() { }
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void ProperChanged([CallerMemberName] string caller = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
    }
}
