namespace IRIS.Presentation.UserControls.PagesUC
{
    partial class DashboardControl
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
            dashboardCardTotalIngredients = new IRIS.Presentation.UserControls.Components.DashboardCard();
            dashboardCardLowStock = new IRIS.Presentation.UserControls.Components.DashboardCard();
            dashboardCardPending = new IRIS.Presentation.UserControls.Components.DashboardCard();
            lblWelcomeDescription = new Label();
            lblWelcomeText = new Label();
            statusOverviewStock = new IRIS.Presentation.UserControls.Components.StatusOverviewCard();
            statusOverviewRequests = new IRIS.Presentation.UserControls.Components.StatusOverviewCard();
            guna2ImageButton1 = new Guna.UI2.WinForms.Guna2ImageButton();
            label1 = new Label();
            dashboardCardApproved = new IRIS.Presentation.UserControls.Components.DashboardCard();
            flowLayoutPanelAlerts = new FlowLayoutPanel();
            dashboardAlertItem1 = new IRIS.Presentation.UserControls.Components.DashboardAlertItem();
            dashboardAlertItem2 = new IRIS.Presentation.UserControls.Components.DashboardAlertItem();
            dashboardAlertItem3 = new IRIS.Presentation.UserControls.Components.DashboardAlertItem();
            dashboardAlertItem4 = new IRIS.Presentation.UserControls.Components.DashboardAlertItem();
            flowLayoutPanelAlerts.SuspendLayout();
            SuspendLayout();
            // 
            // dashboardCardTotalIngredients
            // 
            dashboardCardTotalIngredients.BackColor = Color.Transparent;
            dashboardCardTotalIngredients.Location = new Point(75, 146);
            dashboardCardTotalIngredients.Name = "dashboardCardTotalIngredients";
            dashboardCardTotalIngredients.Padding = new Padding(10);
            dashboardCardTotalIngredients.Size = new Size(360, 170);
            dashboardCardTotalIngredients.TabIndex = 0;
            // 
            // dashboardCardLowStock
            // 
            dashboardCardLowStock.BackColor = Color.Transparent;
            dashboardCardLowStock.Location = new Point(441, 146);
            dashboardCardLowStock.Name = "dashboardCardLowStock";
            dashboardCardLowStock.Padding = new Padding(10);
            dashboardCardLowStock.Size = new Size(360, 168);
            dashboardCardLowStock.TabIndex = 1;
            // 
            // dashboardCardPending
            // 
            dashboardCardPending.BackColor = Color.Transparent;
            dashboardCardPending.Location = new Point(807, 144);
            dashboardCardPending.Name = "dashboardCardPending";
            dashboardCardPending.Padding = new Padding(10);
            dashboardCardPending.Size = new Size(360, 170);
            dashboardCardPending.TabIndex = 2;
            // 
            // lblWelcomeDescription
            // 
            lblWelcomeDescription.AutoSize = true;
            lblWelcomeDescription.Font = new Font("Poppins", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblWelcomeDescription.ForeColor = SystemColors.ControlDarkDark;
            lblWelcomeDescription.Location = new Point(97, 98);
            lblWelcomeDescription.Name = "lblWelcomeDescription";
            lblWelcomeDescription.Size = new Size(533, 40);
            lblWelcomeDescription.TabIndex = 7;
            lblWelcomeDescription.Text = "View all inventory transactions and changes";
            // 
            // lblWelcomeText
            // 
            lblWelcomeText.AutoSize = true;
            lblWelcomeText.Font = new Font("Poppins", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWelcomeText.Location = new Point(87, 46);
            lblWelcomeText.Name = "lblWelcomeText";
            lblWelcomeText.Size = new Size(277, 70);
            lblWelcomeText.TabIndex = 6;
            lblWelcomeText.Text = "Welcome, ...";
            // 
            // statusOverviewStock
            // 
            statusOverviewStock.BackColor = Color.Transparent;
            statusOverviewStock.Location = new Point(101, 698);
            statusOverviewStock.Name = "statusOverviewStock";
            statusOverviewStock.Size = new Size(732, 225);
            statusOverviewStock.TabIndex = 9;
            // 
            // statusOverviewRequests
            // 
            statusOverviewRequests.BackColor = Color.Transparent;
            statusOverviewRequests.Location = new Point(839, 698);
            statusOverviewRequests.Name = "statusOverviewRequests";
            statusOverviewRequests.Size = new Size(698, 225);
            statusOverviewRequests.TabIndex = 10;
            statusOverviewRequests.WidgetType = Components.OverviewWidgetType.Requests;
            // 
            // guna2ImageButton1
            // 
            guna2ImageButton1.CheckedState.ImageSize = new Size(64, 64);
            guna2ImageButton1.HoverState.ImageSize = new Size(40, 40);
            guna2ImageButton1.Image = Properties.Resources.icons8_medium_risk_50;
            guna2ImageButton1.ImageOffset = new Point(0, 0);
            guna2ImageButton1.ImageRotate = 0F;
            guna2ImageButton1.ImageSize = new Size(40, 40);
            guna2ImageButton1.Location = new Point(90, 331);
            guna2ImageButton1.Name = "guna2ImageButton1";
            guna2ImageButton1.PressedState.ImageSize = new Size(440, 40);
            guna2ImageButton1.ShadowDecoration.CustomizableEdges = customizableEdges1;
            guna2ImageButton1.Size = new Size(40, 40);
            guna2ImageButton1.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Poppins", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(136, 334);
            label1.Name = "label1";
            label1.Size = new Size(150, 36);
            label1.TabIndex = 12;
            label1.Text = "Recent Alerts";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dashboardCardApproved
            // 
            dashboardCardApproved.BackColor = Color.Transparent;
            dashboardCardApproved.Location = new Point(1173, 144);
            dashboardCardApproved.Name = "dashboardCardApproved";
            dashboardCardApproved.Padding = new Padding(10);
            dashboardCardApproved.Size = new Size(360, 170);
            dashboardCardApproved.TabIndex = 3;
            // 
            // flowLayoutPanelAlerts
            // 
            flowLayoutPanelAlerts.Controls.Add(dashboardAlertItem1);
            flowLayoutPanelAlerts.Controls.Add(dashboardAlertItem2);
            flowLayoutPanelAlerts.Controls.Add(dashboardAlertItem3);
            flowLayoutPanelAlerts.Controls.Add(dashboardAlertItem4);
            flowLayoutPanelAlerts.Location = new Point(90, 377);
            flowLayoutPanelAlerts.Name = "flowLayoutPanelAlerts";
            flowLayoutPanelAlerts.Padding = new Padding(5);
            flowLayoutPanelAlerts.Size = new Size(1443, 315);
            flowLayoutPanelAlerts.TabIndex = 13;
            // 
            // dashboardAlertItem1
            // 
            dashboardAlertItem1.BackColor = Color.Transparent;
            dashboardAlertItem1.Location = new Point(8, 8);
            dashboardAlertItem1.Name = "dashboardAlertItem1";
            dashboardAlertItem1.Size = new Size(1425, 75);
            dashboardAlertItem1.TabIndex = 0;
            // 
            // dashboardAlertItem2
            // 
            dashboardAlertItem2.BackColor = Color.Transparent;
            dashboardAlertItem2.Location = new Point(8, 89);
            dashboardAlertItem2.Name = "dashboardAlertItem2";
            dashboardAlertItem2.Size = new Size(1425, 75);
            dashboardAlertItem2.TabIndex = 1;
            // 
            // dashboardAlertItem3
            // 
            dashboardAlertItem3.BackColor = Color.Transparent;
            dashboardAlertItem3.Location = new Point(8, 170);
            dashboardAlertItem3.Name = "dashboardAlertItem3";
            dashboardAlertItem3.Size = new Size(1425, 75);
            dashboardAlertItem3.TabIndex = 2;
            // 
            // dashboardAlertItem4
            // 
            dashboardAlertItem4.BackColor = Color.Transparent;
            dashboardAlertItem4.Location = new Point(8, 251);
            dashboardAlertItem4.Name = "dashboardAlertItem4";
            dashboardAlertItem4.Size = new Size(1425, 75);
            dashboardAlertItem4.TabIndex = 3;
            // 
            // DashboardControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.White;
            Controls.Add(flowLayoutPanelAlerts);
            Controls.Add(label1);
            Controls.Add(guna2ImageButton1);
            Controls.Add(statusOverviewRequests);
            Controls.Add(statusOverviewStock);
            Controls.Add(lblWelcomeDescription);
            Controls.Add(lblWelcomeText);
            Controls.Add(dashboardCardApproved);
            Controls.Add(dashboardCardPending);
            Controls.Add(dashboardCardLowStock);
            Controls.Add(dashboardCardTotalIngredients);
            Name = "DashboardControl";
            Padding = new Padding(0, 0, 40, 0);
            Size = new Size(1516, 879);
            flowLayoutPanelAlerts.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Components.DashboardCard dashboardCardTotalIngredients;
        private Components.DashboardCard dashboardCardLowStock;
        private Components.DashboardCard dashboardCardPending;
        private Label lblWelcomeDescription;
        private Label lblWelcomeText;
        private Components.StatusOverviewCard statusOverviewStock;
        private Components.StatusOverviewCard statusOverviewRequests;
        private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton1;
        private Label label1;
        private Components.DashboardCard dashboardCardApproved;
        private FlowLayoutPanel flowLayoutPanelAlerts;
        private Components.DashboardAlertItem dashboardAlertItem1;
        private Components.DashboardAlertItem dashboardAlertItem2;
        private Components.DashboardAlertItem dashboardAlertItem3;
        private Components.DashboardAlertItem dashboardAlertItem4;
    }
}
