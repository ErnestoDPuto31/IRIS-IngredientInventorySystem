using IRIS.Domain.Entities;
using IRIS.Infrastructure.Data;
using IRIS.Presentation.Window_Forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IRIS.UI.Controls
{
    public partial class RequestsTable : UserControl
    {
        public RequestsTable()
        {
            InitializeComponent();

            dgv.CellFormatting += Dgv_CellFormatting;
            dgv.CellContentClick += Dgv_CellContentClick;

            LoadRequests();
        }

        public void LoadRequests()
        {
            try
            {
                var options = new DbContextOptionsBuilder<IrisDbContext>()
                   .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=IRIS_DB;Trusted_Connection=True;")
                   .Options;

                using (var context = new IrisDbContext(options))
                {
                    var data = context.Requests
                        .AsNoTracking()
                        .OrderByDescending(r => r.CreatedAt)
                        .Select(r => new
                        {
                            r.RequestId,
                            r.Subject,
                            r.FacultyName,
                            r.Status,
                            r.CreatedAt
                        })
                        .ToList();

                    dgv.AutoGenerateColumns = false;
                    dgv.DataSource = data;
                    dgv.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void Dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgv.Columns[e.ColumnIndex].Name == "colStatus" && e.Value != null)
            {
                string status = e.Value.ToString();
                e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                switch (status)
                {
                    case "Pending": e.CellStyle.ForeColor = Color.FromArgb(180, 83, 9); break;
                    case "Approved": e.CellStyle.ForeColor = Color.FromArgb(21, 128, 61); break;
                    case "Released": e.CellStyle.ForeColor = Color.FromArgb(37, 99, 235); break;
                    case "Rejected": e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38); break;
                }
            }
        }

        private void Dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgv.Columns[e.ColumnIndex].Name == "colView")
            {
                try
                {
                    dynamic selectedRow = dgv.Rows[e.RowIndex].DataBoundItem;
                    int reqId = selectedRow.RequestId;

                    OpenRequestForm(reqId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening row: " + ex.Message);
                }
            }
        }

        private void OpenRequestForm(int requestId)
        {
            frmViewRequests viewForm = new frmViewRequests();
            viewForm.TargetRequestId = requestId;
            viewForm.RequestUpdated += (s, args) => LoadRequests();
            viewForm.StartPosition = FormStartPosition.CenterParent;
            viewForm.ShowDialog();
        }
    }
}