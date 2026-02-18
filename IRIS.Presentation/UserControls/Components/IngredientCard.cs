using IRIS.Domain.Entities;

namespace IRIS.Presentation.UserControls
{
    public partial class IngredientCard : UserControl
    {
        public Ingredient IngredientData { get; private set; }
        public event EventHandler<Ingredient> EditClicked;
        public event EventHandler<int> DeleteClicked;

        public IngredientCard(Ingredient data)
        {
            InitializeComponent();

            this.Dock = DockStyle.None;
            this.IngredientData = data;
            this.DoubleBuffered = true;
            this.Margin = new Padding(35);
            SetupCard();
        }
        public void UpdateData(Ingredient newData)
        {
            this.IngredientData = newData;
            SetupCard();
        }

        private void SetupCard()
        {
            if (IngredientData == null) return;

            lblIngredientName.Text = IngredientData.Name;
            txtCategoryLabel.Text = IngredientData.Category.ToString();

            string unit = string.IsNullOrEmpty(IngredientData.Unit) ? "g" : IngredientData.Unit;

            lblCurrentStock.Text = $"{IngredientData.CurrentStock} {unit}";
            lblMinThreshold.Text = $"{IngredientData.MinimumStock} {unit}";

            if (IngredientData.UpdatedAt == DateTime.MinValue || IngredientData.UpdatedAt.Year == 1)
            {
                lblUpdatedAt.Text = "Newly Added";
            }
            else
            {
                lblUpdatedAt.Visible = true;
                lblUpdatedAt.Text = $"Updated: {IngredientData.UpdatedAt.ToString("MMM dd, yyyy h:mm tt")}";
            }

            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            double maxRange = (double)IngredientData.MinimumStock * 2.5;
            if (maxRange <= 0) maxRange = 100;

            int percentage = (int)((double)IngredientData.CurrentStock / maxRange * 100);
            guna2ProgressBar1.Value = Math.Clamp(percentage, 5, 100);

            Color statusColor;
            string statusText;

            if (IngredientData.CurrentStock <= 0)
            {
                statusText = "Empty";
                statusColor = Color.FromArgb(192, 57, 43);
            }
            else if (IngredientData.CurrentStock <= IngredientData.MinimumStock)
            {
                statusText = "Low";
                statusColor = Color.FromArgb(231, 76, 60);
            }
            else
            {
                statusText = "Full";
                statusColor = Color.FromArgb(46, 204, 113);
            }

            txtStatus.Text = statusText;
            txtStatus.FillColor = statusColor;
            txtStatus.ForeColor = Color.White;
            txtStatus.BorderThickness = 0;
            guna2ProgressBar1.ProgressColor = statusColor;
            guna2ProgressBar1.ProgressColor2 = statusColor;
            ApplyCategoryColor(IngredientData.Category.ToString());
        }

        private void ApplyCategoryColor(string category)
        {
            Color textColor;
            Color backColor;

            switch (category.ToLower().Trim())
            {
                case "produce":
                    textColor = Color.FromArgb(39, 174, 96);
                    backColor = Color.FromArgb(232, 248, 241);
                    break;
                case "protein":
                    textColor = Color.FromArgb(231, 76, 60);
                    backColor = Color.FromArgb(253, 237, 236);
                    break;
                case "dairy & eggs":
                    textColor = Color.FromArgb(243, 156, 18);
                    backColor = Color.FromArgb(254, 245, 231);
                    break;
                case "pantry staples":
                    textColor = Color.FromArgb(127, 140, 141);
                    backColor = Color.FromArgb(242, 244, 244);
                    break;
                case "spices & seasonings":
                    textColor = Color.FromArgb(211, 84, 0);
                    backColor = Color.FromArgb(251, 238, 230);
                    break;
                case "condiments & oils":
                    textColor = Color.FromArgb(160, 64, 0);
                    backColor = Color.FromArgb(246, 221, 204);
                    break;
                case "grains & legumes":
                    textColor = Color.FromArgb(184, 134, 11);
                    backColor = Color.FromArgb(252, 243, 207);
                    break;
                case "bakery & sweets":
                    textColor = Color.FromArgb(253, 121, 168);
                    backColor = Color.FromArgb(255, 240, 245);
                    break;
                case "beverages":
                    textColor = Color.FromArgb(52, 152, 219);
                    backColor = Color.FromArgb(235, 245, 251);
                    break;
                case "frozen & prepared":
                    textColor = Color.FromArgb(142, 68, 173);
                    backColor = Color.FromArgb(245, 238, 248);
                    break;
                default:
                    textColor = Color.Gray;
                    backColor = Color.FromArgb(240, 240, 240);
                    break;
            }
            txtCategoryLabel.Text = category;
            txtCategoryLabel.ForeColor = textColor;
            txtCategoryLabel.FillColor = backColor;
        }


        private void btnEditIngredient_Click(object sender, EventArgs e)
        {
            EditClicked?.Invoke(this, this.IngredientData);
        }

        private void btnDeleteIngredient_Click(object sender, EventArgs e)
        {
            DeleteClicked?.Invoke(this, this.IngredientData.IngredientId);
        }

        internal void HideActionButtons() {
            if (btnEditIngredient != null) btnEditIngredient.Visible = false;
            if (btnDeleteIngredient != null) btnDeleteIngredient.Visible = false;
        }
    }
}