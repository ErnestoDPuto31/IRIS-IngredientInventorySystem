namespace IRIS.Presentation.Window_Forms
{
    partial class frmViewRequests
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
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            pnlContent = new Panel();
            labelRequested = new Label();
            lblItemsTitle = new Label();
            bunifuDataGridView1 = new Bunifu.UI.WinForms.BunifuDataGridView();
            colIngredient = new DataGridViewTextBoxColumn();
            colRequested = new DataGridViewTextBoxColumn();
            btnClose = new Button();
            btnApprove = new FontAwesome.Sharp.IconButton();
            btnReject = new Button();
            materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            lblAllowedQtyData = new Label();
            lblRecipeCostData = new Label();
            lblStudentData = new Label();
            lblDateData = new Label();
            lblFacultyData = new Label();
            lblSubjectData = new Label();
            label8 = new Label();
            label5 = new Label();
            label6 = new Label();
            label3 = new Label();
            label4 = new Label();
            label2 = new Label();
            label1 = new Label();
            txtRemarks = new TextBox();
            lblRemarksHeader = new Label();
            lblStatusBadge = new Label();
            btnTopClose = new FontAwesome.Sharp.IconButton();
            pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bunifuDataGridView1).BeginInit();
            SuspendLayout();
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.White;
            pnlContent.Controls.Add(labelRequested);
            pnlContent.Controls.Add(lblItemsTitle);
            pnlContent.Controls.Add(bunifuDataGridView1);
            pnlContent.Controls.Add(btnClose);
            pnlContent.Controls.Add(btnApprove);
            pnlContent.Controls.Add(btnReject);
            pnlContent.Controls.Add(materialDivider1);
            pnlContent.Controls.Add(lblAllowedQtyData);
            pnlContent.Controls.Add(lblRecipeCostData);
            pnlContent.Controls.Add(lblStudentData);
            pnlContent.Controls.Add(lblDateData);
            pnlContent.Controls.Add(lblFacultyData);
            pnlContent.Controls.Add(lblSubjectData);
            pnlContent.Controls.Add(label8);
            pnlContent.Controls.Add(label5);
            pnlContent.Controls.Add(label6);
            pnlContent.Controls.Add(label3);
            pnlContent.Controls.Add(label4);
            pnlContent.Controls.Add(label2);
            pnlContent.Controls.Add(label1);
            pnlContent.Controls.Add(txtRemarks);
            pnlContent.Controls.Add(lblRemarksHeader);
            pnlContent.Controls.Add(lblStatusBadge);
            pnlContent.Controls.Add(btnTopClose);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Font = new Font("Segoe UI", 10F);
            pnlContent.Location = new Point(0, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(640, 692);
            pnlContent.TabIndex = 0;
            // 
            // labelRequested
            // 
            labelRequested.AutoSize = true;
            labelRequested.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            labelRequested.ForeColor = Color.Gray;
            labelRequested.Location = new Point(40, 557);
            labelRequested.Name = "labelRequested";
            labelRequested.Size = new Size(137, 21);
            labelRequested.TabIndex = 44;
            labelRequested.Text = "[REQUESTED BY]";
            // 
            // lblItemsTitle
            // 
            lblItemsTitle.AutoSize = true;
            lblItemsTitle.Font = new Font("Segoe UI Semibold", 10F);
            lblItemsTitle.Location = new Point(40, 310);
            lblItemsTitle.Name = "lblItemsTitle";
            lblItemsTitle.Size = new Size(165, 28);
            lblItemsTitle.TabIndex = 0;
            lblItemsTitle.Text = "Requested Items";
            // 
            // bunifuDataGridView1
            // 
            bunifuDataGridView1.AllowCustomTheming = true;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(248, 251, 255);
            dataGridViewCellStyle7.ForeColor = Color.Black;
            bunifuDataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            bunifuDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            bunifuDataGridView1.BackgroundColor = Color.White;
            bunifuDataGridView1.BorderStyle = BorderStyle.None;
            bunifuDataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            bunifuDataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.White;
            dataGridViewCellStyle8.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dataGridViewCellStyle8.ForeColor = Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = Color.White;
            dataGridViewCellStyle8.SelectionForeColor = Color.Black;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            bunifuDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            bunifuDataGridView1.ColumnHeadersHeight = 40;
            bunifuDataGridView1.Columns.AddRange(new DataGridViewColumn[] { colIngredient, colRequested });
            bunifuDataGridView1.CurrentTheme.AlternatingRowsStyle.BackColor = Color.FromArgb(248, 251, 255);
            bunifuDataGridView1.CurrentTheme.AlternatingRowsStyle.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            bunifuDataGridView1.CurrentTheme.AlternatingRowsStyle.ForeColor = Color.Black;
            bunifuDataGridView1.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = Color.FromArgb(210, 232, 255);
            bunifuDataGridView1.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = Color.Black;
            bunifuDataGridView1.CurrentTheme.BackColor = Color.White;
            bunifuDataGridView1.CurrentTheme.GridColor = Color.FromArgb(240, 240, 240);
            bunifuDataGridView1.CurrentTheme.HeaderStyle.BackColor = Color.White;
            bunifuDataGridView1.CurrentTheme.HeaderStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            bunifuDataGridView1.CurrentTheme.HeaderStyle.ForeColor = Color.Black;
            bunifuDataGridView1.CurrentTheme.HeaderStyle.SelectionBackColor = Color.White;
            bunifuDataGridView1.CurrentTheme.HeaderStyle.SelectionForeColor = Color.Black;
            bunifuDataGridView1.CurrentTheme.Name = null;
            bunifuDataGridView1.CurrentTheme.RowsStyle.BackColor = Color.White;
            bunifuDataGridView1.CurrentTheme.RowsStyle.Font = new Font("Segoe UI", 9F);
            bunifuDataGridView1.CurrentTheme.RowsStyle.ForeColor = Color.Black;
            bunifuDataGridView1.CurrentTheme.RowsStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            bunifuDataGridView1.CurrentTheme.RowsStyle.SelectionForeColor = Color.Black;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.White;
            dataGridViewCellStyle9.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle9.ForeColor = Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle9.SelectionForeColor = Color.Black;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.False;
            bunifuDataGridView1.DefaultCellStyle = dataGridViewCellStyle9;
            bunifuDataGridView1.EnableHeadersVisualStyles = false;
            bunifuDataGridView1.GridColor = Color.FromArgb(240, 240, 240);
            bunifuDataGridView1.HeaderBackColor = Color.White;
            bunifuDataGridView1.HeaderBgColor = Color.Empty;
            bunifuDataGridView1.HeaderForeColor = Color.Black;
            bunifuDataGridView1.Location = new Point(40, 344);
            bunifuDataGridView1.Name = "bunifuDataGridView1";
            bunifuDataGridView1.RowHeadersVisible = false;
            bunifuDataGridView1.RowHeadersWidth = 62;
            bunifuDataGridView1.RowTemplate.Height = 35;
            bunifuDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            bunifuDataGridView1.Size = new Size(560, 200);
            bunifuDataGridView1.TabIndex = 22;
            bunifuDataGridView1.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
            // 
            // colIngredient
            // 
            colIngredient.HeaderText = "Ingredient";
            colIngredient.MinimumWidth = 8;
            colIngredient.Name = "colIngredient";
            // 
            // colRequested
            // 
            colRequested.HeaderText = "Qty Requested";
            colRequested.MinimumWidth = 8;
            colRequested.Name = "colRequested";
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10F);
            btnClose.ForeColor = Color.Black;
            btnClose.Location = new Point(40, 600);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 45);
            btnClose.TabIndex = 23;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnTopClose_Click;
            // 
            // btnApprove
            // 
            btnApprove.BackColor = Color.FromArgb(22, 163, 74);
            btnApprove.FlatStyle = FlatStyle.Flat;
            btnApprove.Font = new Font("Segoe UI Semibold", 10F);
            btnApprove.ForeColor = Color.White;
            btnApprove.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnApprove.IconColor = Color.White;
            btnApprove.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnApprove.IconSize = 20;
            btnApprove.Location = new Point(450, 600);
            btnApprove.Name = "btnApprove";
            btnApprove.Size = new Size(150, 45);
            btnApprove.TabIndex = 24;
            btnApprove.Text = " Approve";
            btnApprove.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnApprove.UseVisualStyleBackColor = false;
            btnApprove.Click += BtnApprove_Click;
            // 
            // btnReject
            // 
            btnReject.BackColor = Color.FromArgb(254, 242, 242);
            btnReject.FlatStyle = FlatStyle.Flat;
            btnReject.Font = new Font("Segoe UI Semibold", 10F);
            btnReject.ForeColor = Color.FromArgb(220, 38, 38);
            btnReject.Location = new Point(290, 600);
            btnReject.Name = "btnReject";
            btnReject.Size = new Size(150, 45);
            btnReject.TabIndex = 25;
            btnReject.Text = "Reject";
            btnReject.UseVisualStyleBackColor = false;
            // 
            // materialDivider1
            // 
            materialDivider1.BackColor = Color.FromArgb(226, 232, 240);
            materialDivider1.Depth = 0;
            materialDivider1.Location = new Point(40, 300);
            materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            materialDivider1.Name = "materialDivider1";
            materialDivider1.Size = new Size(560, 1);
            materialDivider1.TabIndex = 26;
            // 
            // lblAllowedQtyData
            // 
            lblAllowedQtyData.AutoSize = true;
            lblAllowedQtyData.Font = new Font("Segoe UI", 11F);
            lblAllowedQtyData.Location = new Point(350, 252);
            lblAllowedQtyData.Name = "lblAllowedQtyData";
            lblAllowedQtyData.Size = new Size(25, 30);
            lblAllowedQtyData.TabIndex = 27;
            lblAllowedQtyData.Text = "0";
            // 
            // lblRecipeCostData
            // 
            lblRecipeCostData.AutoSize = true;
            lblRecipeCostData.Font = new Font("Segoe UI", 11F);
            lblRecipeCostData.Location = new Point(40, 252);
            lblRecipeCostData.Name = "lblRecipeCostData";
            lblRecipeCostData.Size = new Size(73, 30);
            lblRecipeCostData.TabIndex = 28;
            lblRecipeCostData.Text = "₱ 0.00";
            // 
            // lblStudentData
            // 
            lblStudentData.AutoSize = true;
            lblStudentData.Font = new Font("Segoe UI", 11F);
            lblStudentData.Location = new Point(350, 182);
            lblStudentData.Name = "lblStudentData";
            lblStudentData.Size = new Size(25, 30);
            lblStudentData.TabIndex = 29;
            lblStudentData.Text = "0";
            // 
            // lblDateData
            // 
            lblDateData.AutoSize = true;
            lblDateData.Font = new Font("Segoe UI", 11F);
            lblDateData.Location = new Point(40, 182);
            lblDateData.Name = "lblDateData";
            lblDateData.Size = new Size(163, 30);
            lblDateData.TabIndex = 30;
            lblDateData.Text = "[MM/DD/YYYY]";
            // 
            // lblFacultyData
            // 
            lblFacultyData.AutoSize = true;
            lblFacultyData.Font = new Font("Segoe UI", 11F);
            lblFacultyData.Location = new Point(350, 115);
            lblFacultyData.Name = "lblFacultyData";
            lblFacultyData.Size = new Size(157, 30);
            lblFacultyData.TabIndex = 31;
            lblFacultyData.Text = "[Faculty Name]";
            // 
            // lblSubjectData
            // 
            lblSubjectData.AutoSize = true;
            lblSubjectData.Font = new Font("Segoe UI", 11F);
            lblSubjectData.Location = new Point(40, 115);
            lblSubjectData.Name = "lblSubjectData";
            lblSubjectData.Size = new Size(162, 30);
            lblSubjectData.TabIndex = 32;
            lblSubjectData.Text = "[Subject Name]";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label8.ForeColor = Color.Gray;
            label8.Location = new Point(350, 230);
            label8.Name = "label8";
            label8.Size = new Size(120, 21);
            label8.TabIndex = 33;
            label8.Text = "ALLOWED QTY";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label5.ForeColor = Color.Gray;
            label5.Location = new Point(40, 230);
            label5.Name = "label5";
            label5.Size = new Size(136, 21);
            label5.TabIndex = 34;
            label5.Text = "RECIPE COSTING";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label6.ForeColor = Color.Gray;
            label6.Location = new Point(350, 160);
            label6.Name = "label6";
            label6.Size = new Size(92, 21);
            label6.TabIndex = 35;
            label6.Text = "STUDENTS";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label3.ForeColor = Color.Gray;
            label3.Location = new Point(40, 160);
            label3.Name = "label3";
            label3.Size = new Size(108, 21);
            label3.TabIndex = 36;
            label3.Text = "DATE OF USE";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label4.ForeColor = Color.Gray;
            label4.Location = new Point(350, 93);
            label4.Name = "label4";
            label4.Size = new Size(76, 21);
            label4.TabIndex = 37;
            label4.Text = "FACULTY";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label2.ForeColor = Color.Gray;
            label2.Location = new Point(40, 93);
            label2.Name = "label2";
            label2.Size = new Size(153, 21);
            label2.TabIndex = 38;
            label2.Text = "SUBJECT / COURSE";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 14F);
            label1.Location = new Point(35, 30);
            label1.Name = "label1";
            label1.Size = new Size(212, 38);
            label1.TabIndex = 39;
            label1.Text = "Request Details";
            // 
            // txtRemarks
            // 
            txtRemarks.Location = new Point(183, 557);
            txtRemarks.Name = "txtRemarks";
            txtRemarks.Size = new Size(100, 34);
            txtRemarks.TabIndex = 40;
            // 
            // lblRemarksHeader
            // 
            lblRemarksHeader.Location = new Point(0, 0);
            lblRemarksHeader.Name = "lblRemarksHeader";
            lblRemarksHeader.Size = new Size(100, 23);
            lblRemarksHeader.TabIndex = 41;
            // 
            // lblStatusBadge
            // 
            lblStatusBadge.Location = new Point(253, 41);
            lblStatusBadge.Name = "lblStatusBadge";
            lblStatusBadge.Size = new Size(100, 23);
            lblStatusBadge.TabIndex = 42;
            // 
            // btnTopClose
            // 
            btnTopClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTopClose.FlatAppearance.BorderSize = 0;
            btnTopClose.FlatStyle = FlatStyle.Flat;
            btnTopClose.IconChar = FontAwesome.Sharp.IconChar.Close;
            btnTopClose.IconColor = Color.Gray;
            btnTopClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnTopClose.IconSize = 20;
            btnTopClose.Location = new Point(590, 15);
            btnTopClose.Name = "btnTopClose";
            btnTopClose.Size = new Size(40, 40);
            btnTopClose.TabIndex = 43;
            btnTopClose.Click += btnTopClose_Click;
            // 
            // frmViewRequests
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(640, 692);
            Controls.Add(pnlContent);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmViewRequests";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Request Details";
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bunifuDataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblStatusBadge;
        private System.Windows.Forms.Label lblItemsTitle;
        private System.Windows.Forms.Label lblRemarksHeader;
        private System.Windows.Forms.TextBox txtRemarks;
        private FontAwesome.Sharp.IconButton btnTopClose;
        private System.Windows.Forms.Label label1;

        // Static Headers
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;

        // Data Labels (USE THESE IN LOGIC)
        public System.Windows.Forms.Label lblSubjectData;
        public System.Windows.Forms.Label lblFacultyData;
        public System.Windows.Forms.Label lblDateData;
        public System.Windows.Forms.Label lblStudentData;
        public System.Windows.Forms.Label lblRecipeCostData;
        public System.Windows.Forms.Label lblAllowedQtyData;

        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private FontAwesome.Sharp.IconButton btnApprove;
        private System.Windows.Forms.Button btnReject; // Changed to Standard Button for safety
        private System.Windows.Forms.Button btnClose;
        private Bunifu.UI.WinForms.BunifuDataGridView bunifuDataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIngredient;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRequested;
        private Label labelRequested;
    }
}