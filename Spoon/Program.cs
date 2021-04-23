using Spoon.DBConfig;
using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Spoon
{
    static class Program
    {
        private static Mutex mutex = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// SIDC POS  Online/Offline Node
        [STAThread]
        static void Main()
        {
            const string appName = "SIDC-Spoon";
            bool createdNew;

            mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                MessageBox.Show(appName + " is already running! Exiting the application.");
                return;
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string extractionPath = ConfigurationManager.AppSettings["CSVPathSource"].ToString();
            if (!Directory.Exists(extractionPath))
                System.IO.Directory.CreateDirectory(extractionPath);


            string filesPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + @"\Files");
            if (!Directory.Exists(filesPath))
                System.IO.Directory.CreateDirectory(filesPath);

            string processedPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + @"\Processed");
            if (!Directory.Exists(processedPath))
                System.IO.Directory.CreateDirectory(processedPath);

            string queuePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + @"\Queue");
            if (!Directory.Exists(queuePath))
                System.IO.Directory.CreateDirectory(queuePath);

            string extractedPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + @"\extracted");
            if (!Directory.Exists(extractedPath))
                System.IO.Directory.CreateDirectory(extractedPath);


            bool initSpoon = false;
            try
            {
                bool.TryParse(ConfigurationManager.AppSettings["Init"].ToString(), out initSpoon);
            }
            catch
            {
                initSpoon = false;
            }

            //Application.Run(new Admin.View.frmExtract());
            Application.Run(new Inventory.View.frmTransact());

            //if (initSpoon)
            //    Application.Run(new SIDC.View.frmSpoonSetup());
            //else
            //    Application.Run(new Admin.View.frmExtract());

        }
    }
}
