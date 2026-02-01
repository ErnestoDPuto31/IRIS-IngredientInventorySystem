using IRIS.Domain.Entities;

namespace IRIS.Presentation.Window_Forms
{
    public partial class frmAddIngredient : Form
    {
        public Ingredient NewIngredient { get; private set; }

        public frmAddIngredient()
        {
            InitializeComponent();

            cmbCategory.Text = "Select Category";
            cmbCategory.SelectedIndex = 0;

            cmbUnit.Text = "Select Unit";
            cmbUnit.SelectedIndex = 0;

            btnExitForm.Click += (s, e) => this.Close();
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
        }

        private void btnAddIngredient_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIngredientName.Text))
            {
                MessageBox.Show("Please enter an ingredient name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NewIngredient = new Ingredient
            {
                Name = txtIngredientName.Text.Trim(),
                Category = cmbCategory.SelectedItem?.ToString() ?? string.Empty,
                Unit = cmbUnit.SelectedItem?.ToString() ?? string.Empty,
                CurrentStock = numCurrentStock.Value,
                MinimumStock = numMinimumThreshold.Value,
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
