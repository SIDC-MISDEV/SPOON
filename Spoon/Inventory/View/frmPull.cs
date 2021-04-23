using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace Spoon.Inventory.View
{
    public partial class frmPull : Form
    {
        public frmPull()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            // Extract the argument.
            int arg = (int)e.Argument;

            // Start the time-consuming operation.
            e.Result = TimeConsumingOperation(bw, arg);

            // If the operation was canceled by the user, 
            // set the DoWorkEventArgs.Cancel property to true.
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        // This event handler demonstrates how to interpret 
        // the outcome of the asynchronous operation implemented
        // in the DoWork event handler.
        private void backgroundWorker1_RunWorkerCompleted(
            object sender,
            RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                // The user canceled the operation.
                MessageBox.Show("Operation was canceled");
            }
            else if (e.Error != null)
            {
                // There was an error during the operation.
                string msg = String.Format("An error occurred: {0}", e.Error.Message);
                MessageBox.Show(msg);
            }
            else
            {
                // The operation completed normally.
                string msg = String.Format("Result = {0}", e.Result);
                MessageBox.Show(msg);
            }
        }

        private int TimeConsumingOperation(
            BackgroundWorker bw,
            int sleepPeriod)
        {
            int result = 0;

            Random rand = new Random();

            while (!bw.CancellationPending)
            {
                bool exit = false;

                switch (rand.Next(3))
                {
                    // Raise an exception.
                    case 0:
                        {
                            throw new Exception("An error condition occurred.");
                            break;
                        }

                    // Sleep for the number of milliseconds
                    // specified by the sleepPeriod parameter.
                    case 1:
                        {
                            Thread.Sleep(sleepPeriod);
                            break;
                        }

                    // Exit and return normally.
                    case 2:
                        {
                            result = 23;
                            exit = true;
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }

                if (exit)
                {
                    break;
                }
            }

            return result;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.RunWorkerAsync(2000);
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync();
        }

    }
}
