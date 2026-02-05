using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Services.Interfaces;
using IRIS.Presentation.DependencyInjection;

namespace IRIS.Presentation.UserControls.Table
{
    public partial class RestockTable : UserControl
    {
        private readonly IRestockService _restockService;

        public RestockTable()
        {
            InitializeComponent();

            try
            {
                _restockService = ServiceFactory.GetRestockService();
                ConfigureTable();

                _restockService.RefreshRestockData();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Initialization Error: {ex.Message}");
            }
        }

        // --- PUBLIC METHOD (With Auto-Correction Sorting) ---
        public void SetData(object data)
        {
            if (data is IEnumerable<Restock> list)
            {
                // We calculate sort order dynamically based on math, NOT the stored Status
                var sortedList = list.OrderBy(x => GetCalculatedSortOrder(x)).ToList();
                gvTable.DataSource = sortedList;
            }
            else
            {
                gvTable.DataSource = data;
            }
            gvTable.Refresh();
        }

        // --- INTERNAL LOAD (With Auto-Correction Sorting) ---
        private void LoadData()
        {
            if (_restockService == null) return;

            var data = _restockService.GetRestockList()
                .OrderBy(x => GetCalculatedSortOrder(x))
                .ToList();

            gvTable.DataSource = data;
            gvTable.Refresh();
        }

        // --- FIX PART 1: Calculate Status for Sorting ---
        // We ignore 'item.Status' and check the numbers directly
        private int GetCalculatedSortOrder(Restock item)
        {
            if (item.CurrentStock <= 0) return 0; // Top: Empty
            if (item.CurrentStock <= item.MinimumThreshold) return 1; // Middle: Low
            return 2; // Bottom: Well Stocked
        }

        private void ConfigureTable()
        {
            gvTable.AllowUserToResizeColumns = false;
            gvTable.AllowUserToResizeRows = false;
            gvTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            gvTable.AutoGenerateColumns = false;
            gvTable.AllowUserToAddRows = false;
            gvTable.ReadOnly = true;
            gvTable.RowHeadersVisible = false;
            gvTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvTable.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            gvTable.GridColor = Color.FromArgb(230, 230, 230);
            gvTable.BackgroundColor = Color.White;
            gvTable.ColumnHeadersHeight = 50;
            gvTable.RowTemplate.Height = 60;

            gvTable.Columns.Clear();

            gvTable.Columns.Add(CreateTextColumn("Ingredient", "IngredientName", 200));
            gvTable.Columns.Add(CreateTextColumn("Category", "Category", 150));
            gvTable.Columns.Add(CreateTextColumn("Current Stock", "CurrentStock", 150));
            gvTable.Columns.Add(CreateTextColumn("Min Threshold", "MinimumThreshold", 150));
            gvTable.Columns.Add(CreateTextColumn("Suggested Restock", "SuggestedRestockQuantity", 180));
            gvTable.Columns.Add(CreateTextColumn("Status", "Status", 120));

            var actionBtn = new DataGridViewButtonColumn
            {
                HeaderText = "Action",
                Name = "btnAction",
                Text = "",
                Width = 140
            };
            gvTable.Columns.Add(actionBtn);

            gvTable.EnableHeadersVisualStyles = false;
            gvTable.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            gvTable.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            gvTable.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            gvTable.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn col in gvTable.Columns)
            {
                if (col.Name == "IngredientName")
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                else
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            gvTable.CellPainting -= GvTable_CellPainting;
            gvTable.CellPainting += GvTable_CellPainting;

            gvTable.CellContentClick -= GvTable_CellContentClick;
            gvTable.CellContentClick += GvTable_CellContentClick;
        }

        private DataGridViewTextBoxColumn CreateTextColumn(string header, string property, int width)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = property,
                HeaderText = header,
                DataPropertyName = property,
                Width = width,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Resizable = DataGridViewTriState.False
            };
        }

        private void GvTable_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (gvTable.Columns[e.ColumnIndex].Name == "btnAction")
            {
                e.PaintBackground(e.CellBounds, true);
                int btnWidth = 100, btnHeight = 35;
                Rectangle btnRect = new Rectangle(e.CellBounds.X + (e.CellBounds.Width - btnWidth) / 2,
                                                  e.CellBounds.Y + (e.CellBounds.Height - btnHeight) / 2, btnWidth, btnHeight);

                using (GraphicsPath path = GetRoundedPath(btnRect, 10))
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(75, 0, 130)))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.FillPath(brush, path);
                    TextRenderer.DrawText(e.Graphics, "+ Restock", new Font("Segoe UI", 9, FontStyle.Bold), btnRect, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
                e.Handled = true;
            }
            else if (gvTable.Columns[e.ColumnIndex].Name == "Category")
            {
                e.PaintBackground(e.CellBounds, true);
                if (e.Value != null)
                {
                    string text = e.Value.ToString();
                    var textSize = e.Graphics.MeasureString(text, e.CellStyle.Font);
                    int badgeWidth = (int)textSize.Width + 20;
                    Rectangle badgeRect = new Rectangle(e.CellBounds.X + (e.CellBounds.Width - badgeWidth) / 2,
                                                        e.CellBounds.Y + (e.CellBounds.Height - 24) / 2, badgeWidth, 24);

                    using (GraphicsPath path = GetRoundedPath(badgeRect, 12))
                    using (Pen pen = new Pen(Color.LightGray, 1))
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        e.Graphics.DrawPath(pen, path);
                        TextRenderer.DrawText(e.Graphics, text, new Font("Segoe UI", 9), badgeRect, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    }
                    using (Pen p = new Pen(gvTable.GridColor))
                        e.Graphics.DrawLine(p, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
                }
                e.Handled = true;
            }
            // --- FIX PART 2: Status Visuals ---
            else if (gvTable.Columns[e.ColumnIndex].Name == "Status")
            {
                e.PaintBackground(e.CellBounds, true);
                if (e.RowIndex >= 0 && gvTable.Rows[e.RowIndex].DataBoundItem is Restock item)
                {
                    string statusText;
                    Color bgColor;
                    Color textColor;

                    // OVERRIDE: We ignore 'item.Status' and calculate strictly on numbers
                    // This fixes the issue where the DB sends "Low" for well-stocked items
                    if (item.CurrentStock <= 0)
                    {
                        statusText = "Empty";
                        bgColor = Color.Crimson;
                        textColor = Color.White;
                    }
                    else if (item.CurrentStock <= item.MinimumThreshold)
                    {
                        statusText = "Low";
                        bgColor = Color.FromArgb(255, 245, 157); // Yellow
                        textColor = Color.FromArgb(100, 100, 0);
                    }
                    else
                    {
                        // Explicitly catch everything else as Well Stocked
                        statusText = "Well Stocked";
                        bgColor = Color.FromArgb(200, 240, 200); // Green
                        textColor = Color.FromArgb(0, 100, 0);
                    }

                    int badgeWidth = 90;
                    Rectangle badgeRect = new Rectangle(e.CellBounds.X + (e.CellBounds.Width - badgeWidth) / 2,
                                                        e.CellBounds.Y + (e.CellBounds.Height - 22) / 2, badgeWidth, 22);

                    using (GraphicsPath path = GetRoundedPath(badgeRect, 10))
                    using (SolidBrush brush = new SolidBrush(bgColor))
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        e.Graphics.FillPath(brush, path);
                        TextRenderer.DrawText(e.Graphics, statusText, new Font("Segoe UI", 8, FontStyle.Bold), badgeRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    }
                    using (Pen p = new Pen(gvTable.GridColor))
                        e.Graphics.DrawLine(p, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
                }
                e.Handled = true;
            }
            else if (gvTable.Columns[e.ColumnIndex].Name == "CurrentStock" || gvTable.Columns[e.ColumnIndex].Name == "SuggestedRestockQuantity")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.SelectionBackground);

                var item = (Restock)gvTable.Rows[e.RowIndex].DataBoundItem;
                Color textColor = Color.Black;

                if (gvTable.Columns[e.ColumnIndex].Name == "CurrentStock")
                    textColor = (item.CurrentStock == 0) ? Color.Red : Color.Goldenrod;
                else
                    textColor = Color.Purple;

                string cellValue = e.Value?.ToString() + " grams";
                TextRenderer.DrawText(e.Graphics, cellValue, e.CellStyle.Font, e.CellBounds, textColor, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                e.Handled = true;
            }
            else
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void GvTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && gvTable.Columns[e.ColumnIndex].Name == "btnAction")
            {
                var selectedItem = (Restock)gvTable.Rows[e.RowIndex].DataBoundItem;
                string input = Microsoft.VisualBasic.Interaction.InputBox($"Enter restock amount for {selectedItem.IngredientName}:", "Inventory Update", "0");
                if (decimal.TryParse(input, out decimal amount) && amount > 0)
                {
                    _restockService.ProcessRestock(selectedItem.RestockId, amount);
                    LoadData();
                }
            }
        }
    }
}