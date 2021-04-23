using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Spoon.SIDC
{
    class Logs
    {
        string idUser = "Lenard";


        public static void WriteLog(string text1, string strLog)
        {
            try
            {
                string logsString = "";
                string ipadd = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(o => o.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).First().ToString();

                //http://stackoverflow.com/questions/20185015/how-to-write-log-file-in-c
                StreamWriter log;
                FileStream fileStream = null;
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo;

                string executableLocation = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);

                // string path = ConfigurationManager.AppSettings["CSVPathBranch"].ToString();
                //string logFilePath = path + "\\Logs\\";
                string logFilePath = executableLocation + "\\Logs\\";
                //string logFilePath = "\\" + "\\sap19\\Users\\Administrator\\OpenCloseLogs\\";
                logFilePath = logFilePath + "Log-" + System.DateTime.Now.ToString("MM-dd-yyyy_hhmmss") + "." + "csv";
                logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }
                log = new StreamWriter(fileStream);
                if (string.IsNullOrEmpty(text1) == true || text1 == "")
                    logsString = strLog + " \n @@ -- Time: " + DateTime.Now.ToString("HH:mm:ss tt") + " @@ -- PC-NAME: " + Environment.MachineName + " // IP: " + ipadd + "\r\n\r\n";
                else
                    logsString = text1 + " \n @@ -- " + strLog + " @@ -- Time: " + DateTime.Now.ToString("HH:mm:ss tt") + " @@ -- PC-NAME: " + Environment.MachineName + " // IP: " + ipadd + " // DATABASE&SERVER: ";

                log.WriteLine(logsString);

                log.Close();
                Process.Start(logFilePath);

                //Mailer mailer = new Mailer(fileStream.Name);
                //mailer.SendMessage("SPORK Notification", "Please see attached file");

            }
            catch (Exception)
            {

            }
        }


    }
}
