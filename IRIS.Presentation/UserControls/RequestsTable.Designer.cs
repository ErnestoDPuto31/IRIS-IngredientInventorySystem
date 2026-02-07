using System.Drawing;
using System.Windows.Forms;

namespace IRIS.UI.Controls
{
    partial class RequestsTable
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgv;
        private DataGridViewTextBoxColumn colSubject;
        private DataGridViewTextBoxColumn colFaculty;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colSubmitted; // NEW
        private DataGridViewButtonColumn colView;       // CHANGED to Eye Button

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle statusStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle buttonStyle = new DataGridViewCellStyle();

            dgv = new DataGridView();
            colSubject = new DataGridViewTextBoxColumn();
            colFaculty = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colSubmitted = new DataGridViewTextBoxColumn();
            colView = new DataGridViewButtonColumn();

            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            SuspendLayout();

            // 
            // dgv Setup
            // 
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Header Styling (Poppins/Segoe UI)
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = Color.White;
            headerStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            headerStyle.ForeColor = Color.Black;
            headerStyle.SelectionBackColor = Color.White;
            headerStyle.SelectionForeColor = Color.Black;
            headerStyle.WrapMode = DataGridViewTriState.True;
            dgv.ColumnHeadersDefaultCellStyle = headerStyle;
            dgv.ColumnHeadersHeight = 50;
            dgv.EnableHeadersVisualStyles = false;

            // Add Columns in correct order
            dgv.Columns.AddRange(new DataGridViewColumn[] {
                colSubject,
                colFaculty,
                colStatus,
                colSubmitted,
                colView
            });

            // Default Cell Styling
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            cellStyle.BackColor = Color.White;
            cellStyle.Font = new Font("Segoe UI", 9.5F);
            cellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            cellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250); // Light gray selection
            cellStyle.SelectionForeColor = Color.Black;
            cellStyle.WrapMode = DataGridViewTriState.False;
            dgv.DefaultCellStyle = cellStyle;

            dgv.Dock = DockStyle.Fill;
            dgv.GridColor = Color.FromArgb(240, 240, 240);
            dgv.Location = new Point(0, 0);
            dgv.Name = "dgv";
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.RowTemplate.Height = 55;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new Size(1429, 617);
            dgv.TabIndex = 0;

            // 
            // 1. colSubject
            // 
            colSubject.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colSubject.DataPropertyName = "Subject";
            colSubject.FillWeight = 30F;
            colSubject.HeaderText = "Subject/Course";
            colSubject.Name = "colSubject";
            colSubject.ReadOnly = true;

            // 
            // 2. colFaculty
            // 
            colFaculty.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colFaculty.DataPropertyName = "FacultyName";
            colFaculty.FillWeight = 25F;
            colFaculty.HeaderText = "Faculty";
            colFaculty.Name = "colFaculty";
            colFaculty.ReadOnly = true;

            // 
            // 3. colStatus
            // 
            colStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colStatus.DataPropertyName = "Status";
            colStatus.FillWeight = 15F;
            colStatus.HeaderText = "Status";
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            // Center align the status text
            statusStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colStatus.DefaultCellStyle = statusStyle;

            // 
            // 4. colSubmitted (NEW)
            // 
            colSubmitted.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colSubmitted.DataPropertyName = "CreatedAt"; // Ensure this matches DB property
            colSubmitted.FillWeight = 15F;
            colSubmitted.HeaderText = "Submitted";
            colSubmitted.Name = "colSubmitted";
            colSubmitted.ReadOnly = true;
            colSubmitted.DefaultCellStyle.Format = "MM/dd/yyyy"; // Format like 1/26/2026

            // 
            // 5. colView (The Eye Button)
            // 
            colView.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colView.Width = 80;
            colView.HeaderText = ""; // Blank Header
            colView.Name = "colView";
            colView.ReadOnly = true;
            colView.Text = "👁"; // The Eye Symbol
            colView.UseColumnTextForButtonValue = true;

            // Style the button to look flat/minimal
            colView.FlatStyle = FlatStyle.Flat;
            buttonStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            buttonStyle.Font = new Font("Segoe UI Symbol", 12F); // Font that supports icons well
            buttonStyle.ForeColor = Color.DarkGray;
            buttonStyle.SelectionForeColor = Color.Black;
            colView.DefaultCellStyle = buttonStyle;

            // 
            // RequestsTable
            // 
            Controls.Add(dgv);
            Name = "RequestsTable";
            Size = new Size(1429, 617);
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ResumeLayout(false);
        }
    }
}