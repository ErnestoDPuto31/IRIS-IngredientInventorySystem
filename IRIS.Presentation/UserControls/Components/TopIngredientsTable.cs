using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using IRIS.Domain.Helpers;

namespace IRIS.Presentation.UserControls.Components
{
    public partial class TopIngredientsTable : UserControl
    {
        private int hoveredRowIndex = -1;

        private readonly Color appBackground = Color.FromArgb(245, 247, 252);
        private readonly Color cardBackground = Color.White;
        private readonly Color defaultRowColor = Color.White;
        private readonly Color alternateRowColor = Color.FromArgb(246, 248, 252);
        private readonly Color hoverRowColor = Color.FromArgb(230, 238, 255);

        private Label lblSubtitle;

        public TopIngredientsTable()
        {
            InitializeComponent();

            DoubleBuffered = true;
            BackColor = appBackground;

            BuildLayout();
            ApplyModernStyling();
            EnableDoubleBufferingForGrid();

            dgvTopIngredients.CellMouseEnter += DgvTopIngredients_CellMouseEnter;
            dgvTopIngredients.CellMouseLeave += DgvTopIngredients_CellMouseLeave;
            dgvTopIngredients.CellFormatting += DgvTopIngredients_CellFormatting;
            dgvTopIngredients.DataBindingComplete += (s, e) => dgvTopIngredients.ClearSelection();
        }

        private void BuildLayout()
        {
            Controls.Clear();

            // title
            lblTitle = new Label
            {
                Dock = DockStyle.Top,
                Height = 38,
                Padding = new Padding(18, 12, 0, 0),
                Font = new Font("Segoe UI Semibold", 13F),
                ForeColor = Color.FromArgb(32, 64, 140),
                Text = "Top 5 Ingredients"
            };

            // subtitle / date
            lblSubtitle = new Label
            {
                Dock = DockStyle.Top,
                Height = 26,
                Padding = new Padding(18, 0, 0, 0),
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(110, 120, 140),
                Text = $"as of {DateTime.Now:MMMM dd, yyyy}"
            };

            Panel card = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(14),
                BackColor = cardBackground
            };

            dgvTopIngredients.Dock = DockStyle.Fill;

            card.Controls.Add(dgvTopIngredients);
            Controls.Add(card);
            Controls.Add(lblSubtitle);
            Controls.Add(lblTitle);
        }

        private void DgvTopIngredients_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 &&
                dgvTopIngredients.Columns[e.ColumnIndex].Name == "Category" &&
                e.Value != null)
            {
                e.Value = FormatHelper.FormatCategoryName(e.Value.ToString());
                e.FormattingApplied = true;
            }
        }

        private void EnableDoubleBufferingForGrid()
        {
            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null,
                dgvTopIngredients,
                new object[] { true }
            );
        }

        private void ApplyModernStyling()
        {
            dgvTopIngredients.BackgroundColor = Color.White;
            dgvTopIngredients.BorderStyle = BorderStyle.None;
            dgvTopIngredients.CellBorderStyle = DataGridViewCellBorderStyle.None;

            dgvTopIngredients.RowHeadersVisible = false;
            dgvTopIngredients.AllowUserToAddRows = false;
            dgvTopIngredients.AllowUserToDeleteRows = false;
            dgvTopIngredients.AllowUserToResizeRows = false;
            dgvTopIngredients.ReadOnly = true;

            dgvTopIngredients.EnableHeadersVisualStyles = false;
            dgvTopIngredients.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvTopIngredients.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvTopIngredients.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(70, 80, 100);
            dgvTopIngredients.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI Semibold", 10.5F);
            dgvTopIngredients.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            dgvTopIngredients.ColumnHeadersHeight = 48;
            dgvTopIngredients.ColumnHeadersDefaultCellStyle.Padding =
                new Padding(12, 0, 12, 0);

            dgvTopIngredients.DefaultCellStyle.BackColor = defaultRowColor;
            dgvTopIngredients.DefaultCellStyle.ForeColor = Color.FromArgb(55, 65, 85);
            dgvTopIngredients.DefaultCellStyle.Font =
                new Font("Segoe UI", 10F);
            dgvTopIngredients.DefaultCellStyle.SelectionBackColor = hoverRowColor;
            dgvTopIngredients.DefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 35, 55);
            dgvTopIngredients.DefaultCellStyle.Padding =
                new Padding(12, 0, 12, 0);

            dgvTopIngredients.RowsDefaultCellStyle.BackColor = defaultRowColor;
            dgvTopIngredients.AlternatingRowsDefaultCellStyle.BackColor = alternateRowColor;

            dgvTopIngredients.RowTemplate.Height = 56;
            dgvTopIngredients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTopIngredients.Cursor = Cursors.Hand;
        }

        private void DgvTopIngredients_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex == hoveredRowIndex) return;

            if (hoveredRowIndex >= 0 && hoveredRowIndex < dgvTopIngredients.Rows.Count)
                dgvTopIngredients.Rows[hoveredRowIndex].DefaultCellStyle.BackColor =
                    hoveredRowIndex % 2 == 0 ? defaultRowColor : alternateRowColor;

            hoveredRowIndex = e.RowIndex;
            dgvTopIngredients.Rows[hoveredRowIndex].DefaultCellStyle.BackColor = hoverRowColor;
        }

        private void DgvTopIngredients_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex != hoveredRowIndex) return;

            dgvTopIngredients.Rows[hoveredRowIndex].DefaultCellStyle.BackColor =
                hoveredRowIndex % 2 == 0 ? defaultRowColor : alternateRowColor;

            hoveredRowIndex = -1;
        }

        public void LoadData(object dataSource)
        {
            if (dgvTopIngredients.InvokeRequired)
                dgvTopIngredients.Invoke(new Action(() => LoadDataCore(dataSource)));
            else
                LoadDataCore(dataSource);
        }

        private void LoadDataCore(object dataSource)
        {
            dgvTopIngredients.DataSource = null;
            dgvTopIngredients.AutoGenerateColumns = true;
            dgvTopIngredients.DataSource = dataSource;

            if (dgvTopIngredients.Columns["Name"] != null)
            {
                dgvTopIngredients.Columns["Name"].HeaderText = "Ingredient";
                dgvTopIngredients.Columns["Name"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleLeft;
                dgvTopIngredients.Columns["Name"].HeaderCell.Style.Alignment =
                    DataGridViewContentAlignment.MiddleLeft;
                dgvTopIngredients.Columns["Name"].DefaultCellStyle.Font =
                    new Font("Segoe UI Semibold", 10.5F);
            }

            if (dgvTopIngredients.Columns["Category"] != null)
            {
                dgvTopIngredients.Columns["Category"].HeaderText = "Category";
                dgvTopIngredients.Columns["Category"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
                dgvTopIngredients.Columns["Category"].HeaderCell.Style.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvTopIngredients.Columns["TotalUsed"] != null)
            {
                dgvTopIngredients.Columns["TotalUsed"].HeaderText = "Total Used";
                dgvTopIngredients.Columns["TotalUsed"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
                dgvTopIngredients.Columns["TotalUsed"].HeaderCell.Style.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvTopIngredients.Columns["Unit"] != null)
            {
                dgvTopIngredients.Columns["Unit"].HeaderText = "Unit";
                dgvTopIngredients.Columns["Unit"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
                dgvTopIngredients.Columns["Unit"].HeaderCell.Style.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            dgvTopIngredients.ClearSelection();
        }
    }
}