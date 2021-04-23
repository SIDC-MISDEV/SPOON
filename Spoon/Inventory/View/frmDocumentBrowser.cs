using Spoon.Admin.View;
using System;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Spoon.Inventory.View
{
    public partial class frmDocumentBrowser : Form
    {
        frmStatus frm;
        frmTransact _transactionForm;
        public string reference, path, originalFoldername;
        [DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(IntPtr Destination, int Length);

        public frmDocumentBrowser()
        {
            InitializeComponent();
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }
        private void frmDocumentBrowser_Load(object sender, EventArgs e)
        {
            //string reportPath = Path.GetDirectoryName(Application.ExecutablePath);
            //string reportFullPath = Path.Combine(reportPath + @"\Files");
            string reportFullPath = ConfigurationManager.AppSettings["CSVPathSource"].ToString();

            if (!File.Exists(reportFullPath))
            {
                System.IO.Directory.CreateDirectory(reportFullPath);
            }

            foreach (string s in Directory.GetDirectories(reportFullPath))
            {
                DirectoryInfo dir_info = new DirectoryInfo(s);
                dgvPcosting.Rows.Add(
                    dir_info.FullName,
                    dir_info.Name.Substring(0, dir_info.Name.IndexOf("_")),
                    dir_info.Name.Substring(dir_info.Name.IndexOf("_") + 1,
                    dir_info.Name.Length - (dir_info.Name.IndexOf("_") + 1)),
                    DateTime.Parse(
                            dir_info.LastWriteTime.ToString()
                            ).ToString("yyyy-MM-dd"),
                    dir_info.Name
                    );
            }


        }
        public void DirSearch(string sDir)
        {
            string path = sDir;
            DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.*"); //Getting Text files
                                                  //FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
            string str = "";
            foreach (FileInfo file in Files)
            {

                dgvPcosting.Rows.Add(
                    file.Directory.Name
                    );
                //dgvPcosting.Rows.Add(
                //    file.Name,
                //    file.Directory.Name,
                //    file.Extension,
                //    DateTime.Parse(
                //            file.LastWriteTime.ToString()
                //            ).ToString("yyyy-MM-dd")
                //    );
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.GetSelected();
        }
        private void GetSelected()
        {
            if (dgvPcosting.SelectedCells.Count > 0)
            {
                frm = new frmStatus();
                frm.Show();
                frm.AddMessage("Initializing...");

                var count = this.dgvPcosting.Columns.Count;
                int selectedrowindex = dgvPcosting.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dgvPcosting.Rows[selectedrowindex];

                path = selectedRow.Cells["file_path"].Value.ToString();
                reference = selectedRow.Cells["file_reference"].Value.ToString();
                originalFoldername = selectedRow.Cells["foldername"].Value.ToString();
                TransferFileToDecrypt(path + @"\");
                frm.ExitStatus();
                this.Close();
            }
        }

        private void TransferFileToDecrypt(string source)
        {

            string path = source;
            DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.*"); //Getting Text files
            //FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
            string str = "";
            foreach (FileInfo file in Files)
            {
                //MessageBox.Show(file.FullName);
                frm.AddMessage("Decripting " + file.Name);
                Decrypt(file.FullName);
            }
        }

        private void Decrypt(string filePath)
        {
            string password = "ThePasswordToDecryptAndEncryptTheFile";

            string fileDestination = Path.GetDirectoryName(Application.ExecutablePath) + @"\Queue\" + (Path.GetFileName(filePath)).Remove((Path.GetFileName(filePath).Length - 4));

            // For additional security Pin the password of your files
            GCHandle gch = GCHandle.Alloc(password, GCHandleType.Pinned);

            // Decrypt the file
            FileDecrypt(filePath, fileDestination, password);

            // To increase the security of the decryption, delete the used password from the memory !
            ZeroMemory(gch.AddrOfPinnedObject(), password.Length * 2);
            gch.Free();

            // You can verify it by displaying its value later on the console (the password won't appear)
            Console.WriteLine("The given password is surely nothing: " + password);

        }



        private void FileDecrypt(string inputFile, string outputFile, string password)
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] salt = new byte[32];

            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);
            fsCrypt.Read(salt, 0, salt.Length);

            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            //AES.Padding = PaddingMode.PKCS7;
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.CFB;

            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);



            cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);

            FileStream fsOut = new FileStream(outputFile, FileMode.Create);



            int read;
            byte[] buffer = new byte[1048576];

            bool success = false;

            try
            {
                while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Application.DoEvents();
                    fsOut.Write(buffer, 0, read);
                    cs.FlushFinalBlock();
                }
                success = true;


            }
            catch (CryptographicException ex_CryptographicException)
            {
                //Console.WriteLine("CryptographicException error: " + ex_CryptographicException.Message);
                frm.AddMessage("Error - " + "CryptographicException error: " + ex_CryptographicException.Message);
                success = false;
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                // frm.AddMessage("Error: " + ex.Message);
                success = false;
            }

            try
            {
                cs.Close();
                AES.Clear();
                success = true;
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error by closing CryptoStream: " + ex.Message);
                frm.AddMessage("Error by closing CryptoStream: " + ex.Message);
                success = false;
            }
            finally
            {
                fsOut.Close();
                fsCrypt.Close();
            }



            if (success)
            {
                string exePath = Path.GetDirectoryName(Application.ExecutablePath);
                string reportFullPath = Path.Combine(exePath + @"\Processed\" + originalFoldername + @"\");
                if (!Directory.Exists(reportFullPath))
                {
                    System.IO.Directory.CreateDirectory(reportFullPath);
                }


                //Moving Original File
                string originalFile = inputFile;
                string originalPathDestination = reportFullPath + Path.GetFileName(originalFile);

                if (File.Exists(originalPathDestination))
                {
                    try
                    {
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        File.Delete(originalPathDestination);
                    }
                    catch (Exception ex) { }
                }
                try
                {
                    success = MoveOriginalFiles(originalFile, originalPathDestination);
                    frm.AddMessage("Successfully decripted " + Path.GetFileName(originalFile));
                }
                catch (Exception ex) { }
            }





        }
        private bool MoveOriginalFiles(string originalFile, string originalPathDestination)
        {
            bool success = false;
            try
            {
                File.Copy(originalFile, originalPathDestination);
                success = true;

            }
            catch (IOException ex)
            {
                frm.AddMessage("Error : " + ex.Message);
                // MessageBox.Show(ex.Message); // Write error
            }

            if (File.Exists(originalFile))
            {
                Console.WriteLine("The original file still exists, which is unexpected.");
            }
            else
            {
                Console.WriteLine("The original file no longer exists, which is expected.");
            }

            return success;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
