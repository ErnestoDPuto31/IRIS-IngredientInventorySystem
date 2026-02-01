namespace IRIS.Presentation.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

        }


        public void LoadPage(UserControl page)
        {
            if (pnlMainContent.Controls.Count > 0)
            {
                pnlMainContent.Controls.Clear();
            }

            page.Dock = DockStyle.Fill;
            pnlMainContent.Controls.Add(page);
            page.BringToFront();
        }
    }
}
