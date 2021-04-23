namespace Spoon.Inventory.View
{
    partial class frmTransact
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTransact));
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.item_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_conversion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.chkVerifyTransaction = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dgvItemMasterData = new System.Windows.Forms.DataGridView();
            this.master_idstock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.master_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.master_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.master_supplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.master_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.master_remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.master_barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.master_discontinued = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.master_consignment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.master_taxable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.master_CatID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.master_packaging = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.master_subcategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.master_subcatid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.master_brand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.master_brandid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkVerifyItemMasterData = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvPcosting = new System.Windows.Forms.DataGridView();
            this.p_idstock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_conversion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.p_costingMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel7 = new System.Windows.Forms.Panel();
            this.chkVerifyItemUnit = new System.Windows.Forms.CheckBox();
            this.txtCross = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemMasterData)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPcosting)).BeginInit();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTransactions
            // 
            this.dgvTransactions.AllowUserToAddRows = false;
            this.dgvTransactions.AllowUserToDeleteRows = false;
            this.dgvTransactions.BackgroundColor = System.Drawing.Color.White;
            this.dgvTransactions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.item_id,
            this.item_barcode,
            this.item_code,
            this.item_description,
            this.item_unit,
            this.item_qty,
            this.item_price,
            this.item_total,
            this.item_conversion});
            this.dgvTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTransactions.EnableHeadersVisualStyles = false;
            this.dgvTransactions.Location = new System.Drawing.Point(4, 4);
            this.dgvTransactions.Margin = new System.Windows.Forms.Padding(4);
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.ReadOnly = true;
            this.dgvTransactions.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransactions.Size = new System.Drawing.Size(1136, 446);
            this.dgvTransactions.TabIndex = 14;
            this.dgvTransactions.DataSourceChanged += new System.EventHandler(this.dgvforPurchaseOrder_DataSourceChanged);
            this.dgvTransactions.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvItem_EditingControlShowing);
            this.dgvTransactions.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvItem_RowPostPaint);
            this.dgvTransactions.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvItem_UserDeletedRow);
            // 
            // item_id
            // 
            dataGridViewCellStyle2.NullValue = "0";
            this.item_id.DefaultCellStyle = dataGridViewCellStyle2;
            this.item_id.HeaderText = "ID";
            this.item_id.MaxInputLength = 12;
            this.item_id.Name = "item_id";
            this.item_id.ReadOnly = true;
            this.item_id.Visible = false;
            // 
            // item_barcode
            // 
            this.item_barcode.HeaderText = "Barcode";
            this.item_barcode.MaxInputLength = 50;
            this.item_barcode.Name = "item_barcode";
            this.item_barcode.ReadOnly = true;
            this.item_barcode.Width = 150;
            // 
            // item_code
            // 
            this.item_code.HeaderText = "Itemcode";
            this.item_code.Name = "item_code";
            this.item_code.ReadOnly = true;
            // 
            // item_description
            // 
            this.item_description.HeaderText = "Description";
            this.item_description.Name = "item_description";
            this.item_description.ReadOnly = true;
            this.item_description.Width = 150;
            // 
            // item_unit
            // 
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.item_unit.DefaultCellStyle = dataGridViewCellStyle3;
            this.item_unit.HeaderText = "Unit";
            this.item_unit.Name = "item_unit";
            this.item_unit.ReadOnly = true;
            this.item_unit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.item_unit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.item_unit.Width = 87;
            // 
            // item_qty
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = "0.00";
            this.item_qty.DefaultCellStyle = dataGridViewCellStyle4;
            this.item_qty.HeaderText = "Quantity";
            this.item_qty.MaxInputLength = 12;
            this.item_qty.Name = "item_qty";
            this.item_qty.ReadOnly = true;
            // 
            // item_price
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = "0.00";
            this.item_price.DefaultCellStyle = dataGridViewCellStyle5;
            this.item_price.HeaderText = "Price";
            this.item_price.MaxInputLength = 12;
            this.item_price.Name = "item_price";
            this.item_price.ReadOnly = true;
            // 
            // item_total
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = "0.00";
            this.item_total.DefaultCellStyle = dataGridViewCellStyle6;
            this.item_total.HeaderText = "Total";
            this.item_total.MaxInputLength = 12;
            this.item_total.Name = "item_total";
            this.item_total.ReadOnly = true;
            // 
            // item_conversion
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = "0.00";
            this.item_conversion.DefaultCellStyle = dataGridViewCellStyle7;
            this.item_conversion.HeaderText = "Conversion";
            this.item_conversion.MaxInputLength = 12;
            this.item_conversion.Name = "item_conversion";
            this.item_conversion.ReadOnly = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(805, 693);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(151, 33);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(1012, 693);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(151, 33);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.CalendarFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpDate.Enabled = false;
            this.dtpDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(0, 0);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDate.MinDate = new System.DateTime(1991, 12, 31, 0, 0, 0, 0);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(177, 27);
            this.dtpDate.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.dtpDate);
            this.panel2.Location = new System.Drawing.Point(947, 80);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(177, 25);
            this.panel2.TabIndex = 88;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(28, 81);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 23);
            this.label4.TabIndex = 92;
            this.label4.Text = "Code:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.SeaGreen;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1189, 12);
            this.panel3.TabIndex = 93;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Location = new System.Drawing.Point(24, 111);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 23);
            this.label5.TabIndex = 94;
            this.label5.Text = "Name :";
            // 
            // txtCode
            // 
            this.txtCode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCode.Enabled = false;
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.Location = new System.Drawing.Point(132, 82);
            this.txtCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtCode.MaxLength = 10;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(245, 22);
            this.txtCode.TabIndex = 3;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.Gainsboro;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Enabled = false;
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(132, 112);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.MaxLength = 200;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(468, 22);
            this.txtName.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(832, 6);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 23);
            this.label7.TabIndex = 99;
            this.label7.Text = "Total :";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotal.ForeColor = System.Drawing.Color.Black;
            this.lblTotal.Location = new System.Drawing.Point(919, 6);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(205, 23);
            this.lblTotal.TabIndex = 100;
            this.lblTotal.Text = "0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.label9.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label9.Location = new System.Drawing.Point(-3, 14);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(304, 46);
            this.label9.TabIndex = 103;
            this.label9.Text = "Document Transfer";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label11.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label11.Location = new System.Drawing.Point(761, 82);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(111, 23);
            this.label11.TabIndex = 106;
            this.label11.Text = "Date Posted :";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkGray;
            this.panel4.Location = new System.Drawing.Point(17, 62);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1161, 1);
            this.panel4.TabIndex = 110;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(16, 151);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1152, 516);
            this.tabControl1.TabIndex = 124;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvTransactions);
            this.tabPage1.Controls.Add(this.panel6);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1144, 487);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Transactions";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.chkVerifyTransaction);
            this.panel6.Controls.Add(this.lblTotal);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(4, 450);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1136, 33);
            this.panel6.TabIndex = 17;
            // 
            // chkVerifyTransaction
            // 
            this.chkVerifyTransaction.AutoSize = true;
            this.chkVerifyTransaction.Location = new System.Drawing.Point(8, 9);
            this.chkVerifyTransaction.Margin = new System.Windows.Forms.Padding(4);
            this.chkVerifyTransaction.Name = "chkVerifyTransaction";
            this.chkVerifyTransaction.Size = new System.Drawing.Size(140, 21);
            this.chkVerifyTransaction.TabIndex = 101;
            this.chkVerifyTransaction.Text = "Verify transaction";
            this.chkVerifyTransaction.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel5);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1144, 487);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Item Master Data";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dgvItemMasterData);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(4, 4);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1136, 446);
            this.panel5.TabIndex = 17;
            // 
            // dgvItemMasterData
            // 
            this.dgvItemMasterData.AllowUserToAddRows = false;
            this.dgvItemMasterData.AllowUserToDeleteRows = false;
            this.dgvItemMasterData.BackgroundColor = System.Drawing.Color.White;
            this.dgvItemMasterData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemMasterData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvItemMasterData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemMasterData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.master_idstock,
            this.master_name,
            this.master_type,
            this.master_supplier,
            this.master_status,
            this.master_remarks,
            this.master_barcode,
            this.master_discontinued,
            this.master_consignment,
            this.master_taxable,
            this.master_CatID,
            this.master_packaging,
            this.master_subcategory,
            this.master_subcatid,
            this.master_brand,
            this.master_brandid});
            this.dgvItemMasterData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItemMasterData.EnableHeadersVisualStyles = false;
            this.dgvItemMasterData.Location = new System.Drawing.Point(0, 0);
            this.dgvItemMasterData.Margin = new System.Windows.Forms.Padding(4);
            this.dgvItemMasterData.Name = "dgvItemMasterData";
            this.dgvItemMasterData.ReadOnly = true;
            this.dgvItemMasterData.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvItemMasterData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemMasterData.Size = new System.Drawing.Size(1136, 446);
            this.dgvItemMasterData.TabIndex = 15;
            // 
            // master_idstock
            // 
            this.master_idstock.HeaderText = "Itemcode";
            this.master_idstock.Name = "master_idstock";
            this.master_idstock.ReadOnly = true;
            // 
            // master_name
            // 
            this.master_name.HeaderText = "Name";
            this.master_name.Name = "master_name";
            this.master_name.ReadOnly = true;
            // 
            // master_type
            // 
            this.master_type.HeaderText = "Type";
            this.master_type.Name = "master_type";
            this.master_type.ReadOnly = true;
            // 
            // master_supplier
            // 
            this.master_supplier.HeaderText = "Supplier";
            this.master_supplier.Name = "master_supplier";
            this.master_supplier.ReadOnly = true;
            // 
            // master_status
            // 
            this.master_status.HeaderText = "Status";
            this.master_status.Name = "master_status";
            this.master_status.ReadOnly = true;
            // 
            // master_remarks
            // 
            this.master_remarks.HeaderText = "Remarks";
            this.master_remarks.Name = "master_remarks";
            this.master_remarks.ReadOnly = true;
            // 
            // master_barcode
            // 
            this.master_barcode.HeaderText = "Barcode";
            this.master_barcode.Name = "master_barcode";
            this.master_barcode.ReadOnly = true;
            // 
            // master_discontinued
            // 
            this.master_discontinued.HeaderText = "Discontinued";
            this.master_discontinued.Name = "master_discontinued";
            this.master_discontinued.ReadOnly = true;
            // 
            // master_consignment
            // 
            this.master_consignment.HeaderText = "Consignment";
            this.master_consignment.Name = "master_consignment";
            this.master_consignment.ReadOnly = true;
            // 
            // master_taxable
            // 
            this.master_taxable.HeaderText = "Taxable";
            this.master_taxable.Name = "master_taxable";
            this.master_taxable.ReadOnly = true;
            // 
            // master_CatID
            // 
            this.master_CatID.HeaderText = "CatID";
            this.master_CatID.Name = "master_CatID";
            this.master_CatID.ReadOnly = true;
            // 
            // master_packaging
            // 
            this.master_packaging.HeaderText = "Packaging";
            this.master_packaging.Name = "master_packaging";
            this.master_packaging.ReadOnly = true;
            // 
            // master_subcategory
            // 
            this.master_subcategory.HeaderText = "Subcategory";
            this.master_subcategory.Name = "master_subcategory";
            this.master_subcategory.ReadOnly = true;
            // 
            // master_subcatid
            // 
            this.master_subcatid.HeaderText = "Subcatid";
            this.master_subcatid.Name = "master_subcatid";
            this.master_subcatid.ReadOnly = true;
            // 
            // master_brand
            // 
            this.master_brand.HeaderText = "Brand";
            this.master_brand.Name = "master_brand";
            this.master_brand.ReadOnly = true;
            // 
            // master_brandid
            // 
            this.master_brandid.HeaderText = "Brandid";
            this.master_brandid.Name = "master_brandid";
            this.master_brandid.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkVerifyItemMasterData);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(4, 450);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1136, 33);
            this.panel1.TabIndex = 16;
            // 
            // chkVerifyItemMasterData
            // 
            this.chkVerifyItemMasterData.AutoSize = true;
            this.chkVerifyItemMasterData.Location = new System.Drawing.Point(7, 7);
            this.chkVerifyItemMasterData.Margin = new System.Windows.Forms.Padding(4);
            this.chkVerifyItemMasterData.Name = "chkVerifyItemMasterData";
            this.chkVerifyItemMasterData.Size = new System.Drawing.Size(130, 21);
            this.chkVerifyItemMasterData.TabIndex = 103;
            this.chkVerifyItemMasterData.Text = "Verify Item Data";
            this.chkVerifyItemMasterData.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvPcosting);
            this.tabPage3.Controls.Add(this.panel7);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(1144, 487);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Item Master Data (Pcosting)";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvPcosting
            // 
            this.dgvPcosting.AllowUserToAddRows = false;
            this.dgvPcosting.AllowUserToDeleteRows = false;
            this.dgvPcosting.BackgroundColor = System.Drawing.Color.White;
            this.dgvPcosting.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPcosting.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvPcosting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPcosting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.p_idstock,
            this.p_barcode,
            this.p_unit,
            this.p_conversion,
            this.p_costingMethod});
            this.dgvPcosting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPcosting.EnableHeadersVisualStyles = false;
            this.dgvPcosting.Location = new System.Drawing.Point(4, 4);
            this.dgvPcosting.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPcosting.Name = "dgvPcosting";
            this.dgvPcosting.ReadOnly = true;
            this.dgvPcosting.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPcosting.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPcosting.Size = new System.Drawing.Size(1136, 446);
            this.dgvPcosting.TabIndex = 16;
            // 
            // p_idstock
            // 
            this.p_idstock.HeaderText = "Itemcode";
            this.p_idstock.Name = "p_idstock";
            this.p_idstock.ReadOnly = true;
            // 
            // p_barcode
            // 
            this.p_barcode.HeaderText = "Barcode";
            this.p_barcode.Name = "p_barcode";
            this.p_barcode.ReadOnly = true;
            // 
            // p_unit
            // 
            this.p_unit.HeaderText = "Unit";
            this.p_unit.Name = "p_unit";
            this.p_unit.ReadOnly = true;
            // 
            // p_conversion
            // 
            this.p_conversion.HeaderText = "Conversion";
            this.p_conversion.Name = "p_conversion";
            this.p_conversion.ReadOnly = true;
            // 
            // p_costingMethod
            // 
            this.p_costingMethod.HeaderText = "CostingMethod";
            this.p_costingMethod.Name = "p_costingMethod";
            this.p_costingMethod.ReadOnly = true;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.chkVerifyItemUnit);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(4, 450);
            this.panel7.Margin = new System.Windows.Forms.Padding(4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1136, 33);
            this.panel7.TabIndex = 17;
            // 
            // chkVerifyItemUnit
            // 
            this.chkVerifyItemUnit.AutoSize = true;
            this.chkVerifyItemUnit.Location = new System.Drawing.Point(8, 7);
            this.chkVerifyItemUnit.Margin = new System.Windows.Forms.Padding(4);
            this.chkVerifyItemUnit.Name = "chkVerifyItemUnit";
            this.chkVerifyItemUnit.Size = new System.Drawing.Size(130, 21);
            this.chkVerifyItemUnit.TabIndex = 102;
            this.chkVerifyItemUnit.Text = "Verify Item units";
            this.chkVerifyItemUnit.UseVisualStyleBackColor = true;
            // 
            // txtCross
            // 
            this.txtCross.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCross.BackColor = System.Drawing.Color.Gainsboro;
            this.txtCross.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCross.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCross.Enabled = false;
            this.txtCross.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCross.Location = new System.Drawing.Point(947, 111);
            this.txtCross.Margin = new System.Windows.Forms.Padding(4);
            this.txtCross.MaxLength = 20;
            this.txtCross.Name = "txtCross";
            this.txtCross.Size = new System.Drawing.Size(183, 22);
            this.txtCross.TabIndex = 125;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(761, 110);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 23);
            this.label1.TabIndex = 126;
            this.label1.Text = "Cross :";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.White;
            this.btnSearch.BackgroundImage = global::Spoon.Properties.Resources.search;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(1137, 111);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(33, 22);
            this.btnSearch.TabIndex = 127;
            this.btnSearch.Text = "...";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // frmTransact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1189, 738);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtCross);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1077, 738);
            this.Name = "frmTransact";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTransact";
            this.Load += new System.EventHandler(this.frmTransact_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemMasterData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPcosting)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTransactions;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvItemMasterData;
        private System.Windows.Forms.TextBox txtCross;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn master_idstock;
        private System.Windows.Forms.DataGridViewTextBoxColumn master_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn master_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn master_supplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn master_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn master_remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn master_barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn master_discontinued;
        private System.Windows.Forms.DataGridViewTextBoxColumn master_consignment;
        private System.Windows.Forms.DataGridViewTextBoxColumn master_taxable;
        private System.Windows.Forms.DataGridViewTextBoxColumn master_CatID;
        private System.Windows.Forms.DataGridViewTextBoxColumn master_packaging;
        private System.Windows.Forms.DataGridViewTextBoxColumn master_subcategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn master_subcatid;
        private System.Windows.Forms.DataGridViewTextBoxColumn master_brand;
        private System.Windows.Forms.DataGridViewTextBoxColumn master_brandid;
        private System.Windows.Forms.DataGridView dgvPcosting;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_idstock;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_conversion;
        private System.Windows.Forms.DataGridViewTextBoxColumn p_costingMethod;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_description;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_total;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_conversion;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.CheckBox chkVerifyTransaction;
        private System.Windows.Forms.CheckBox chkVerifyItemUnit;
        private System.Windows.Forms.CheckBox chkVerifyItemMasterData;
    }
}