namespace IRIS.Presentation.UserControls.Components
{
    partial class NotificationDropdown
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
            flpNotifications = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // flpNotifications
            // 
            flpNotifications.AutoScroll = true;
            flpNotifications.Dock = DockStyle.Fill;
            flpNotifications.Location = new Point(0, 0);
            flpNotifications.Name = "flpNotifications";
            flpNotifications.Size = new Size(348, 448);
            flpNotifications.TabIndex = 0;
            // 
            // NotificationDropdown
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(flpNotifications);
            Name = "NotificationDropdown";
            Size = new Size(348, 448);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flpNotifications;
    }
}
