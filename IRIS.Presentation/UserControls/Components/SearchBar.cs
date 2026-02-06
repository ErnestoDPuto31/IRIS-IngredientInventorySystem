using System.ComponentModel;

namespace IRIS.Presentation.UserControls.Components
{
    public partial class SearchBar : UserControl
    {
        public event EventHandler<string> SearchTextChanged;

        public SearchBar()
        {
            InitializeComponent();
            this.txtSearchBar.TextChanged += txtSearchBar_TextChanged;
        }

        [Browsable(true)]
        [Category("Behavior")]
        [Description("The text currently in the search bar.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string SearchText
        {
            get { return txtSearchBar.Text; }
            set { txtSearchBar.Text = value; }
        }

        private void txtSearchBar_TextChanged(object sender, EventArgs e)
        {
            SearchTextChanged?.Invoke(this, txtSearchBar.Text);
        }

public void Clear()
        {
            txtSearchBar.Clear();
        }
    }
}