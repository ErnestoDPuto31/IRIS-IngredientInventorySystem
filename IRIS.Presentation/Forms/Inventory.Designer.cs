namespace IRIS.Presentation.Forms
{
    partial class Inventory
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
            navigationPanel1 = new IRIS.Presentation.components.navigator.NavigationPanel();
            SuspendLayout();
            // 
            // navigationPanel1
            // 
            navigationPanel1.BackColor = Color.Indigo;
            navigationPanel1.Dock = DockStyle.Left;
            navigationPanel1.Location = new Point(0, 0);
            navigationPanel1.Name = "navigationPanel1";
            navigationPanel1.Size = new Size(75, 673);
            navigationPanel1.TabIndex = 0;
            // 
            // Inventory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 673);
            Controls.Add(navigationPanel1);
            Name = "Inventory";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inventory";
            ResumeLayout(false);
        }

        #endregion

        private components.navigator.NavigationPanel navigationPanel1;
    }
}