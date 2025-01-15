using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SolumReaderID3000
{
    [Serializable]
    public class SettingParams : CreateClassByPath<SettingParams>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void ProperChanged([CallerMemberName] string caller = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        //public Dictionary<string, SettingParam> Parameters { get; set; } = new Dictionary<string, SettingParam>();
        public List<SettingParam> Parameters { get; set; } = new List<SettingParam>();
    }

    public class SettingParam : CreateClass<SettingParam>
    {
        public string ModelName { get; set; }
        public float Exposure { get; set; }
        public float Gain { get; set; }

        public int Length {  get; set; }    
        public string Format {  get; set; } 

        public int ModelQty { get; set; }
    }

}
