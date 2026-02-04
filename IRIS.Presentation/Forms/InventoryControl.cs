using IRIS.Presentation.UserControls;
using IRIS.Presentation.Window_Forms;

namespace IRIS.Presentation.Forms
{
    public partial class InventoryControl : UserControl
    {
        // We create this programmatically to fix the layout issue
        private FlowLayoutPanel _ingredientsGrid;

        public InventoryControl()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            cmbCategory.Text = "Select Category";
            cmbSortIngredients.Text = "Sort By";

            _ingredientsGrid = new FlowLayoutPanel();
            _ingredientsGrid.Dock = DockStyle.Fill;
            _ingredientsGrid.FlowDirection = FlowDirection.LeftToRight;
            _ingredientsGrid.WrapContents = true;
            _ingredientsGrid.AutoScroll = true; 
            _ingredientsGrid.BackColor = Color.Transparent;

            pnlIngredients.Controls.Clear();
            pnlIngredients.Controls.Add(_ingredientsGrid);
        }

        private void btnAddIngredient_Click(object sender, EventArgs e)
        {
            using (frmAddIngredient form = new frmAddIngredient())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var card = new IngredientCard(form.NewIngredient);

                    card.Margin = new Padding(10);
                    _ingredientsGrid.Controls.Add(card);
                }
            }
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            if (pnlMainContent != null)
            {
                pnlMainContent.Dock = DockStyle.Fill;
                pnlMainContent.SendToBack();
            }
        }
    }
}