using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IRIS.Presentation.UserControls.Components
{
    public partial class TopIngredientsTable : UserControl
    {
        public TopIngredientsTable()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Binds the provided data to the DataGridView.
        /// </summary>
        /// <param name="dataSource">The list or collection of top ingredients.</param>
        public void LoadData(object dataSource)
        {
            if (dgvTopIngredients.InvokeRequired)
            {
                dgvTopIngredients.Invoke(new Action(() => LoadDataCore(dataSource)));
            }
            else
            {
                LoadDataCore(dataSource);
            }
        }

        private void LoadDataCore(object dataSource)
        {
            dgvTopIngredients.DataSource = null; // Clear existing bindings
            dgvTopIngredients.DataSource = dataSource;

            // Optional: If you want to rename headers explicitly, you can do it here.
            // Example assuming your DTO has properties like "IngredientName", "Category", and "UsageCount":
            // if (dgvTopIngredients.Columns["IngredientName"] != null)
            //     dgvTopIngredients.Columns["IngredientName"].HeaderText = "Ingredient Name";

            // if (dgvTopIngredients.Columns["UsageCount"] != null)
            //     dgvTopIngredients.Columns["UsageCount"].HeaderText = "Times Used";

            dgvTopIngredients.ClearSelection();
        }
    }
}