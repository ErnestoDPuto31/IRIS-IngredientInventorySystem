using Microsoft.EntityFrameworkCore;
using IRIS.Infrastructure.Data;
using IRIS.Services.Implementations;
using IRIS.Presentation.Window_Forms;

namespace IRIS.Presentation.Forms
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
            _ingredientService = new IngredientService(_context);
        }

        private void btnNewRequest_Click(object sender, EventArgs e)
        {
            using (var form = new frmRequestEntry(_requestService, _ingredientService))
            {
                form.StartPosition = FormStartPosition.CenterParent;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    requestTableuc1.LoadData();
                }
            }
        }
    }
}