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

            dgvTopIngredients.AutoGenerateColumns = true;

            dgvTopIngredients.DataSource = dataSource;

            dgvTopIngredients.ClearSelection();
        }
    }
}