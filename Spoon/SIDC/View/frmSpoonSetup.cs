using System;
using System.Configuration;
using System.Windows.Forms;

namespace Spoon.SIDC.View
{
    public partial class frmSpoonSetup : Form
    {
        public frmSpoonSetup()
        {
            InitializeComponent();
        }

        private void frmSpoonSetup_Load(object sender, EventArgs e)
        {
            this.LoadSettings();
        }

        private void LoadSettings()
        {
            txtPOSHost.Text = ConfigurationManager.ConnectionStrings["DBHost"].ToString();
            txtPOSUser.Text = ConfigurationManager.ConnectionStrings["DBUser"].ToString();
            lblPOSPassword.Text = ConfigurationManager.ConnectionStrings["DBPassword"].ToString();
            txtPOSSchema.Text = ConfigurationManager.ConnectionStrings["DBName"].ToString();

            txtSAPHost.Text = ConfigurationManager.ConnectionStrings["SAPDBHost"].ToString();
            txtSAPUser.Text = ConfigurationManager.ConnectionStrings["SAPDBUser"].ToString();
            lblSAPPassword.Text = ConfigurationManager.ConnectionStrings["SAPPassword"].ToString();
            txtSAPSchema.Text = ConfigurationManager.ConnectionStrings["SAPDBName"].ToString();


            txtBranchName.Text = ConfigurationManager.AppSettings["BranchName"].ToString();
            txtCsvPathSource.Text = ConfigurationManager.AppSettings["CSVPathSource"].ToString();

            txtFlashDriveFormat.Text = ConfigurationManager.AppSettings["FilesHerePathFormat"].ToString();

            bool initSpoon = false;
            try
            {
                bool.TryParse(ConfigurationManager.AppSettings["Init"].ToString(), out initSpoon);
            }
            catch
            {
                initSpoon = false;
            }
            chkInitializeMode.Checked = initSpoon;

            bool SAPasDefaultConnection = false;
            try
            {
                bool.TryParse(ConfigurationManager.AppSettings["SAPasDefaultConnection"].ToString(), out SAPasDefaultConnection);
            }
            catch
            {
                SAPasDefaultConnection = false;
            }
            chkSapAsDefaultConnection.Checked = SAPasDefaultConnection;


            bool UpdateCostInRecording = false;
            try
            {
                bool.TryParse(ConfigurationManager.AppSettings["UpdateCostInRecording"].ToString(), out UpdateCostInRecording);
            }
            catch
            {
                UpdateCostInRecording = false;
            }
            chkUpdateCost.Checked = UpdateCostInRecording;

        }
        private void EncryptText(string server, string passwordtoEncrypt)
        {
            if (string.IsNullOrWhiteSpace(passwordtoEncrypt))
                return;

            ACryptoServiceProvider crypt = new ACryptoServiceProvider();
            string password = crypt.Encrypt(passwordtoEncrypt, "*sup3r5dm1n*"); ;

            if (server == "pos")
            {
                txtPOSPassword.Text = "";
                lblPOSPassword.Text = password;
            }
            else if (server == "sap")
            {
                txtSAPPassword.Text = "";
                lblSAPPassword.Text = password;
            }


        }
        private void Save()
        {
            string logs = "";
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                //POS
                config.ConnectionStrings.ConnectionStrings.Remove(new ConnectionStringSettings("DBHost", txtPOSHost.Text));
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("DBHost", txtPOSHost.Text));

                config.ConnectionStrings.ConnectionStrings.Remove(new ConnectionStringSettings("DBUser", txtPOSUser.Text));
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("DBUser", txtPOSUser.Text));

                config.ConnectionStrings.ConnectionStrings.Remove(new ConnectionStringSettings("DBName", txtPOSSchema.Text));
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("DBName", txtPOSSchema.Text));

                config.ConnectionStrings.ConnectionStrings.Remove(new ConnectionStringSettings("DBPassword", lblPOSPassword.Text));
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("DBPassword", lblPOSPassword.Text));

                //SAP
                config.ConnectionStrings.ConnectionStrings.Remove(new ConnectionStringSettings("SAPDBHost", txtSAPHost.Text));
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("SAPDBHost", txtSAPHost.Text));

                config.ConnectionStrings.ConnectionStrings.Remove(new ConnectionStringSettings("SAPDBUser", txtSAPUser.Text));
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("SAPDBUser", txtSAPUser.Text));

                config.ConnectionStrings.ConnectionStrings.Remove(new ConnectionStringSettings("SAPDBName", txtSAPSchema.Text));
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("SAPDBName", txtSAPSchema.Text));

                config.ConnectionStrings.ConnectionStrings.Remove(new ConnectionStringSettings("SAPPassword", lblSAPPassword.Text));
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("SAPPassword", lblSAPPassword.Text));

                //
                config.AppSettings.Settings.Remove("Init");
                config.AppSettings.Settings.Add("Init", chkInitializeMode.Checked.ToString());

                config.AppSettings.Settings.Remove("SAPasDefaultConnection");
                config.AppSettings.Settings.Add("SAPasDefaultConnection", chkSapAsDefaultConnection.Checked.ToString());

                config.AppSettings.Settings.Remove("UpdateCostInRecording");
                config.AppSettings.Settings.Add("UpdateCostInRecording", chkUpdateCost.Checked.ToString());

                config.AppSettings.Settings.Remove("FilesHerePathFormat");
                config.AppSettings.Settings.Add("FilesHerePathFormat", txtFlashDriveFormat.Text);

                config.AppSettings.Settings.Remove("CSVPathSource");
                config.AppSettings.Settings.Add("CSVPathSource", txtCsvPathSource.Text);

                config.AppSettings.Settings.Remove("BranchName");
                config.AppSettings.Settings.Add("BranchName", txtBranchName.Text);

                config.Save();
                //config.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("connectionStrings");
                ConfigurationManager.RefreshSection("appSettings");


                // this.Close();
                this.LoadSettings();
                MessageBox.Show("Saved");


            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error : {0} ", ex.Message));
            }
            finally
            {

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnPOSEnrypt_Click(object sender, EventArgs e)
        {
            this.EncryptText("pos", txtPOSPassword.Text);
        }

        private void btnEncryptSAPPassword_Click(object sender, EventArgs e)
        {
            this.EncryptText("sap", txtSAPPassword.Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save();

        }
    }
}
