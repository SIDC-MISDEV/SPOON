using System;
using System.Drawing;
using System.Windows.Forms;

namespace Spoon.Admin.View
{
    public partial class frmStatus : Form
    {
        public frmStatus()
        {
            InitializeComponent();
            Rectangle r = Screen.PrimaryScreen.WorkingArea;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
        }

        private void frmStatus_Load(object sender, EventArgs e)
        {

        }

        public void ExitStatus()
        {
            this.AddMessage("Will be close in 5 seconds.");
            timer1.Interval = 5000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
            btnCancel.Visible = true;
        }

        public void AddMessage(string v)
        {
            lblMessage.Text += v + "\n";
            //Thread.Sleep(1000);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
