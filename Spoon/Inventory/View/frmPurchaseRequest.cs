using MetroFramework;
using Spoon.Inventory.Controller;
using Spoon.Inventory.Interface;
using Spoon.Inventory.Model;
using Spoon.SIDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Spoon.Inventory.View
{
    public partial class frmTransact : Form, ITransferView
    {
        TransferController _controller;
        Document _document;
        List<DocumentLines> _listtransactiondetails = null;
        DocumentLines _transactiondetails = null;
        List<Units> _listUnits = null;
        List<Items> _listItems = null;
        Items _items = null;
        Units _units = null;

        Form _form;
        string _path = "";
        string _sourcePath = "";
        string _reference = "";
        frmDocumentBrowser _browser;
        #region Fields
        string _notification = null;
        int _rowIndex = -1;
        List<int> _deletedTransaction = null;


        #endregion

        public frmTransact()
        {
            InitializeComponent();
            _form = this;

            this._controller = new TransferController(this);
            this._document = new Document();
            //this._form = form;

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
        private void EnableGridView(bool v)
        {
            dgvTransactions.AllowUserToAddRows = v;
            //dgvItem.AllowUserToDeleteRows = v;
            // dgvItem.ReadOnly = !v;
        }

        public void ComputeTotal()
        {
            lblTotal.Text = dgvTransactions.Rows.Cast<DataGridViewRow>()
                                  .AsEnumerable()
                                  .Sum(x => decimal.Parse(x.Cells["item_total"].FormattedValue.ToString()))
                                  .ToString("N", CultureInfo.InvariantCulture);
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTransact_Load(object sender, EventArgs e)
        {
            this.LoadFile();
            // this.dgvTransactions.Columns["item_cost"].DefaultCellStyle.Format = "0.00##";
            // this.dgvTransactions.Columns["item_price"].DefaultCellStyle.Format = "0.00##";
            // this.dgvTransactions.Columns["item_cost"].ValueType = GetType(decimal);
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            bool verified = false;
            verified = chkVerifyItemUnit.Checked && chkVerifyItemMasterData.Checked && chkVerifyTransaction.Checked;

            if (verified)
                this.RunCommand();
            else
            {
                string errorMessage = chkVerifyTransaction.Checked ? "" : "Please verify transaction by clicking '" + chkVerifyTransaction.Text + " in tab " + tabPage1.Text + ".\n";
                errorMessage += chkVerifyItemMasterData.Checked ? "" : "Please verify item master data by clicking '" + chkVerifyItemMasterData.Text + " in tab " + tabPage1.Text + ".\n";
                errorMessage += chkVerifyItemUnit.Checked ? "" : "Please verify item unit by clicking '" + chkVerifyItemUnit.Text + " in tab " + tabPage1.Text + ".\n";
                MetroMessageBox.Show(_form, errorMessage, "Verication failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void RunCommand()
        {
            DisplayMessage(this._controller.Save(), false, true);
        }

        private void DisplayMessage(string message, bool retry, bool showMessageBoxIfSuccess = true)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                if (showMessageBoxIfSuccess)
                {
                    MetroMessageBox.Show(_form, "Transaction Saved.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this._document = new Document();
                    this.RemoveFiles();
                    this.Close();
                }
            }
            else
            {
                if (retry)
                {
                    DialogResult dialogResult = MetroMessageBox.Show(_form, message + "\nContinue/Retry to save transaction?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                        this.RunCommand();
                }
                else
                    MetroMessageBox.Show(_form, message, "Error encountered!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void RemoveFiles()
        {
            if (!string.IsNullOrWhiteSpace(this._sourcePath))
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();

                DirectoryInfo sourcedir = new DirectoryInfo(this._sourcePath);
                sourcedir.Delete(true);
            }
            this.RemoveFilesInQueued();
        }

        private void RemoveFilesInQueued()
        {

            try
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();

                DirectoryInfo dir = new DirectoryInfo(Path.GetDirectoryName(Application.ExecutablePath) + @"\Queue\");
                foreach (FileInfo fi in dir.GetFiles())
                {
                    fi.IsReadOnly = false;
                    fi.Delete();
                }
            }
            catch (Exception ex)
            {

            }
        }




        #region Implementation

        public Document document
        {
            get
            {
                this._document.PostedDate = this.dtpDate.Value;
                this._document.Code = txtCode.Text.ToUpper();
                this._document.Total = DataConversion.ToDecimal(lblTotal.Text);
                this._document.CrossReference = txtCross.Text.ToUpper();
                this.TransferTransactionDetailsFromGridToList();
                this._document.DocumentLine = this._listtransactiondetails;
                return this._document;
            }
            set
            {
                //throw new NotImplementedException();
            }
        }

        public List<Items> _item
        {
            get
            {
                this.TransferItemMasterDataFromGridToList();
                return this._listItems;
            }
        }

        public List<Units> _unit
        {
            get
            {
                this.TransferItemsUnitFromGridToList();
                return this._listUnits;
            }
        }

        public void SetController(TransferController controller)
        {
            this._controller = controller;
        }


        #endregion

        #region Transfer from DGV to List

        private void TransferTransactionDetailsFromGridToList()
        {
            this._listtransactiondetails = new List<DocumentLines>();
            dgvTransactions.EndEdit();

            foreach (DataGridViewRow row in dgvTransactions.Rows)
            {
                //transaction = null;
                this._transactiondetails = new DocumentLines();

                if (!string.IsNullOrWhiteSpace(row.Cells["item_code"].FormattedValue.ToString())
                    && !string.IsNullOrWhiteSpace(row.Cells["item_unit"].FormattedValue.ToString())
                   )
                {

                    this._transactiondetails.Item.ItemCode = row.Cells["item_code"].FormattedValue.ToString();
                    this._transactiondetails.Item.Description = row.Cells["item_description"].FormattedValue.ToString();
                    this._transactiondetails.Item.Unit = row.Cells["item_unit"].FormattedValue.ToString();
                    this._transactiondetails.Item.Quantity = DataConversion.ToDecimal(row.Cells["item_qty"].FormattedValue.ToString());
                    this._transactiondetails.Item.Price = DataConversion.ToDecimal(row.Cells["item_price"].FormattedValue.ToString());
                    this._transactiondetails.Total = DataConversion.ToDecimal(row.Cells["item_total"].FormattedValue.ToString());
                    this._transactiondetails.Item.Conversion = DataConversion.ToDecimal(row.Cells["item_conversion"].FormattedValue.ToString());


                    _listtransactiondetails.Add(this._transactiondetails);


                }

            }



        }


        private void TransferItemMasterDataFromGridToList()
        {
            this._listItems = new List<Items>();
            dgvItemMasterData.EndEdit();

            foreach (DataGridViewRow row in dgvItemMasterData.Rows)
            {
                //transaction = null;
                this._items = new Items();

                if (!string.IsNullOrWhiteSpace(row.Cells["master_idstock"].FormattedValue.ToString())
                    && !string.IsNullOrWhiteSpace(row.Cells["master_name"].FormattedValue.ToString())
                   )
                {

                    this._items.ItemCode = row.Cells["master_idstock"].FormattedValue.ToString();
                    this._items.Description = row.Cells["master_name"].FormattedValue.ToString();
                    this._items.s1Category = row.Cells["master_type"].FormattedValue.ToString();
                    this._items.s1Supplier = row.Cells["master_supplier"].FormattedValue.ToString();
                    this._items.Status = row.Cells["master_status"].FormattedValue.ToString();
                    this._items.Remarks = row.Cells["master_remarks"].FormattedValue.ToString();
                    this._items.Barcode = row.Cells["master_barcode"].FormattedValue.ToString();
                    this._items.Discountinued = false;
                    if (row.Cells["master_discontinued"].FormattedValue.ToString() == "1")
                        this._items.Discountinued = true;

                    this._items.Consignment = false;
                    if (row.Cells["master_consignment"].FormattedValue.ToString() == "1")
                        this._items.Consignment = true;

                    this._items.Taxable = false;
                    if (row.Cells["master_taxable"].FormattedValue.ToString() == "1")
                        this._items.Taxable = true;

                    this._items.s1CategoryID = row.Cells["master_CatID"].FormattedValue.ToString();
                    this._items.Packaging = row.Cells["master_packaging"].FormattedValue.ToString();
                    this._items.s1SubCategory = row.Cells["master_subcategory"].FormattedValue.ToString();
                    this._items.s1SubCategoryID = row.Cells["master_subcatid"].FormattedValue.ToString();
                    this._items.s1Brand = row.Cells["master_brand"].FormattedValue.ToString();
                    this._items.s1BrandID = row.Cells["master_brandid"].FormattedValue.ToString();


                    this._listItems.Add(this._items);


                }

            }



        }

        private void TransferItemsUnitFromGridToList()
        {
            this._listUnits = new List<Units>();
            dgvPcosting.EndEdit();

            foreach (DataGridViewRow row in dgvPcosting.Rows)
            {
                //transaction = null;
                this._units = new Units();

                if (!string.IsNullOrWhiteSpace(row.Cells["p_idstock"].FormattedValue.ToString())
                    && !string.IsNullOrWhiteSpace(row.Cells["p_unit"].FormattedValue.ToString())
                   )
                {

                    this._units.ItemCode = row.Cells["p_idstock"].FormattedValue.ToString();
                    this._units.Barcode = row.Cells["p_barcode"].FormattedValue.ToString();
                    this._units.Unit = row.Cells["p_unit"].FormattedValue.ToString();
                    this._units.Conversion = DataConversion.ToDecimal(row.Cells["p_conversion"].FormattedValue.ToString());
                    this._units.CostingMethod = row.Cells["p_costingMethod"].FormattedValue.ToString();


                    this._listUnits.Add(this._units);


                }

            }



        }

        #endregion



        #region DGV
        private void dgvItem_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void dgvItem_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dgvTransactions.CurrentCell.ColumnIndex == 4 || dgvTransactions.CurrentCell.ColumnIndex == 5
                || dgvTransactions.CurrentCell.ColumnIndex == 6) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvItem_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            this.ComputeTotal();
        }

        private void dgvforStockTransfer_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void dgvforPurchaseOrder_DataSourceChanged(object sender, EventArgs e)
        {
            this.ComputeTotal();
        }

        #endregion


        #region LoadFile

        private void LoadFile()
        {
            try
            {
                this.RemoveFilesInQueued();
                this._browser = new frmDocumentBrowser();
                this._browser.ShowDialog();
                _reference = this._browser.reference;
                _sourcePath = this._browser.path;
                _path = Path.GetDirectoryName(Application.ExecutablePath) + @"\Queue\";
                string response = null;

                if (!string.IsNullOrWhiteSpace(_reference) && !string.IsNullOrWhiteSpace(_path))
                {
                    chkVerifyItemUnit.Checked = false;
                    chkVerifyTransaction.Checked = false;
                    chkVerifyItemMasterData.Checked = false;
                    string transactionpath = Path.Combine(_path, "T-" + _reference + ".csv");
                    string masterDatapath = Path.Combine(_path, "M-" + _reference + ".csv");
                    string pcostingpath = Path.Combine(_path, "P-" + _reference + ".csv");

                    if (File.Exists(transactionpath) && File.Exists(masterDatapath) && File.Exists(pcostingpath))
                    {
                        this.LoadBound(transactionpath);
                        this.LoadItemMasterData(masterDatapath);
                        this.LoadItemPcosting(pcostingpath);
                    }
                    else
                    {
                        response += !File.Exists(transactionpath) ? string.Format("Error: Transaction file for {0} is missing!\n", _reference) : string.Format("Transaction file for {0} is available.\n", _reference);
                        response += !File.Exists(masterDatapath) ? string.Format("Error: Master Data of items for {0} is missing!\n", _reference) : string.Format("Master Data of items for {0} is available.\n", _reference);
                        response += !File.Exists(pcostingpath) ? string.Format("Error: Units for {0} is missing!\n", _reference) : string.Format("Units for {0} is available.\n", _reference);
                        DisplayMessage(response, false);
                    }

                }

                this.RemoveFilesInQueued();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void LoadBound(string fName)
        {
            String textLine = string.Empty;
            String[] splitLine;

            // clear the grid view
            var data = new DataTable();
            data.Columns.AddRange(new[]
                                   {
                               new DataColumn ("branch_name"),
                               new DataColumn ("warehouse_code"),
                               new DataColumn ("segment"),
                               new DataColumn ("business_segment"),
                               new DataColumn ("branch_code"),
                               new DataColumn ("transaction_type"),
                               new DataColumn ("reference"),
                               new DataColumn ("posted_date"),
                               new DataColumn ("idfile"),
                               new DataColumn ("iduser"),
                               new DataColumn ("crossreference"),

                               new DataColumn ("item_code"),
                               new DataColumn ("item_description"),
                               new DataColumn ("item_qty"),
                               new DataColumn ("item_unit"),
                               //new DataColumn ("item_cost", typeof(decimal)),
                               new DataColumn ("item_cost"),
                              // new DataColumn ("item_price"),
                               new DataColumn ("item_price"),
                               new DataColumn ("item_total"),
                               new DataColumn ("item_conversion"),
                               new DataColumn ("col1"),
                               new DataColumn ("col2"),
                               new DataColumn ("col3"),
                               new DataColumn ("col4"),
                               new DataColumn ("col5"),
                               new DataColumn ("col6"),
                               new DataColumn ("col7"),
                            });

            data.Rows.Clear();
            //data.Columns["item_cost"].DataType = decimal;
            if (System.IO.File.Exists(fName))
            {
                System.IO.StreamReader objReader = new System.IO.StreamReader(fName);

                var contents = objReader.ReadToEnd();

                var strReader = new System.IO.StringReader(contents);
                do
                {
                    textLine = strReader.ReadLine();
                    if (textLine != string.Empty)
                    {
                        splitLine = textLine.Split('|');
                        if (splitLine[0] != string.Empty || splitLine[1] != string.Empty)
                        {
                            data.Rows.Add(splitLine);
                        }
                    }
                } while (strReader.Peek() != -1);
            }

            dgvTransactions.AutoGenerateColumns = false;

            foreach (DataGridViewColumn col in dgvTransactions.Columns)
            {
                col.DataPropertyName = col.Name;

            }
            dgvTransactions.DataSource = data;

            if (data.Rows.Count > 0)
            {
                txtName.Text = data.Rows[0]["branch_name"].ToString();
                txtCode.Text = data.Rows[0]["warehouse_code"].ToString();
                txtCross.Text = data.Rows[0]["reference"].ToString();
            }
        }

        private void LoadItemMasterData(string fName)
        {
            String textLine = string.Empty;
            String[] splitLine;

            // clear the grid view

            var data = new DataTable();
            data.Columns.AddRange(new[]
                                   {
                               new DataColumn ("master_idstock"),
                               new DataColumn ("master_name"),
                               new DataColumn ("master_type"),
                               new DataColumn ("master_supplier"),
                               new DataColumn ("master_status"),
                               new DataColumn ("master_remarks"),
                               new DataColumn ("master_barcode"),
                               new DataColumn ("master_discontinued"),
                               new DataColumn ("master_consignment"),
                               new DataColumn ("master_taxable"),
                               new DataColumn ("master_discountable"),
                               new DataColumn ("master_CatID"),
                               new DataColumn ("master_packaging"),
                               new DataColumn ("master_subcategory"),
                               new DataColumn ("master_subcatid"),
                               new DataColumn ("master_brand"),
                               new DataColumn ("master_brandid")
                            });

            data.Rows.Clear();

            if (System.IO.File.Exists(fName))
            {
                System.IO.StreamReader objReader = new System.IO.StreamReader(fName);

                var contents = objReader.ReadToEnd();

                var strReader = new System.IO.StringReader(contents);
                do
                {
                    textLine = strReader.ReadLine();
                    if (textLine != string.Empty)
                    {
                        splitLine = textLine.Split('|');
                        if (splitLine[0] != string.Empty || splitLine[1] != string.Empty)
                        {
                            data.Rows.Add(splitLine);
                        }
                    }
                } while (strReader.Peek() != -1);
            }

            dgvItemMasterData.AutoGenerateColumns = false;

            foreach (DataGridViewColumn col in dgvItemMasterData.Columns)
            {
                col.DataPropertyName = col.Name;

            }
            dgvItemMasterData.DataSource = data;


        }

        private void LoadItemPcosting(string fName)
        {
            String textLine = string.Empty;
            String[] splitLine;

            // clear the grid view

            var data = new DataTable();
            data.Columns.AddRange(new[]
                                   {
                               new DataColumn ("p_idstock"),
                               new DataColumn ("p_barcode"),
                               new DataColumn ("p_unit"),
                               new DataColumn ("cost"),
                               new DataColumn ("selling"),
                               new DataColumn ("markup"),
                               new DataColumn ("p_conversion"),
                               new DataColumn ("p_costingMethod"),
                            });

            data.Rows.Clear();

            if (System.IO.File.Exists(fName))
            {
                System.IO.StreamReader objReader = new System.IO.StreamReader(fName);

                var contents = objReader.ReadToEnd();

                var strReader = new System.IO.StringReader(contents);
                do
                {
                    textLine = strReader.ReadLine();
                    if (textLine != string.Empty)
                    {
                        splitLine = textLine.Split('|');
                        if (splitLine[0] != string.Empty || splitLine[1] != string.Empty)
                        {
                            data.Rows.Add(splitLine);
                        }
                    }
                } while (strReader.Peek() != -1);
            }

            dgvPcosting.AutoGenerateColumns = false;

            foreach (DataGridViewColumn col in dgvPcosting.Columns)
            {
                col.DataPropertyName = col.Name;

            }
            dgvPcosting.DataSource = data;


        }


        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //txtName.Text = "";
            //txtCode.Text = "";
            //txtCross.Text = "";
            //dgvTransactions.DataSource = new DataTable();
            //dgvItemMasterData.DataSource = new DataTable();
            //dgvPcosting.DataSource = new DataTable();
            ////dgvItemMasterData.Refresh();
            ////dgvTransactions.Refresh();
            ////dgvPcosting.Refresh();
            //this.LoadFile();

            System.Diagnostics.Process.Start(Application.ExecutablePath); // to start new instance of application
            Process.GetCurrentProcess().Kill();
            //this.Close(); //to turn off current app
        }
    }




    /** stored proc
     
        CREATE DEFINER=`root`@`localhost` PROCEDURE `LA_PullTransaction`(reference varchar(45))
BEGIN
set @reference := reference;
SET @SQL = concat(
        "SELECT 
(select branchName from business_segments where idbranch=i.idbranch limit 1),
(select whse from business_segments where idbranch=i.idbranch limit 1),
(select main_segment from business_segments where idbranch=i.idbranch limit 1),
(select business_segment from business_segments where idbranch=i.idbranch limit 1),
(select branchCode from business_segments where idbranch=i.idbranch limit 1),
left(i.reference,2),i.reference,i.date,i.idfile,i.iduser,
ifnull((select crossreference from  ledger l where reference=i.reference limit 1),''),
ifnull(i.idstock,''),
ifnull((select name from  stocks  where idstock=i.idstock limit 1),''),
ifnull(i.quantity,''), ifnull(i.unit,''),  ifnull(i.cost,'0.00'),ifnull(i.selling,'0.00'),ifnull(i.amount,'0.00'),unitQuantity,
''
from invoice i where not cancelled
and i.quantity<>'0' and reference='",@reference,"'
INTO OUTFILE 'C:/ionic/new/ST/",@reference,"/T-", concat(@reference,"-",date_format( now(), '%Y%m%d%h%i%s')), ".csv' FIELDS TERMINATED BY '|' ENCLOSED BY '' LINES TERMINATED BY '\r\n'");

prepare s from @sql;
execute s;
drop prepare s;


SET @SQL = concat(
        "SELECT idstock, name, type, supplier, status, remarks, barcode, discontinued, consignment, taxable, discountable, CatID,
packaging, subcategory, subcatid, brand, brandid FROM stocks
where idstock in (
select idstock 
from invoice i where not cancelled
and i.quantity<>'0' and reference='",@reference,"')
INTO OUTFILE 'C:/ionic/new/ST/",@reference,"/M-", concat(@reference,"-",date_format( now(), '%Y%m%d%h%i%s')), ".csv' FIELDS TERMINATED BY '|' ENCLOSED BY '' LINES TERMINATED BY '\r\n'");

prepare s from @sql;
execute s;
drop prepare s;


SET @SQL = concat(
        "SELECT idStock, barcode, unit, 0 as cost, 0 as selling, 0 as markup, 0 as quantity, costingMethod
     FROM pcosting
where idstock in (
select idstock 
from invoice i where not cancelled
and i.quantity<>'0' and reference='",@reference,"')
INTO OUTFILE 'C:/ionic/new/ST/",@reference,"/P-", concat(@reference,"-",date_format( now(), '%Y%m%d%h%i%s')), ".csv' FIELDS TERMINATED BY '|' ENCLOSED BY '' LINES TERMINATED BY '\r\n'");

prepare s from @sql;
execute s;
drop prepare s;


END
     
     */

}
