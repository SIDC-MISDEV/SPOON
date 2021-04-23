namespace Spoon.Inventory.View
{
    partial class frmDocumentBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocumentBrowser));
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvPcosting = new System.Windows.Forms.DataGridView();
            this.file_path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.warehouse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.file_reference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.file_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.foldername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPcosting)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.SeaGreen;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(541, 10);
            this.panel3.TabIndex = 94;
            // 
            // dgvPcosting
            // 
            this.dgvPcosting.AllowUserToAddRows = false;
            this.dgvPcosting.AllowUserToDeleteRows = false;
            this.dgvPcosting.AllowUserToResizeRows = false;
            this.dgvPcosting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPcosting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPcosting.BackgroundColor = System.Drawing.Color.White;
            this.dgvPcosting.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPcosting.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPcosting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPcosting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.file_path,
            this.warehouse,
            this.file_reference,
            this.file_date,
            this.foldername});
            this.dgvPcosting.EnableHeadersVisualStyles = false;
            this.dgvPcosting.Location = new System.Drawing.Point(0, 0);
            this.dgvPcosting.MultiSelect = false;
            this.dgvPcosting.Name = "dgvPcosting";
            this.dgvPcosting.ReadOnly = true;
            this.dgvPcosting.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPcosting.RowHeadersVisible = false;
            this.dgvPcosting.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPcosting.Size = new System.Drawing.Size(517, 367);
            this.dgvPcosting.TabIndex = 95;
            // 
            // file_path
            // 
            this.file_path.HeaderText = "Column1";
            this.file_path.Name = "file_path";
            this.file_path.ReadOnly = true;
            this.file_path.Visible = false;
            // 
            // warehouse
            // 
            this.warehouse.HeaderText = "Warehouse";
            this.warehouse.Name = "warehouse";
            this.warehouse.ReadOnly = true;
            // 
            // file_reference
            // 
            this.file_reference.HeaderText = "Reference";
            this.file_reference.Name = "file_reference";
            this.file_reference.ReadOnly = true;
            // 
            // file_date
            // 
            this.file_date.HeaderText = "Date extracted";
            this.file_date.Name = "file_date";
            this.file_date.ReadOnly = true;
            // 
            // foldername
            // 
            this.foldername.HeaderText = "foldername";
            this.foldername.Name = "foldername";
            this.foldername.ReadOnly = true;
            this.foldername.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dgvPcosting);
            this.panel1.Location = new System.Drawing.Point(12, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(520, 367);
            this.panel1.TabIndex = 96;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.label9.ForeColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(5, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(242, 37);
            this.label9.TabIndex = 104;
            this.label9.Text = "Document browser";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkGray;
            this.panel4.Location = new System.Drawing.Point(12, 53);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(520, 1);
            this.panel4.TabIndex = 111;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(419, 457);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(113, 27);
            this.btnCancel.TabIndex = 113;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(264, 457);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(113, 27);
            this.btnSave.TabIndex = 112;
            this.btnSave.Text = "Select";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmDocumentBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(541, 496);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDocumentBrowser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDocumentBrowser";
            this.Load += new System.EventHandler(this.frmDocumentBrowser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPcosting)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgvPcosting;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn file_path;
        private System.Windows.Forms.DataGridViewTextBoxColumn warehouse;
        private System.Windows.Forms.DataGridViewTextBoxColumn file_reference;
        private System.Windows.Forms.DataGridViewTextBoxColumn file_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn foldername;
    }
}