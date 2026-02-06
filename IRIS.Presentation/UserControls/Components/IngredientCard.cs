using IRIS.Domain.Entities;
using IRIS.Presentation.Window_Forms;

namespace IRIS.Presentation.UserControls
{
    public partial class IngredientCard : UserControl
    {
        public Ingredient IngredientData { get; private set; }
        public IngredientCard(Ingredient data)
        {
            InitializeComponent();

            this.Dock = DockStyle.None;
            this.IngredientData = data;
            this.DoubleBuffered = true;
            this.Margin = new Padding(35);
            SetupCard();
        }

        private void SetupCard()
        {
            if (IngredientData == null) return;

            lblIngredientName.Text = IngredientData.Name;
            txtCategoryLabel.Text = IngredientData.Category;

            string unit = string.IsNullOrEmpty(IngredientData.Unit) ? "g" : IngredientData.Unit;

            lblCurrentStock.Text = $"{IngredientData.CurrentStock} {unit}";
            lblMinThreshold.Text = $"{IngredientData.MinimumStock} {unit}";

            lblUpdatedAt.Text = $"Updated At {DateTime.Now.ToString("g")}";

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

            ApplyCategoryColor(IngredientData.Category);
        }

        private void ApplyCategoryColor(string category)
        {
            Color textColor;
            Color backColor;

            switch (category.ToLower().Trim())
            {
                case "produce":
                    textColor = Color.FromArgb(39, 174, 96);   // Emerald Green
                    backColor = Color.FromArgb(232, 248, 241); // Very Light Green
                    break;
                case "protein":
                    textColor = Color.FromArgb(231, 76, 60);   // Soft Red
                    backColor = Color.FromArgb(253, 237, 236); // Very Light Red
                    break;
                case "dairy & eggs":
                    textColor = Color.FromArgb(243, 156, 18);  // Deep Yellow/Orange
                    backColor = Color.FromArgb(254, 245, 231); // Very Light Yellow
                    break;
                case "pantry staples":
                    textColor = Color.FromArgb(127, 140, 141); // Slate Gray
                    backColor = Color.FromArgb(242, 244, 244); // Very Light Gray
                    break;
                case "spices & seasonings":
                    textColor = Color.FromArgb(211, 84, 0);    // Burnt Orange
                    backColor = Color.FromArgb(251, 238, 230); // Very Light Orange
                    break;
                case "condiments & oils":
                    textColor = Color.FromArgb(160, 64, 0);    // Sienna Brown
                    backColor = Color.FromArgb(246, 221, 204); // Light Tan
                    break;
                case "grains & legumes":
                    textColor = Color.FromArgb(184, 134, 11);  // Dark Gold
                    backColor = Color.FromArgb(252, 243, 207); // Light Gold
                    break;
                case "bakery & sweets":
                    textColor = Color.FromArgb(253, 121, 168); // Soft Pink
                    backColor = Color.FromArgb(255, 240, 245); // Very Light Pink
                    break;
                case "beverages":
                    textColor = Color.FromArgb(52, 152, 219);  // Bright Blue
                    backColor = Color.FromArgb(235, 245, 251); // Very Light Blue
                    break;
                case "frozen & prepared":
                    textColor = Color.FromArgb(142, 68, 173);  // Amethyst Purple
                    backColor = Color.FromArgb(245, 238, 248); // Very Light Purple
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
            using (var form = new frmAddIngredient(this.IngredientData))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    this.IngredientData = form.NewIngredient;
                    SetupCard();
                }
            }
        }

        private void btnDeleteIngredient_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
            this.Dispose();
        }
    }
}
