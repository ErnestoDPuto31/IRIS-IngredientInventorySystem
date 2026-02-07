namespace IRIS.Presentation.Forms
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            label1 = new Label();
            label2 = new Label();
            pnlMainContent = new Guna.UI2.WinForms.Guna2Panel();
            requestsTable1 = new IRIS.UI.Controls.RequestsTable();
            materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            labelDate = new Label();
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
            label1.Font = new Font("Poppins", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(103, 87);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(574, 79);
            label1.TabIndex = 2;
            label1.Text = "Requests Management";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Poppins", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlDarkDark;
            label2.Location = new Point(114, 152);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(502, 42);
            label2.TabIndex = 3;
            label2.Text = "Create and manage ingredient requests";
            // 
            // pnlMainContent
            // 
            pnlMainContent.Controls.Add(requestsTable1);
            pnlMainContent.Controls.Add(materialDivider1);
            pnlMainContent.Controls.Add(labelDate);
            pnlMainContent.Controls.Add(label2);
            pnlMainContent.Controls.Add(label1);
            pnlMainContent.CustomizableEdges = customizableEdges1;
            pnlMainContent.Dock = DockStyle.Fill;
            pnlMainContent.Location = new Point(0, 0);
            pnlMainContent.Margin = new Padding(4);
            pnlMainContent.Name = "pnlMainContent";
            pnlMainContent.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlMainContent.ShadowDecoration.Enabled = true;
            pnlMainContent.ShadowDecoration.Shadow = new Padding(10);
            pnlMainContent.Size = new Size(1600, 900);
            pnlMainContent.TabIndex = 5;
            // 
            // requestsTable1
            // 
            requestsTable1.Location = new Point(114, 214);
            requestsTable1.Name = "requestsTable1";
            requestsTable1.Size = new Size(1429, 617);
            requestsTable1.TabIndex = 41;
            // 
            // materialDivider1
            // 
            materialDivider1.BackColor = SystemColors.AppWorkspace;
            materialDivider1.Depth = 0;
            materialDivider1.ForeColor = SystemColors.ControlDarkDark;
            materialDivider1.Location = new Point(114, 74);
            materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            materialDivider1.Name = "materialDivider1";
            materialDivider1.Size = new Size(1496, 1);
            materialDivider1.TabIndex = 40;
            materialDivider1.Text = "materialDivider1";
            // 
            // labelDate
            // 
            labelDate.AutoSize = true;
            labelDate.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelDate.ForeColor = Color.Gray;
            labelDate.Location = new Point(114, 35);
            labelDate.Name = "labelDate";
            labelDate.Size = new Size(108, 21);
            labelDate.TabIndex = 39;
            labelDate.Text = "[DATE TODAY]";
            // 
            // RequestControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(254, 253, 253);
            Controls.Add(pnlMainContent);
            Margin = new Padding(4);
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
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private Label labelDate;
        private UI.Controls.RequestsTable requestsTable1;
    }
}
