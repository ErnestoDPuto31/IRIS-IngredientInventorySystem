using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Infrastructure.Data;
using IRIS.Presentation.Window_Forms;
using IRIS.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace IRIS.Presentation.UserControls.PagesUC
{
    public partial class RequestControl : UserControl
    {
        private readonly RequestService _requestService;
        private readonly IngredientService _ingredientService;
        private readonly IrisDbContext _context;

        public RequestControl()
        {
            InitializeComponent();

            var optionsBuilder = new DbContextOptionsBuilder<IrisDbContext>();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=IRIS_DB;Trusted_Connection=True;");

            _context = new IrisDbContext(optionsBuilder.Options);
            _requestService = new RequestService(_context);
            var logService = new InventoryLogService(_context);
            _ingredientService = new IngredientService(_context, logService);

            // Hide "New Request" button if not Office Staff
            if (UserSession.CurrentUser.Role != UserRole.OfficeStaff)
            {
                btnNewRequest.Visible = false;
            }

            requestTable.RowActionClicked += (s, requestId) =>
            {
                // Get the Current User's ID from Session
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