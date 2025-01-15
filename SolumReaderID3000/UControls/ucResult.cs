using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolumReaderID3000.UControls
{
    public partial class ucResult : UserControl
    {
        public ucResult()
        {
            InitializeComponent();
            Load += UcResultTable_Load;
            ClassifyResult.Instance.PropertyChanged += Data_PropertyChanged;
        }
        private void UcResultTable_Load(object sender, EventArgs e)
        {
            UpdateView();            
        }

        private void Data_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateView();
        }
        private void UpdateView()
        {
            this.Invoke(new Action(() =>
            {
                //labelNumTrayTotal.Text = (ClassifyResult.Instance.TotalLot == 0 ? "0" : ClassifyResult.Instance.TotalLot.ToString("###,###"));
                //labelNumTrayTotal.Refresh();
                labelCountOK.Text = (ClassifyResult.Instance.OK == 0 ? "0" : ClassifyResult.Instance.OK.ToString("###,###"));
                labelCountOK.Refresh();
                labelCountNG.Text = (ClassifyResult.Instance.NG == 0 ? "0" : ClassifyResult.Instance.NG.ToString("###,###"));
                labelCountNG.Refresh();
                labelCountTotal.Text = (ClassifyResult.Instance.Total == 0 ? "0" : ClassifyResult.Instance.Total.ToString("###,###"));
                labelCountTotal.Refresh();
                labelOKPercent.Text = (ClassifyResult.Instance.OKPercent == 0 ? "0.0%" : ClassifyResult.Instance.OKPercent.ToString("F1") + "%");
                labelOKPercent.Refresh();
                labelNGPercent.Text = (ClassifyResult.Instance.NGPercent == 0 ? "0.0%" : ClassifyResult.Instance.NGPercent.ToString("F1") + "%");
                labelNGPercent.Refresh();
            }));
            if (Global.modelList.Count == Global.Modelqty)
            {
                Global.modelList.Clear();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            string message = "Do you want to clear data?";
            string title = "Clear data Window";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ClassifyResult.Instance.ClearData();
                ClassifyResult.Instance.Save();
            }
        }
    }
}
