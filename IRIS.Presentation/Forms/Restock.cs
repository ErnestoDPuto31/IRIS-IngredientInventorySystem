using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IRIS.Presentation.Forms
{
    public partial class Restock : Form
    {
        public Restock()
        {
            InitializeComponent();
            SetRoundedPanel(pnlTable, 30);

            pnlTable.Resize += (s, e) => SetRoundedPanel(pnlTable, 30);

        }
       

        // Handle the click
        private void Card_Click(object sender, EventArgs e)
        {
            // Retrieve the ID
            Control source = (Control)sender;
            if (source.Tag != null)
            {
                int ingredientId = (int)source.Tag;
                MessageBox.Show($"You clicked Ingredient ID: {ingredientId}");
                // Here you can open a specific restock dialog or detail view
            }
        }
        private void SetRoundedPanel(Panel panel, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.StartFigure();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(panel.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(panel.Width - radius, panel.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, panel.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            panel.Region = new Region(path);
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlTable_Paint(object sender, PaintEventArgs e)
        {
            // Enable smooth lines
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create a rounded rectangle path slightly smaller than the panel so the border fits
            int radius = 30;
            Rectangle rect = new Rectangle(0, 0, pnlTable.Width - 1, pnlTable.Height - 1);

            using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();

                // Draw a thin grey border
                using (Pen pen = new Pen(Color.LightGray, 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }
    }
}
