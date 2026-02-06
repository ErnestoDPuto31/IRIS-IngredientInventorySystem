namespace IRIS.Presentation.Window_Forms
{
    partial class restockIngredients
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblIngredientName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            currentlbl = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel5 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(12, 21);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(181, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Restock Ingredient";
            // 
            // lblIngredientName
            // 
            lblIngredientName.BackColor = Color.Transparent;
            lblIngredientName.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIngredientName.Location = new Point(12, 57);
            lblIngredientName.Name = "lblIngredientName";
            lblIngredientName.Size = new Size(151, 25);
            lblIngredientName.TabIndex = 1;
            lblIngredientName.Text = "[Ingredient Name]";
            // 
            // guna2Panel1
            // 
            guna2Panel1.Controls.Add(guna2HtmlLabel5);
            guna2Panel1.Controls.Add(guna2HtmlLabel4);
            guna2Panel1.Controls.Add(guna2HtmlLabel3);
            guna2Panel1.Controls.Add(guna2HtmlLabel2);
            guna2Panel1.Controls.Add(guna2HtmlLabel1);
            guna2Panel1.Controls.Add(currentlbl);
            guna2Panel1.CustomizableEdges = customizableEdges1;
            guna2Panel1.Location = new Point(12, 122);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel1.Size = new Size(558, 125);
            guna2Panel1.TabIndex = 2;
            // 
            // currentlbl
            // 
            currentlbl.BackColor = Color.Transparent;
            currentlbl.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            currentlbl.ForeColor = SystemColors.ControlDarkDark;
            currentlbl.Location = new Point(20, 15);
            currentlbl.Name = "currentlbl";
            currentlbl.Size = new Size(119, 25);
            currentlbl.TabIndex = 3;
            currentlbl.Text = "Current Stock: ";
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.ForeColor = SystemColors.ControlDarkDark;
            guna2HtmlLabel1.Location = new Point(20, 49);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(123, 25);
            guna2HtmlLabel1.TabIndex = 4;
            guna2HtmlLabel1.Text = "Min Threshold:";
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel2.ForeColor = SystemColors.ControlDarkDark;
            guna2HtmlLabel2.Location = new Point(20, 80);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(160, 25);
            guna2HtmlLabel2.TabIndex = 5;
            guna2HtmlLabel2.Text = "Suggested Restock:";
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.BackColor = Color.Transparent;
            guna2HtmlLabel3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel3.ForeColor = SystemColors.ControlDarkDark;
            guna2HtmlLabel3.Location = new Point(400, 15);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(38, 25);
            guna2HtmlLabel3.TabIndex = 6;
            guna2HtmlLabel3.Text = "0.00";
            // 
            // guna2HtmlLabel4
            // 
            guna2HtmlLabel4.BackColor = Color.Transparent;
            guna2HtmlLabel4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel4.ForeColor = SystemColors.ControlDarkDark;
            guna2HtmlLabel4.Location = new Point(400, 45);
            guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            guna2HtmlLabel4.Size = new Size(38, 25);
            guna2HtmlLabel4.TabIndex = 7;
            guna2HtmlLabel4.Text = "0.00";
            // 
            // guna2HtmlLabel5
            // 
            guna2HtmlLabel5.BackColor = Color.Transparent;
            guna2HtmlLabel5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel5.ForeColor = SystemColors.ControlDarkDark;
            guna2HtmlLabel5.Location = new Point(400, 80);
            guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            guna2HtmlLabel5.Size = new Size(38, 25);
            guna2HtmlLabel5.TabIndex = 8;
            guna2HtmlLabel5.Text = "0.00";
            // 
            // restockIngredients
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 653);
            Controls.Add(guna2Panel1);
            Controls.Add(lblIngredientName);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.None;
            Name = "restockIngredients";
            Text = "restockIngredients";
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblIngredientName;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel currentlbl;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel5;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
    }
}