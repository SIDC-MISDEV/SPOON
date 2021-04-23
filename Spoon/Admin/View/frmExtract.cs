using MetroFramework;
using Spoon.Inventory.Service;
using Spoon.SIDC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Spoon.Admin.View
{
    public partial class frmExtract : Form
    {
        [DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(IntPtr Destination, int Length);


        string _reference;
        string _warehouse;
        string _path;
        frmStatus frm;
        ATransaction atransaction;
        public frmExtract()
        {
            InitializeComponent();
            atransaction = new ATransaction();
            //this._warehouse = atransaction.GetWareHouseCode();
            this._warehouse = ConfigurationManager.AppSettings["BranchName"].ToString();
            bool defaultSAP = false;
            try
            {
                bool.TryParse(ConfigurationManager.AppSettings["SAPasDefaultConnection"].ToString(), out defaultSAP);
            }
            catch
            {
                defaultSAP = false;
            }

            this.chkSapConnection.Checked = defaultSAP;
        }
        public static byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    // Fille the buffer with the generated data
                    rng.GetBytes(data);
                }
            }

            return data;

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
        private void btnSave_Click(object sender, EventArgs e)
        {

            this.ExtractTransaction();
        }

        private void ExtractTransaction()
        {
            if (dgvforPurchaseOrder.Rows.Count > 0)
            {
                this._reference = txtReference.Text;

                if (!string.IsNullOrWhiteSpace(this._warehouse) && !string.IsNullOrWhiteSpace(this._reference))
                {
                    frm = new frmStatus();
                    frm.Show();
                    frm.AddMessage("Initializing...");
                    if (ConfigurationManager.AppSettings["UseGoogleDrive"].ToString() == "false")
                        _path = this.SetFlashDrivePath();
                    else
                        _path = ConfigurationManager.AppSettings["CSVPathSource"].ToString();

                    if (!string.IsNullOrWhiteSpace(_path))
                    {
                        this.Extract();
                        MessageBox.Show("Extraction completed.");
                        Application.Exit();
                    }
                    else
                        frm.AddMessage("Invalid path, extraction failed.");
                }
                else
                {

                    DisplayMessage("Invalid warehouse or reference", false, false);
                }
            }
        }
        private void DisplayMessage(string message, bool retry, bool showMessageBoxIfSuccess = true)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                if (showMessageBoxIfSuccess)
                {
                    MetroMessageBox.Show(this, "Transaction Saved.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                MetroMessageBox.Show(this, message, "Error encountered!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void Extract()
        {

            //string extractPath = @"C:\ionic\new";
            string extractPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\extracted";
            //string extractPath = ConfigurationManager.AppSettings["CSVPath"].ToString();
            string folderName = @"\" + this._warehouse + "_" + this._reference;
            string fullPath = Path.Combine(extractPath + folderName);

            if (Directory.Exists(fullPath))
            {
                frm.AddMessage(folderName + " already exist, starting to trash...");
                Directory.Delete(fullPath, true);
            }
            frm.AddMessage("Extraction path of csv files is missing.");
            System.IO.Directory.CreateDirectory(fullPath);
            frm.AddMessage("Extraction path created.");

            using (var wc = new WaitCursor())
            {
                //AExtract extract = new AExtract();
                frm.AddMessage("Extraction started.");
                //extract.Extract(this._warehouse, this._reference, extractPath);
                this.ExtractCSV(this._warehouse, this._reference, fullPath);

                frm.AddMessage("Extraction completed.");
            }
            frm.AddMessage("Transfering files to target path.");

            using (var wc = new WaitCursor())
            {
                //for trycatch ConfigurationManager.AppSettings["FilesHerePathFormat"].ToString()
                string fileHereFormat = ConfigurationManager.AppSettings["FilesHerePathFormat"].ToString();
                fileHereFormat = string.IsNullOrWhiteSpace(fileHereFormat) ? @"\Files\Here\Spoon" : fileHereFormat;
                this.MoveFolder(fullPath, Path.Combine(_path + fileHereFormat + folderName));
                //this.MoveFolder(fullPath, Path.Combine(_path + @"\Files\Here\Spoon" + folderName));
            }

        }

        private void ExtractCSV(string _warehouse, string _reference, string extractPath)
        {
            string csvST = Path.Combine(extractPath + @"\T-" + _reference + ".csv");
            string csvSTMasterData = Path.Combine(extractPath + @"\M-" + _reference + ".csv");
            string csvSTPcosting = Path.Combine(extractPath + @"\P-" + _reference + ".csv");
            this.writeCSV(dgvforPurchaseOrder, csvST);
            this.writeCSV(dgvItemMasterData, csvSTMasterData);
            this.writeCSV(dgvPcosting, csvSTPcosting);
        }
        public void writeCSV(DataGridView gridIn, string outputFile)
        {
            //test to see if the DataGridView has any rows
            if (gridIn.RowCount > 0)
            {
                string value = "";
                DataGridViewRow dr = new DataGridViewRow();
                StreamWriter swOut = new StreamWriter(outputFile);

                //write header rows to csv
                //for (int i = 0; i <= gridIn.Columns.Count - 1; i++)
                //{
                //    if (i > 0)
                //    {
                //        swOut.Write(",");
                //    }
                //    swOut.Write(gridIn.Columns[i].HeaderText);
                //}

                //swOut.WriteLine();

                //write DataGridView rows to csv
                for (int j = 0; j <= gridIn.Rows.Count - 1; j++)
                {
                    if (j > 0)
                    {
                        swOut.WriteLine();
                    }

                    dr = gridIn.Rows[j];

                    for (int i = 0; i <= gridIn.Columns.Count - 1; i++)
                    {
                        if (i > 0)
                        {
                            swOut.Write("|");
                        }

                        value = dr.Cells[i].FormattedValue.ToString();
                        //replace comma's with spaces
                        value = value.Replace('|', ' ');
                        //replace embedded newlines with spaces
                        value = value.Replace(Environment.NewLine, " ");

                        swOut.Write(value);
                    }
                }
                swOut.Close();
            }
        }

        private string SetFlashDrivePath()
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                frm.AddMessage("Select path to store extracted data.");
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    frm.AddMessage(folderDialog.SelectedPath + " selected path.");
                    return folderDialog.SelectedPath;
                }
                return null;
            }
        }


        public void MoveFolder(string folderToMove, string destination)
        {
            try
            {

                if (Directory.Exists(destination))
                {
                    frm.AddMessage("File already exist, trying to delete.");
                    Directory.Delete(destination, true);
                }

                System.IO.Directory.CreateDirectory(destination);
                frm.AddMessage("Destination path created.");


                var stack = new Stack<Folders>();
                stack.Push(new Folders(folderToMove, destination));
                frm.AddMessage("Transferring file...");
                while (stack.Count > 0)
                {
                    var folders = stack.Pop();
                    Directory.CreateDirectory(folders.Target);
                    foreach (var file in Directory.GetFiles(folders.Source, "*.*"))
                    {
                        if (File.Exists(Path.Combine(folders.Target, Path.GetFileName(file))))
                        {
                            frm.AddMessage("File already exist, trying to delete.");
                            File.Delete(Path.Combine(folders.Target, Path.GetFileName(file)));
                        }
                        Encrypt(file, Path.Combine(folders.Target));
                        //File.Copy(file, Path.Combine(folders.Target, Path.GetFileName(file)));
                        frm.AddMessage(string.Format("{0} file transfered", Path.GetFileName(file)));
                    }

                    foreach (var folder in Directory.GetDirectories(folders.Source))
                    {
                        stack.Push(new Folders(folder, Path.Combine(folders.Target, Path.GetFileName(folder))));
                    }
                }

                frm.AddMessage("Successfully transfered.");
                frm.ExitStatus();

            }
            catch (Exception ex)
            {
                frm.AddMessage("Error encountered : " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Encription

        private void Encrypt(string filePath, string destination)
        {
            string password = "ThePasswordToDecryptAndEncryptTheFile";

            // For additional security Pin the password of your files
            GCHandle gch = GCHandle.Alloc(password, GCHandleType.Pinned);

            // Encrypt the file
            FileEncrypt(filePath, destination, password);
            //FileEncrypt(@"F:\encryption\ToEncrypt\ar\ARTalaibon20171111043337.csv", password);

            // To increase the security of the encryption, delete the given password from the memory !
            ZeroMemory(gch.AddrOfPinnedObject(), password.Length * 2);
            gch.Free();

            // You can verify it by displaying its value later on the console (the password won't appear)
            Console.WriteLine("The given password is surely nothing: " + password);
        }

        private void FileEncrypt(string inputFile, string destination, string password)
        {
            //http://stackoverflow.com/questions/27645527/aes-encryption-on-large-files

            //generate random salt
            byte[] salt = GenerateRandomSalt();

            //create output file name
            FileStream fsCrypt = new FileStream(inputFile + ".aes", FileMode.Create);

            //convert password string to byte arrray
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            //Set Rijndael symmetric encryption algorithm
            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Padding = PaddingMode.PKCS7;

            //http://stackoverflow.com/questions/2659214/why-do-i-need-to-use-the-rfc2898derivebytes-class-in-net-instead-of-directly
            //"What it does is repeatedly hash the user password along with the salt." High iteration counts.
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);

            //Cipher modes: http://security.stackexchange.com/questions/52665/which-is-the-best-cipher-mode-and-padding-mode-for-aes-encryption
            AES.Mode = CipherMode.CFB;

            // write salt to the begining of the output file, so in this case can be random every time
            fsCrypt.Write(salt, 0, salt.Length);

            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);

            FileStream fsIn = new FileStream(inputFile, FileMode.Open);

            //create a buffer (1mb) so only this amount will allocate in the memory and not the whole file
            byte[] buffer = new byte[1048576];
            int read;

            bool success = false;
            try
            {
                while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Application.DoEvents(); // -> for responsive GUI, using Task will be better!
                    cs.Write(buffer, 0, read);
                }

                // Close up
                fsIn.Close();
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cs.Close();
                fsCrypt.Close();
            }


            if (success)
            {
                ////Moving Original File
                //string originalFile = inputFile;
                //string originalPathDestination = @"F:\encryption\original\ar\" + Path.GetFileName(originalFile);

                //if (File.Exists(originalPathDestination))
                //    File.Delete(originalPathDestination);


                //success = MoveOriginalFiles(originalFile, originalPathDestination);


                //Moving Encrypted File
                string encryptedFile = inputFile + ".aes";
                string encryptedFilePathDestination = destination + @"\" + Path.GetFileName(encryptedFile);

                if (success)
                    MoveEncryptedFiles(encryptedFile, encryptedFilePathDestination);

            }

        }

        private void MoveEncryptedFiles(string encryptedFile, string encryptedFilePathDestination)
        {
            try
            {
                File.Move(encryptedFile, encryptedFilePathDestination);

            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message); // Write error
            }

            if (File.Exists(encryptedFile))
            {
                Console.WriteLine("The original file still exists, which is unexpected.");
            }
            else
            {
                Console.WriteLine("The original file no longer exists, which is expected.");
            }
        }

        private bool MoveOriginalFiles(string originalFile, string originalPathDestination)
        {
            bool success = false;
            try
            {
                File.Move(originalFile, originalPathDestination);
                success = true;

            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message); // Write error
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


        #endregion

        private void txtReference_Leave(object sender, EventArgs e)
        {
            this.GetTransaction(txtReference.Text);
        }

        private void GetTransaction(string reference)
        {
            try
            {
                if (reference.Length >= 3)
                {
                    using (var wc = new WaitCursor())
                    {
                        this.SetTransaction(reference, atransaction);
                        this.SetItemMaster(reference, atransaction);
                        this.SetItemPcosting(reference, atransaction);
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetTransaction(string reference, ATransaction atransaction)
        {
            dgvforPurchaseOrder.AutoGenerateColumns = false;

            foreach (DataGridViewColumn col in dgvforPurchaseOrder.Columns)
            {
                col.DataPropertyName = col.Name;

            }
            dgvforPurchaseOrder.Refresh();
            var data = new DataTable();

            if (chkSapConnection.Checked)
                data = atransaction.GetTransaction(reference, chkSapConnection.Checked, "ST");
            else
                data = atransaction.GetTransaction(reference, false);

            txtName.Text = "";
            txtCode.Text = "";
            txtReference.Text = "";
            dtpDate.Value = DateTime.Now;
            btnSave.Visible = false;

            if (data.Rows.Count > 0)
            {
                txtName.Text = data.Rows[0]["branch_name"].ToString();
                txtCode.Text = data.Rows[0]["warehouse_code"].ToString();
                txtReference.Text = data.Rows[0]["reference"].ToString();
                dtpDate.Value = DateTime.Parse(data.Rows[0]["date_posted"].ToString());
                btnSave.Visible = true;
                btnSave.Enabled = btnSave.Visible;
            }

            dgvforPurchaseOrder.DataSource = data;

        }


        private void SetItemMaster(string reference, ATransaction atransaction)
        {
            // dgvItemMasterData.AutoGenerateColumns = false;

            foreach (DataGridViewColumn col in dgvItemMasterData.Columns)
            {
                col.DataPropertyName = col.Name;

            }
            dgvItemMasterData.Refresh();

            var data = new DataTable();

            if (chkSapConnection.Checked)
                data = atransaction.GetItemMaster(reference, chkSapConnection.Checked, "ST");
            else
                data = atransaction.GetItemMaster(reference, false);

            dgvItemMasterData.DataSource = data;
        }

        private void SetItemPcosting(string reference, ATransaction atransaction)
        {
            dgvPcosting.AutoGenerateColumns = false;

            foreach (DataGridViewColumn col in dgvPcosting.Columns)
            {
                col.DataPropertyName = col.Name;

            }
            dgvPcosting.Refresh();

            var data = new DataTable();

            if (chkSapConnection.Checked)
                data = atransaction.GetItemPcosting(reference, chkSapConnection.Checked, "ST");
            else
                data = atransaction.GetItemPcosting(reference, false);

            dgvPcosting.DataSource = data;
        }

        private void dgvforPurchaseOrder_DataSourceChanged(object sender, EventArgs e)
        {
            this.ComputeTotal();
        }
        public void ComputeTotal()
        {
            lblTotal.Text = dgvforPurchaseOrder.Rows.Cast<DataGridViewRow>()
                                  .AsEnumerable()
                                  .Sum(x => decimal.Parse(x.Cells["item_total"].FormattedValue.ToString()))
                                  .ToString("N", CultureInfo.InvariantCulture);
        }

        private void dgvforPurchaseOrder_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // http://www.devcomponents.com/kb2/?p=1230
            DataGridView dg = (DataGridView)sender;
            // Current row record
            string rowNumber = (e.RowIndex + 1).ToString();

            // Format row based on number of records displayed by using leading zeros
            while (rowNumber.Length < dg.RowCount.ToString().Length) rowNumber = "0" + rowNumber;

            // Position text
            SizeF size = e.Graphics.MeasureString(rowNumber, this.Font);
            if (dg.RowHeadersWidth < (int)(size.Width + 20)) dg.RowHeadersWidth = (int)(size.Width + 20);

            // Use default system text brush
            Brush b = SystemBrushes.ControlText;

            // Draw row number
            e.Graphics.DrawString(rowNumber, dg.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));

        }

        private void dgvItemMasterData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // http://www.devcomponents.com/kb2/?p=1230
            DataGridView dg = (DataGridView)sender;
            // Current row record
            string rowNumber = (e.RowIndex + 1).ToString();

            // Format row based on number of records displayed by using leading zeros
            while (rowNumber.Length < dg.RowCount.ToString().Length) rowNumber = "0" + rowNumber;

            // Position text
            SizeF size = e.Graphics.MeasureString(rowNumber, this.Font);
            if (dg.RowHeadersWidth < (int)(size.Width + 20)) dg.RowHeadersWidth = (int)(size.Width + 20);

            // Use default system text brush
            Brush b = SystemBrushes.ControlText;

            // Draw row number
            e.Graphics.DrawString(rowNumber, dg.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));

        }

        private void dgvPcosting_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // http://www.devcomponents.com/kb2/?p=1230
            DataGridView dg = (DataGridView)sender;
            // Current row record
            string rowNumber = (e.RowIndex + 1).ToString();

            // Format row based on number of records displayed by using leading zeros
            while (rowNumber.Length < dg.RowCount.ToString().Length) rowNumber = "0" + rowNumber;

            // Position text
            SizeF size = e.Graphics.MeasureString(rowNumber, this.Font);
            if (dg.RowHeadersWidth < (int)(size.Width + 20)) dg.RowHeadersWidth = (int)(size.Width + 20);

            // Use default system text brush
            Brush b = SystemBrushes.ControlText;

            // Draw row number
            e.Graphics.DrawString(rowNumber, dg.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));

        }

        private void txtReference_LocationChanged(object sender, EventArgs e)
        {

        }

        private void frmExtract_Load(object sender, EventArgs e)
        {

        }
    }

    public class Folders
    {
        public string Source { get; private set; }
        public string Target { get; private set; }

        public Folders(string source, string target)
        {
            Source = source;
            Target = target;
        }
    }

}

