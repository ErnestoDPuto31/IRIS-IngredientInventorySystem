using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using IRIS.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace IRIS.Presentation.Helpers
{
    public static class ExportPdf
    {
        public static void ExportAndRevealInExplorer(
            string filePath,
            string exportedBy,
            int totalIngredients,
            int totalRequests,
            int totalTransactions,
            double approvalRate,
            Dictionary<string, double> inventoryStats,
            Dictionary<string, double> requestStats,
            Dictionary<string, double> categoryStats,
            List<TopIngredientItem> topIngredients,
            List<LowStockItem> lowStock,
            byte[]? irisLogoPng
        )
        {
            QuestPDF.Settings.License = LicenseType.Community;

            // BIG pie chart from data
            byte[] piePng = RenderPieChartPng(
                inventoryStats ?? new Dictionary<string, double>(),
                width: 900,
                height: 520
            );

            var doc = new ReportsDocument(
                exportedBy,
                totalIngredients,
                totalRequests,
                totalTransactions,
                approvalRate,
                requestStats ?? new Dictionary<string, double>(),
                categoryStats ?? new Dictionary<string, double>(),
                topIngredients ?? new List<TopIngredientItem>(),
                lowStock ?? new List<LowStockItem>(),
                piePng,
                irisLogoPng
            );

            doc.GeneratePdf(filePath);

            try
            {
                Process.Start(new ProcessStartInfo("explorer.exe", $"/select,\"{filePath}\"")
                {
                    UseShellExecute = true
                });
            }
            catch { }
        }

        // convert System.Drawing.Image to PNG bytes (explicit System.Drawing types to avoid ambiguity)
        public static byte[] ImageToPngBytes(System.Drawing.Image img)
        {
            using var ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        // ============================
        // PIE CHART RENDERER
        // ============================
        private static byte[] RenderPieChartPng(Dictionary<string, double> data, int width, int height)
        {
            using var bmp = new System.Drawing.Bitmap(width, height);
            using var g = System.Drawing.Graphics.FromImage(bmp);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.Clear(System.Drawing.Color.White);

            int padding = 30;
            int titleH = 60;

            var chartRect = new System.Drawing.Rectangle(
                padding,
                padding + titleH,
                height - (padding * 2),
                height - (padding * 2) - titleH
            );

            var legendRect = new System.Drawing.Rectangle(
                chartRect.Right + 30,
                chartRect.Top,
                width - chartRect.Right - 60,
                chartRect.Height
            );

            using (var titleFont = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point))
            using (var titleBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(17, 24, 39)))
            {
                g.DrawString("Inventory Status Distribution", titleFont, titleBrush, padding, padding);
            }

            double total = data.Values.Sum();
            if (total <= 0.0001)
            {
                using var f = new System.Drawing.Font("Segoe UI", 12);
                using var b = new System.Drawing.SolidBrush(System.Drawing.Color.Gray);
                g.DrawString("No data", f, b, chartRect);
                return BitmapToPng(bmp);
            }

            var palette = new[]
            {
                System.Drawing.Color.FromArgb(239, 68, 68),   // red
                System.Drawing.Color.FromArgb(245, 158, 11),  // amber
                System.Drawing.Color.FromArgb(34, 197, 94),   // green
                System.Drawing.Color.FromArgb(124, 58, 237),  // purple
                System.Drawing.Color.FromArgb(59, 130, 246),  // blue
            };

            float start = -90f;
            int i = 0;

            foreach (var kv in data)
            {
                float sweep = (float)(kv.Value / total * 360.0);
                using var br = new System.Drawing.SolidBrush(palette[i % palette.Length]);
                g.FillPie(br, chartRect, start, sweep);
                start += sweep;
                i++;
            }

            using (var pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(225, 228, 233), 2))
                g.DrawEllipse(pen, chartRect);

            using var legendTitleFont = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            using var legendFont = new System.Drawing.Font("Segoe UI", 11);
            using var legendBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(17, 24, 39));
            using var mutedBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(107, 114, 128));

            g.DrawString("Legend", legendTitleFont, legendBrush, legendRect.X, legendRect.Y);

            int rowY = legendRect.Y + 34;
            int rowH = 34;
            i = 0;

            foreach (var kv in data)
            {
                var color = palette[i % palette.Length];
                var pct = (kv.Value / total) * 100.0;

                using var swatch = new System.Drawing.SolidBrush(color);
                FillRoundedRect(g, swatch, new System.Drawing.Rectangle(legendRect.X, rowY + 6, 18, 18), 6);

                g.DrawString(kv.Key, legendFont, legendBrush, legendRect.X + 28, rowY + 4);
                g.DrawString($"{kv.Value:0} ({pct:0.#}%)", legendFont, mutedBrush, legendRect.X + 300, rowY + 4);

                rowY += rowH;
                i++;
            }

            return BitmapToPng(bmp);
        }

        private static void FillRoundedRect(System.Drawing.Graphics g, System.Drawing.Brush brush, System.Drawing.Rectangle rect, int radius)
        {
            using var path = new GraphicsPath();
            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            g.FillPath(brush, path);
        }

        private static byte[] BitmapToPng(System.Drawing.Bitmap bmp)
        {
            using var ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        // ============================
        // QUESTPDF DOCUMENT
        // ============================
        private sealed class ReportsDocument : IDocument
        {
            private readonly string _exportedBy;
            private readonly int _totalIngredients;
            private readonly int _totalRequests;
            private readonly int _totalTransactions;
            private readonly double _approvalRate;

            private readonly Dictionary<string, double> _requestStats;
            private readonly Dictionary<string, double> _categoryStats;

            private readonly List<TopIngredientItem> _topIngredients;
            private readonly List<LowStockItem> _lowStock;

            private readonly byte[] _piePng;
            private readonly byte[]? _logoPng;

            private static readonly string Purple = "#7C3AED";

            public ReportsDocument(
                string exportedBy,
                int totalIngredients,
                int totalRequests,
                int totalTransactions,
                double approvalRate,
                Dictionary<string, double> requestStats,
                Dictionary<string, double> categoryStats,
                List<TopIngredientItem> topIngredients,
                List<LowStockItem> lowStock,
                byte[] piePng,
                byte[]? logoPng
            )
            {
                _exportedBy = exportedBy;
                _totalIngredients = totalIngredients;
                _totalRequests = totalRequests;
                _totalTransactions = totalTransactions;
                _approvalRate = approvalRate;

                _requestStats = requestStats;
                _categoryStats = categoryStats;

                _topIngredients = topIngredients;
                _lowStock = lowStock;

                _piePng = piePng;
                _logoPng = logoPng;
            }

            public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

            public void Compose(IDocumentContainer container)
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(26);
                    page.DefaultTextStyle(x => x.FontFamily("Segoe UI").FontSize(10).FontColor(Colors.Grey.Darken3));

                    page.Header().Element(Header);
                    page.Content().Element(Content);
                    page.Footer().Element(Footer);
                });
            }
            private void Footer(IContainer c)
            {
                c.PaddingTop(8).Row(row =>
                {
                    row.RelativeItem().Text(t =>
                    {
                        t.Span("exported by ").FontColor(Colors.Grey.Darken1);
                        t.Span(_exportedBy).SemiBold().FontColor(Colors.Black);
                        t.Span(" • ").FontColor(Colors.Grey.Darken1);
                        t.Span(DateTime.Now.ToString("MMM dd, yyyy hh:mm tt")).FontColor(Colors.Grey.Darken1);
                    });

                    row.ConstantItem(120).AlignRight().Text(t =>
                    {
                        t.CurrentPageNumber();
                        t.Span(" / ");
                        t.TotalPages();
                    });
                });
            }
            private void Header(IContainer c)
            {
                c.PaddingBottom(10).Row(row =>
                {
                    row.ConstantItem(6).Background(Purple).Height(54);

                    row.Spacing(12);

                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("IRIS Reports & Analytics").FontSize(18).SemiBold().FontColor(Colors.Black);
                        col.Item().Text("Data for today").FontColor(Colors.Grey.Darken1);
                    });

                    row.ConstantItem(90).AlignRight().AlignMiddle().Element(x =>
                    {
                        if (_logoPng == null || _logoPng.Length == 0)
                            return x;

                        x.Height(46).Image(_logoPng).FitHeight();
                        return x;
                    });
                });
            }

         
            private void Content(IContainer c)
            {
                c.Column(col =>
                {
                    col.Spacing(14);

                    col.Item().Row(r =>
                    {
                        r.Spacing(10);
                        r.RelativeItem().Element(x => StatCard(x, "Total Ingredients", _totalIngredients.ToString()));
                        r.RelativeItem().Element(x => StatCard(x, "Total Requests", _totalRequests.ToString()));
                        r.RelativeItem().Element(x => StatCard(x, "Approval Rate", $"{_approvalRate:0.0}%"));
                        r.RelativeItem().Element(x => StatCard(x, "Transactions", _totalTransactions.ToString()));
                    });

                    // BIG PIE (bigger)
                    col.Item().Element(x => Card(x, "Inventory Status Distribution", body =>
                    {
                        body.Height(320).Image(_piePng).FitArea();
                    }));

                    // tables only
                    col.Item().Element(x => Card(x, "Request Status Summary", body => SimpleKeyValueTable(body, _requestStats, "request")));
                    col.Item().Element(x => Card(x, "Ingredients by Category", body => SimpleKeyValueTable(body, _categoryStats, "category")));
                    col.Item().Element(x => Card(x, "Top 5 Most Used Ingredients", body => TopIngredientsTable(body, _topIngredients)));
                    col.Item().Element(x => Card(x, "Low Stock Alerts", body => LowStockTable(body, _lowStock)));
                });
            }

            private void StatCard(IContainer c, string title, string value)
            {
                c.Border(1).BorderColor(Colors.Grey.Lighten2)
                 .CornerRadius(12)
                 .Padding(12)
                 .Background(Colors.White)
                 .Column(col =>
                 {
                     col.Item().Text(title).FontColor(Colors.Grey.Darken1);
                     col.Item().Text(value).FontSize(16).SemiBold().FontColor(Colors.Black);
                 });
            }

            private void Card(IContainer c, string title, Action<IContainer> content)
            {
                c.Border(1).BorderColor(Colors.Grey.Lighten2)
                 .CornerRadius(12)
                 .Padding(12)
                 .Background(Colors.White)
                 .Column(col =>
                 {
                     col.Item().Row(r =>
                     {
                         r.ConstantItem(4).Height(16).Background(Purple);
                         r.Spacing(10);
                         r.RelativeItem().Text(title).SemiBold().FontColor(Colors.Black);
                     });

                     col.Item().PaddingTop(10).Element(content);
                 });
            }

            private void SimpleKeyValueTable(IContainer c, Dictionary<string, double> data, string firstColumnHeader)
            {
                var rows = data?.ToList() ?? new List<KeyValuePair<string, double>>();
                if (rows.Count == 0)
                {
                    c.Text("No data").FontColor(Colors.Grey.Medium);
                    return;
                }

                c.Table(t =>
                {
                    t.ColumnsDefinition(cols =>
                    {
                        cols.RelativeColumn(3);
                        cols.RelativeColumn(1);
                    });

                    t.Header(h =>
                    {
                        h.Cell().Element(CellHeader).Text(firstColumnHeader);
                        h.Cell().Element(CellHeader).AlignRight().Text("count");
                    });

                    foreach (var kv in rows)
                    {
                        t.Cell().Element(CellBody).Text(kv.Key);
                        t.Cell().Element(CellBody).AlignRight().Text($"{kv.Value:0}");
                    }
                });
            }
            private void TopIngredientsTable(IContainer c, List<TopIngredientItem> rows)
            {
                if (rows == null || rows.Count == 0)
                {
                    c.Text("No data").FontColor(Colors.Grey.Medium);
                    return;
                }

                c.Table(t =>
                {
                    t.ColumnsDefinition(cols =>
                    {
                        cols.RelativeColumn(3);
                        cols.RelativeColumn(2);
                        cols.RelativeColumn(2);
                        cols.RelativeColumn(2);
                    });

                    t.Header(h =>
                    {
                        h.Cell().Element(CellHeader).Text("ingredients");
                        h.Cell().Element(CellHeader).Text("category");
                        h.Cell().Element(CellHeader).AlignRight().Text("total used");
                        h.Cell().Element(CellHeader).Text("unit");
                    });

                    foreach (var x in rows)
                    {
                        t.Cell().Element(CellBody).Text(x.Name);
                        t.Cell().Element(CellBody).Text(x.Category);
                        t.Cell().Element(CellBody).AlignRight().Text($"{x.TotalUsed:0.##}");
                        t.Cell().Element(CellBody).Text(x.Unit);
                    }
                });
            }

            private void LowStockTable(IContainer c, List<LowStockItem> rows)
            {
                if (rows == null || rows.Count == 0)
                {
                    c.Text("No data").FontColor(Colors.Grey.Medium);
                    return;
                }

                c.Table(t =>
                {
                    t.ColumnsDefinition(cols =>
                    {
                        cols.RelativeColumn(3);
                        cols.RelativeColumn(2);
                        cols.RelativeColumn(2);
                        cols.RelativeColumn(2);
                        cols.RelativeColumn(2);
                    });

                    t.Header(h =>
                    {
                        h.Cell().Element(CellHeader).Text("ingredient");
                        h.Cell().Element(CellHeader).Text("category");
                        h.Cell().Element(CellHeader).AlignRight().Text("stock");
                        h.Cell().Element(CellHeader).AlignRight().Text("min");
                        h.Cell().Element(CellHeader).Text("unit");
                    });

                    foreach (var x in rows)
                    {
                        t.Cell().Element(CellBody).Text(x.Name);
                        t.Cell().Element(CellBody).Text(x.Category);
                        t.Cell().Element(CellBody).AlignRight().Text($"{x.Stock:0.##}");
                        t.Cell().Element(CellBody).AlignRight().Text($"{x.Min:0.##}");
                        t.Cell().Element(CellBody).Text(x.Unit);
                    }
                });
            }

            private static IContainer CellHeader(IContainer c) =>
                c.Background(Colors.Grey.Lighten4)
                 .PaddingVertical(6)
                 .PaddingHorizontal(8)
                 .DefaultTextStyle(x => x.SemiBold().FontColor(Colors.Grey.Darken3));

            private static IContainer CellBody(IContainer c) =>
                c.BorderBottom(1)
                 .BorderColor(Colors.Grey.Lighten3)
                 .PaddingVertical(6)
                 .PaddingHorizontal(8);
        }
    }
}