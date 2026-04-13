using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Presentation.Window_Forms;
using IRIS.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection; 
using System;
using System.Windows.Forms;

namespace IRIS.Presentation.UserControls.PagesUC
{
    public partial class RequestControl : UserControl
    {
        private readonly IServiceScope _scope;
        private readonly IRequestService _requestService;
        private readonly IIngredientService _ingredientService;

        public RequestControl()
        {
            InitializeComponent();

            // 1. Create a scope and pull services from Program.cs
            _scope = Program.Services.CreateScope();
            _requestService = _scope.ServiceProvider.GetRequiredService<IRequestService>();
            _ingredientService = _scope.ServiceProvider.GetRequiredService<IIngredientService>();


            if (UserSession.CurrentUser.Role != UserRole.OfficeStaff)
            {
                btnNewRequest.Visible = false;
            }

            requestTable.RowActionClicked += (s, requestId) =>
            {

                int currentUserId = UserSession.CurrentUser.UserId;

                using (var form = new frmViewRequests(requestId, _requestService, currentUserId))
                {
                    form.StartPosition = FormStartPosition.CenterParent;
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        requestTable.LoadData();
                    }
                }
            };
        }

        private void btnNewRequest_Click(object sender, EventArgs e)
        {
            using (var form = new frmRequestEntry(_requestService, _ingredientService))
            {
                form.StartPosition = FormStartPosition.CenterParent;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    requestTable.LoadData();
                }
                Refresh();
            }
        }
    }
}