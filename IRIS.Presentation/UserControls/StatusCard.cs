using System;
using System.Drawing;
using System.Windows.Forms;
using IRIS.Domain.Enums; // Siguraduhin na nandito ang StockStatus enum mo

namespace IRIS.Presentation.UserControls
{
    public partial class StatusCard : UserControl
    {
        // Properties na lalabas sa "Properties Window" ng Visual Studio
        public string Title { get; set; } = "STOCK STATUS";
        public int Count { get; set; } = 0;
        public StockStatus TargetStatus { get; set; } 

            public StatusCard()
        {
            InitializeComponent();
            this.Size = new Size(250, 100); // Fixed size
            this.BackColor = Color.White;
        }

        // Method para i-update ang itsura ng card base sa data
        public void UpdateData(int newCount)
        {
            this.Count = newCount;
            lblCount.Text = Count.ToString();
            lblTitle.Text = Title;

            // Logic para sa kulay base sa TargetStatus
            switch (TargetStatus)
            {
                case StockStatus.Empty:
                    lblCount.ForeColor = Color.Crimson;
                    pnlIndicator.BackColor = Color.Crimson;
                    break;
                case StockStatus.LowStock:
                    lblCount.ForeColor = Color.Goldenrod;
                    pnlIndicator.BackColor = Color.Goldenrod;
                    break;
                case StockStatus.WellStocked:
                    lblCount.ForeColor = Color.SeaGreen;
                    pnlIndicator.BackColor = Color.SeaGreen;
                    break;
            }
        }

        // Para sa magandang border (Optional)
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.FromArgb(230, 230, 230), ButtonBorderStyle.Solid);
        }
    }
}