using IRIS.Presentation.UserControls;
using IRIS.Presentation.Window_Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IRIS.Presentation.Forms
{
    public partial class Inventory : Form
    {
        public Inventory()
        {
            InitializeComponent();
            cmbCategory.Text = "Select Category";
            cmbSortIngredients.Text = "Sort By";
        }

        private void btnAddIngredient_Click(object sender, EventArgs e)
        {
            using (frmAddIngredient form = new frmAddIngredient())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var card = new IngredientCard(form.NewIngredient);
                    card.Margin = new Padding(15);
                    pnlIngredients.Controls.Add(card);
                }
            }
        }
    }
}
