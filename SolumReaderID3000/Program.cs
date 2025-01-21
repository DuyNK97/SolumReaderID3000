using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolumReaderID3000
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool result;
            var mutex = new Mutex(true, "UniqueAppId", out result);
            if (!result)
            {
                MessageBox.Show("Application is already running!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Application.Run(new fMain());
            GC.KeepAlive(mutex);
        }
    }
}
