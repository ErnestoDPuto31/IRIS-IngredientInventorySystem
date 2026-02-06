namespace IRIS.Presentation.UserControls.Components
{
    partial class SearchBar
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            txtSearchBar = new Guna.UI2.WinForms.Guna2TextBox();
            SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 15;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // txtSearchBar
            // 
            txtSearchBar.AutoSize = true;
            txtSearchBar.BorderColor = SystemColors.ControlDark;
            txtSearchBar.BorderRadius = 15;
            txtSearchBar.BorderThickness = 2;
            txtSearchBar.CustomizableEdges = customizableEdges1;
            txtSearchBar.DefaultText = "";
            txtSearchBar.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtSearchBar.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtSearchBar.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtSearchBar.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtSearchBar.FocusedState.BorderColor = Color.BlueViolet;
            txtSearchBar.Font = new Font("Poppins", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearchBar.HoverState.BorderColor = Color.BlueViolet;
            txtSearchBar.IconLeft = Properties.Resources.icons8_search_100;
            txtSearchBar.IconLeftOffset = new Point(10, 0);
            txtSearchBar.Location = new Point(5, 6);
            txtSearchBar.Margin = new Padding(4, 6, 4, 6);
            txtSearchBar.Name = "txtSearchBar";
            txtSearchBar.PlaceholderText = "Search...";
            txtSearchBar.SelectedText = "";
            txtSearchBar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtSearchBar.Size = new Size(332, 47);
            txtSearchBar.TabIndex = 0;
            txtSearchBar.TextChanged += txtSearchBar_TextChanged;
            // 
            // SearchBar
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(txtSearchBar);
            Name = "SearchBar";
            Size = new Size(341, 60);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2TextBox txtSearchBar;
    }
}
