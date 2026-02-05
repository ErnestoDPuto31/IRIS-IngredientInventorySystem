namespace IRIS.Presentation.UserControls.Components
{
    partial class StatusCardUC
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlLowStock = new Guna.UI2.WinForms.Guna2Panel();
            SuspendLayout();
            // 
            // pnlLowStock
            // 
            pnlLowStock.BorderRadius = 30;
            pnlLowStock.CustomizableEdges = customizableEdges1;
            pnlLowStock.Dock = DockStyle.Fill;
            pnlLowStock.Location = new Point(0, 0);
            pnlLowStock.Name = "pnlLowStock";
            pnlLowStock.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlLowStock.Size = new Size(400, 200);
            pnlLowStock.TabIndex = 0;
            // 
            // StatusCardUC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlLowStock);
            Name = "StatusCardUC";
            Size = new Size(400, 200);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlLowStock;
    }
}
