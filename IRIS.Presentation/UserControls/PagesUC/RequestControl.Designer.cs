namespace IRIS.Presentation.UserControls.PagesUC
{
    partial class RequestControl
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            label1 = new Label();
            label2 = new Label();
            pnlMainContent = new Guna.UI2.WinForms.Guna2Panel();
            requestTable = new IRIS.Presentation.UserControls.Table.RequestTableUC();
            btnNewRequest = new Guna.UI2.WinForms.Guna2GradientButton();
            pnlMainContent.SuspendLayout();
            SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Poppins", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(118, 28);
            label1.Name = "label1";
            label1.Size = new Size(532, 70);
            label1.TabIndex = 2;
            label1.Text = "REQUESTS MANAGEMENT";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Poppins", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlDarkDark;
            label2.Location = new Point(128, 80);
            label2.Name = "label2";
            label2.Size = new Size(487, 40);
            label2.TabIndex = 3;
            label2.Text = "Create and manage ingredient requests";
            // 
            // pnlMainContent
            // 
            pnlMainContent.Controls.Add(requestTable);
            pnlMainContent.Controls.Add(btnNewRequest);
            pnlMainContent.Controls.Add(label2);
            pnlMainContent.Controls.Add(label1);
            pnlMainContent.CustomizableEdges = customizableEdges3;
            pnlMainContent.Dock = DockStyle.Fill;
            pnlMainContent.Location = new Point(0, 0);
            pnlMainContent.Name = "pnlMainContent";
            pnlMainContent.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pnlMainContent.ShadowDecoration.Enabled = true;
            pnlMainContent.ShadowDecoration.Shadow = new Padding(10);
            pnlMainContent.Size = new Size(1600, 900);
            pnlMainContent.TabIndex = 5;
            // 
            // requestTable
            // 
            requestTable.BackColor = Color.White;
            requestTable.Location = new Point(118, 146);
            requestTable.Name = "requestTable";
            requestTable.Padding = new Padding(25);
            requestTable.Size = new Size(1408, 632);
            requestTable.TabIndex = 5;
            // 
            // btnNewRequest
            // 
            btnNewRequest.BorderRadius = 10;
            btnNewRequest.CustomizableEdges = customizableEdges1;
            btnNewRequest.DisabledState.BorderColor = Color.DarkGray;
            btnNewRequest.DisabledState.CustomBorderColor = Color.DarkGray;
            btnNewRequest.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnNewRequest.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnNewRequest.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnNewRequest.FillColor = Color.FromArgb(77, 10, 133);
            btnNewRequest.FillColor2 = Color.FromArgb(137, 65, 208);
            btnNewRequest.Font = new Font("Poppins", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNewRequest.ForeColor = Color.White;
            btnNewRequest.HoverState.FillColor = Color.FromArgb(95, 20, 161);
            btnNewRequest.HoverState.FillColor2 = Color.FromArgb(155, 86, 226);
            btnNewRequest.Image = Properties.Resources.icons8_add_100;
            btnNewRequest.ImageAlign = HorizontalAlignment.Left;
            btnNewRequest.ImageOffset = new Point(3, 0);
            btnNewRequest.Location = new Point(1327, 51);
            btnNewRequest.Name = "btnNewRequest";
            btnNewRequest.PressedColor = Color.FromArgb(111, 49, 171);
            btnNewRequest.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnNewRequest.Size = new Size(199, 69);
            btnNewRequest.TabIndex = 4;
            btnNewRequest.Text = "New Request";
            btnNewRequest.TextAlign = HorizontalAlignment.Left;
            btnNewRequest.TextOffset = new Point(10, 0);
            btnNewRequest.Click += btnNewRequest_Click;
            // 
            // RequestControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(254, 253, 253);
            Controls.Add(pnlMainContent);
            Name = "RequestControl";
            Size = new Size(1600, 900);
            pnlMainContent.ResumeLayout(false);
            pnlMainContent.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Label label1;
        private Label label2;
        private Guna.UI2.WinForms.Guna2Panel pnlMainContent;
        private Guna.UI2.WinForms.Guna2GradientButton btnNewRequest;
        private UserControls.Table.RequestTableUC requestTableuc1;
        private UserControls.Table.RequestTableUC requestTable;
    }
}
