namespace IRIS.Presentation.Forms
{
    partial class Restock
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
            pnlTable = new Guna.UI2.WinForms.Guna2Panel();
            restockTable1 = new IRIS.Presentation.UserControls.Table.RestockTable();
            pnlTable.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTable
            // 
            pnlTable.AutoScroll = true;
            pnlTable.AutoSize = true;
            pnlTable.BorderRadius = 20;
            pnlTable.Controls.Add(restockTable1);
            pnlTable.CustomizableEdges = customizableEdges1;
            pnlTable.Location = new Point(20, 430);
            pnlTable.Name = "pnlTable";
            pnlTable.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlTable.Size = new Size(1512, 400);
            pnlTable.TabIndex = 0;
            pnlTable.Paint += pnlTable_Paint;
            // 
            // restockTable1
            // 
            restockTable1.Dock = DockStyle.Fill;
            restockTable1.Location = new Point(0, 0);
            restockTable1.Name = "restockTable1";
            restockTable1.Size = new Size(1512, 400);
            restockTable1.TabIndex = 0;
            // 
            // Restock
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(1582, 853);
            Controls.Add(pnlTable);
            ForeColor = Color.Red;
            Name = "Restock";
            Padding = new Padding(20);
            Text = "Restock";
            pnlTable.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlTable;
        private UserControls.Table.RestockTable restockTable1;
    }
}