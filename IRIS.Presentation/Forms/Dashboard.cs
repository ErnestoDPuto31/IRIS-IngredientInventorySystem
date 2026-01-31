using IRIS.Domain.Entities;
using IRIS.Domain.Enums;

namespace IRIS.Presentation.Forms
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            ApplyRolePermissions();
            LoadView(new DashboardControl());
        }

        private void ApplyRolePermissions()
        {
            var role = UserSession.CurrentUser.Role;

            btnDashboard.Visible = true;                            // Dashboard is visible to all roles
            btnHistory.Visible = true;                              // History is visible to all roles
            btnRestock.Visible = (role == UserRole.OfficeStaff);    // Restock is visible to OfficeStaff only
            btnInventory.Visible = (role == UserRole.OfficeStaff);  // Inventory is visible to OfficeStaff only
            btnRequests.Visible = (role == UserRole.OfficeStaff 
                || role == UserRole.AssistantDean
                || role == UserRole.Dean);                          // Requests is visible to OfficeStaff, AssistantDean, and Dean
            btnRequests.Visible = true;                             // Requests is visible to all roles
        }
        
        private void LoadView(UserControl userControl)
        {
            MainContentPanel.Controls.Clear();
            userControl.Dock = DockStyle.Fill;
            MainContentPanel.Controls.Add(userControl);
        }
    }
}
