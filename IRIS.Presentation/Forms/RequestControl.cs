using System;
using System.Windows.Forms;

namespace IRIS.Presentation.Forms
{
    public partial class RequestControl : UserControl
    {
        public RequestControl()
        {
            InitializeComponent();

            // Setup the view immediately
            SetupView();
        }

        private void SetupView()
        {
            // 1. Set the Date Label (safely)
            if (labelDate != null)
            {
                labelDate.Text = DateTime.Now.ToString("D"); // e.g., "Monday, June 15, 2009"
            }
        
            if (requestsTable1 != null)
            {
                requestsTable1.LoadRequests();
            }
        }
    }
}