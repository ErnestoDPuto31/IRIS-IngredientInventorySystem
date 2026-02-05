    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D; // Required for rounded corners
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
        private void LoadData()
        {
            var data = _restockService.GetRestockList().ToList();

            // I-distribute ang data sa bawat card
            // Gagamit tayo ng LINQ para mabilang kung ilan ang match sa bawat status

            cardEmpty.UpdateData(data.Count(x => x.Status == StockStatus.Empty));
            cardLow.UpdateData(data.Count(x => x.Status == StockStatus.LowStock));
            cardWell.UpdateData(data.Count(x => x.Status == StockStatus.WellStocked));

            // I-update din ang table
            gvTable.DataSource = data;
        }
        public RestockTable()
            {
                InitializeComponent();

                try
                {
                    _restockService = ServiceFactory.GetRestockService();
                    ConfigureTable();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Initialization Error: {ex.Message}");
                }
            }

            private void ConfigureTable()
            {
                // 1. General Grid Settings
                gvTable.AutoGenerateColumns = false;
                gvTable.AllowUserToAddRows = false;
                gvTable.AllowUserToResizeRows = false;
                gvTable.ReadOnly = true;
                gvTable.RowHeadersVisible = false;
                gvTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // DESIGN FIX: Horizontal Lines Only
                gvTable.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                gvTable.GridColor = Color.FromArgb(230, 230, 230); // Light Grey Lines
                gvTable.BackgroundColor = Color.White;

                // Row & Header Heights
                gvTable.ColumnHeadersHeight = 50;
                gvTable.RowTemplate.Height = 60; // Taller rows for the badges

                gvTable.Columns.Clear();

                // 2. Setup Columns (Widths adjusted to match image)
                gvTable.Columns.Add(CreateTextColumn("Ingredient", "IngredientName", 200));
                gvTable.Columns.Add(CreateTextColumn("Category", "Category", 150));
                gvTable.Columns.Add(CreateTextColumn("Current Stock", "CurrentStock", 150));
                gvTable.Columns.Add(CreateTextColumn("Min Threshold", "MinimumThreshold", 150));
                gvTable.Columns.Add(CreateTextColumn("Suggested Restock", "SuggestedRestockQuantity", 180));
                gvTable.Columns.Add(CreateTextColumn("Status", "Status", 120));

                // 3. Setup Action Button
                // We create a blank button here because we will manually paint the Indigo shape over it
                var actionBtn = new DataGridViewButtonColumn
                {
                    HeaderText = "Action",
                    Name = "btnAction",
                    Text = "",
                    Width = 140
                };
                gvTable.Columns.Add(actionBtn);

            // 4. Header Styling (Minimalist)
            gvTable.EnableHeadersVisualStyles = false;
            gvTable.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            gvTable.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            gvTable.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            gvTable.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            gvTable.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

                // 5. Wire up events
                gvTable.CellPainting += GvTable_CellPainting;
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
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };
            }

            // --- THE KEY DESIGN METHOD ---
            private void GvTable_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
            {
                if (e.RowIndex < 0) return; // Skip header

                // 1. CUSTOM BUTTON PAINTING (Solid Indigo Button)
                if (gvTable.Columns[e.ColumnIndex].Name == "btnAction")
                {
                    // Draw background (White)
                    e.PaintBackground(e.CellBounds, true);

                    // Define Button Size (smaller than cell for padding)
                    int btnWidth = 100;
                    int btnHeight = 35;
                    Rectangle btnRect = new Rectangle(
                        e.CellBounds.X + (e.CellBounds.Width - btnWidth) / 2,
                        e.CellBounds.Y + (e.CellBounds.Height - btnHeight) / 2,
                        btnWidth,
                        btnHeight
                    );

                    // Draw Rounded Indigo Rectangle
                    using (GraphicsPath path = GetRoundedPath(btnRect, 10)) // Radius 10
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(75, 0, 130))) // Indigo
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        e.Graphics.FillPath(brush, path);

                        // Draw Text
                        TextRenderer.DrawText(e.Graphics, "+ Restock",
                            new Font("Segoe UI", 9, FontStyle.Bold),
                            btnRect, Color.White,
                            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    }

                    e.Handled = true; // Prevent default button drawing
                }
                // 2. CATEGORY BADGE (Rounded Outline)
                else if (gvTable.Columns[e.ColumnIndex].Name == "Category")
                {
                    e.PaintBackground(e.CellBounds, true);
                    if (e.Value != null)
                    {
                        string text = e.Value.ToString();
                        var textSize = e.Graphics.MeasureString(text, e.CellStyle.Font);
                        int badgeWidth = (int)textSize.Width + 20;
                        int badgeHeight = 24;

                        Rectangle badgeRect = new Rectangle(
                            e.CellBounds.X,
                            e.CellBounds.Y + (e.CellBounds.Height - badgeHeight) / 2,
                            badgeWidth,
                            badgeHeight
                        );

                        using (GraphicsPath path = GetRoundedPath(badgeRect, 12))
                        using (Pen pen = new Pen(Color.LightGray, 1))
                        {
                            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            e.Graphics.DrawPath(pen, path);

                            TextRenderer.DrawText(e.Graphics, text, new Font("Segoe UI", 9), badgeRect, Color.Black,
                                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                        }

                        // Manually draw bottom border since we handled the painting
                        using (Pen p = new Pen(gvTable.GridColor))
                            e.Graphics.DrawLine(p, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
                    }
                    e.Handled = true;
                }
                // 3. STATUS BADGE (Filled Colored Pill)
                else if (gvTable.Columns[e.ColumnIndex].Name == "Status")
                {
                    e.PaintBackground(e.CellBounds, true);
                    if (e.RowIndex >= 0 && gvTable.Rows[e.RowIndex].DataBoundItem is Restock item)
                    {
                        string statusText = item.Status == StockStatus.Empty ? "empty" : "low";
                        Color bgColor = item.Status == StockStatus.Empty ? Color.Crimson : Color.FromArgb(255, 245, 157); // Red or Pale Yellow
                        Color textColor = item.Status == StockStatus.Empty ? Color.White : Color.FromArgb(100, 100, 0);

                        int badgeWidth = 60;
                        int badgeHeight = 22;
                        Rectangle badgeRect = new Rectangle(
                            e.CellBounds.X,
                            e.CellBounds.Y + (e.CellBounds.Height - badgeHeight) / 2,
                            badgeWidth,
                            badgeHeight
                        );

                        using (GraphicsPath path = GetRoundedPath(badgeRect, 10))
                        using (SolidBrush brush = new SolidBrush(bgColor))
                        {
                            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            e.Graphics.FillPath(brush, path);

                            TextRenderer.DrawText(e.Graphics, statusText, new Font("Segoe UI", 8, FontStyle.Bold), badgeRect, textColor,
                                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                        }

                        // Manually draw bottom border
                        using (Pen p = new Pen(gvTable.GridColor))
                            e.Graphics.DrawLine(p, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
                    }
                    e.Handled = true;
                }
                // 4. TEXT COLORS (Numbers)
                else
                {
                    // Default paint handles standard text and selection highlighting
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    // If it's one of our special number columns, we redraw the text with custom colors
                    if (gvTable.Columns[e.ColumnIndex].Name == "CurrentStock" ||
                        gvTable.Columns[e.ColumnIndex].Name == "SuggestedRestockQuantity")
                    {
                        // Clear the text area again (keeping selection background if selected)
                        e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.SelectionBackground);

                        var item = (Restock)gvTable.Rows[e.RowIndex].DataBoundItem;
                        Color textColor = Color.Black;

                        if (gvTable.Columns[e.ColumnIndex].Name == "CurrentStock")
                            textColor = (item.CurrentStock == 0) ? Color.Red : Color.Goldenrod;
                        else if (gvTable.Columns[e.ColumnIndex].Name == "SuggestedRestockQuantity")
                            textColor = Color.Purple;

                        string cellValue = e.Value?.ToString() + " grams";

                        // Adjust text position
                        Rectangle textRect = e.CellBounds;
                        textRect.X += 2;

                        TextRenderer.DrawText(e.Graphics, cellValue, e.CellStyle.Font, textRect, textColor,
                            TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

                        e.Handled = true;
                    }
                }
            }

            // Helper for rounded corners
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

            private void LoadData()
            {
                try
                {
                    _restockService.RefreshRestockData();
                    var data = _restockService.GetRestockList();
                    gvTable.DataSource = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data: {ex.Message}");
                }
            }

            private void GvTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex >= 0 && gvTable.Columns[e.ColumnIndex].Name == "btnAction")
                {
                    var selectedItem = (Restock)gvTable.Rows[e.RowIndex].DataBoundItem;
                    string input = Microsoft.VisualBasic.Interaction.InputBox(
                        $"Enter restock amount for {selectedItem.IngredientName}:", "Inventory Update", "0");

                    if (decimal.TryParse(input, out decimal amount) && amount > 0)
                    {
                        _restockService.ProcessRestock(selectedItem.RestockId, amount);
                        LoadData();
                    }
                }
            }
        }
    }