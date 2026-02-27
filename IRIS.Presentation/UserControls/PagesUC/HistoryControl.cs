using Microsoft.EntityFrameworkCore; 
using IRIS.Infrastructure.Data;
using IRIS.Services.Interfaces;
using IRIS.Services.Implementations;

namespace IRIS.Presentation.UserControls.PagesUC
{
    public partial class HistoryControl : UserControl
    {
        private IInventoryLogService _logService;
        private IrisDbContext _context;

        private int _currentPage = 1;
        private const int _pageSize = 10;
        private int _totalPages = 1;
        private string _currentSearchTerm = "";

        public HistoryControl()
        {
            InitializeComponent();

            var optionsBuilder = new DbContextOptionsBuilder<IrisDbContext>();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=IRIS_DB;Trusted_Connection=True;");
            _context = new IrisDbContext(optionsBuilder.Options);

            _logService = new InventoryLogService(_context);
            historyTableuc1.OnSearchChanged += HistoryTable_OnSearchChanged;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                LoadPage();
            }
        }

        private void HistoryTable_OnSearchChanged(object sender, string searchText)
        {
            _currentSearchTerm = searchText;
            _currentPage = 1;
            LoadPage();
        }

        private void LoadPage()
        {
            int totalLogs = _logService.GetTotalLogCount(_currentSearchTerm);
            _totalPages = (int)Math.Ceiling((double)totalLogs / _pageSize);
            if (_totalPages == 0) _totalPages = 1;

            var logs = _logService.GetPaginatedLogs(_currentPage, _pageSize, _currentSearchTerm);
            historyTableuc1.SetData(logs);

            lblPageInfo.Text = $"Page {_currentPage} of {_totalPages}";
            btnPrevious.Enabled = _currentPage > 1;
            btnNext.Enabled = _currentPage < _totalPages;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                LoadPage();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                LoadPage();
            }
        }
    }
}