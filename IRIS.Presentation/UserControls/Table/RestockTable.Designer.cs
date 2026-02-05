namespace IRIS.Presentation.UserControls.Table
{
    partial class RestockTable
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            gvTable = new Guna.UI2.WinForms.Guna2DataGridView();
            ((System.ComponentModel.ISupportInitialize)gvTable).BeginInit();
            SuspendLayout();
            // 
            // gvTable
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            gvTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            gvTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            gvTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            gvTable.DefaultCellStyle = dataGridViewCellStyle3;
            gvTable.Dock = DockStyle.Fill;
            gvTable.GridColor = Color.FromArgb(231, 229, 255);
            gvTable.Location = new Point(0, 0);
            gvTable.Name = "gvTable";
            gvTable.RowHeadersVisible = false;
            gvTable.RowHeadersWidth = 51;
            gvTable.Size = new Size(1000, 500);
            gvTable.TabIndex = 0;
            gvTable.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            gvTable.ThemeStyle.AlternatingRowsStyle.Font = null;
            gvTable.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            gvTable.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            gvTable.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            gvTable.ThemeStyle.BackColor = Color.White;
            gvTable.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            gvTable.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            gvTable.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            gvTable.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            gvTable.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            gvTable.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gvTable.ThemeStyle.HeaderStyle.Height = 4;
            gvTable.ThemeStyle.ReadOnly = false;
            gvTable.ThemeStyle.RowsStyle.BackColor = Color.White;
            gvTable.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            gvTable.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            gvTable.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            gvTable.ThemeStyle.RowsStyle.Height = 29;
            gvTable.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            gvTable.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // ReusableTable
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gvTable);
            Name = "ReusableTable";
            Size = new Size(1000, 500);
            ((System.ComponentModel.ISupportInitialize)gvTable).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2DataGridView gvTable;
    }
}
