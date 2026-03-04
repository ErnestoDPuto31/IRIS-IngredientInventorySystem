using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using ClosedXML.Excel;
using IRIS.Domain.Entities;

namespace IRIS.Presentation.Helpers
{
    public static class ExportExcel
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
            List<LowStockItem> lowStock
        )
        {
            exportedBy = string.IsNullOrWhiteSpace(exportedBy) ? "Unknown User" : exportedBy;

            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Reports");

            // theme
            var purple = XLColor.FromHtml("#7C3AED");
            var lightPurple = XLColor.FromHtml("#F5F3FF");
            var lightGray = XLColor.FromHtml("#F8F9FC");
            var border = XLColor.FromHtml("#DFE2E9");

            int r = 1;

            // ===== title bar =====
            ws.Range(r, 1, r, 6).Merge().Value = "IRIS Reports & Analytics";
            ws.Range(r, 1, r, 6).Style
                .Font.SetBold().Font.SetFontSize(16).Font.SetFontColor(XLColor.White)
                .Fill.SetBackgroundColor(purple)
                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
            ws.Row(r).Height = 26;
            r++;

            ws.Cell(r, 1).Value = "Exported By";
            ws.Cell(r, 2).Value = exportedBy;
            ws.Cell(r, 4).Value = "Exported At";
            ws.Cell(r, 5).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            ws.Range(r, 1, r, 6).Style.Font.SetFontSize(10);
            r += 2;

            // ===== summary cards section =====
            r = SectionTitle(ws, r, "Summary", purple, 6);

            ws.Cell(r, 1).Value = "Metric";
            ws.Cell(r, 2).Value = "Value";
            StyleHeader(ws.Range(r, 1, r, 2), lightPurple, border);
            r++;

            var summary = new (string k, string v)[]
            {
                ("Total Ingredients", totalIngredients.ToString(CultureInfo.InvariantCulture)),
                ("Total Requests", totalRequests.ToString(CultureInfo.InvariantCulture)),
                ("Total Transactions", totalTransactions.ToString(CultureInfo.InvariantCulture)),
                ("Approval Rate (%)", approvalRate.ToString("0.0", CultureInfo.InvariantCulture))
            };

            foreach (var s in summary)
            {
                ws.Cell(r, 1).Value = s.k;
                ws.Cell(r, 2).Value = s.v;
                StyleRow(ws.Range(r, 1, r, 2), lightGray, border, zebra: (r % 2 == 0));
                r++;
            }
            r++;

            // ===== inventory stats =====
            r = KeyValueSection(ws, r, "Inventory Status Distribution", "Status", inventoryStats, purple, border);

            // ===== request stats =====
            r = KeyValueSection(ws, r, "Request Status Distribution", "Request", requestStats, purple, border);

            // ===== category stats =====
            r = KeyValueSection(ws, r, "Ingredients By Category", "Category", categoryStats, purple, border);

            // ===== top ingredients =====
            r = SectionTitle(ws, r, "Top 5 Most Used Ingredients", purple, 6);

            var topStart = r;
            ws.Cell(r, 1).Value = "Ingredients";
            ws.Cell(r, 2).Value = "Category";
            ws.Cell(r, 3).Value = "Total Used";
            ws.Cell(r, 4).Value = "Unit";
            ws.Range(r, 1, r, 4).Style.Fill.SetBackgroundColor(lightPurple);
            StyleHeader(ws.Range(r, 1, r, 4), lightPurple, border);
            r++;

            foreach (var x in topIngredients ?? new List<TopIngredientItem>())
            {
                ws.Cell(r, 1).Value = x?.Name ?? "";
                ws.Cell(r, 2).Value = x?.Category ?? "";
                ws.Cell(r, 3).Value = x?.TotalUsed ?? 0;
                ws.Cell(r, 3).Style.NumberFormat.Format = "0.##";
                ws.Cell(r, 4).Value = x?.Unit ?? "";
                StyleRow(ws.Range(r, 1, r, 4), lightGray, border, zebra: (r % 2 == 0));
                r++;
            }

            // make it a nice table
            if (r - 1 >= topStart + 1)
                ws.Range(topStart, 1, r - 1, 4).CreateTable().Theme = XLTableTheme.TableStyleLight9;

            r++;

            // ===== low stock =====
            r = SectionTitle(ws, r, "Low Stock Alerts", purple, 6);

            var lowStart = r;
            ws.Cell(r, 1).Value = "Ingredients";
            ws.Cell(r, 2).Value = "Category";
            ws.Cell(r, 3).Value = "Current Stock";
            ws.Cell(r, 4).Value = "Min Threshold";
            ws.Cell(r, 5).Value = "Unit";
            StyleHeader(ws.Range(r, 1, r, 5), lightPurple, border);
            r++;

            foreach (var x in lowStock ?? new List<LowStockItem>())
            {
                ws.Cell(r, 1).Value = x?.Name ?? "";
                ws.Cell(r, 2).Value = x?.Category ?? "";
                ws.Cell(r, 3).Value = x?.Stock ?? 0;
                ws.Cell(r, 3).Style.NumberFormat.Format = "0.##";
                ws.Cell(r, 4).Value = x?.Min ?? 0;
                ws.Cell(r, 4).Style.NumberFormat.Format = "0.##";
                ws.Cell(r, 5).Value = x?.Unit ?? "";

                StyleRow(ws.Range(r, 1, r, 5), lightGray, border, zebra: (r % 2 == 0));

                // highlight low rows
                if (x != null && x.Stock < x.Min)
                {
                    ws.Range(r, 1, r, 5).Style.Fill.SetBackgroundColor(XLColor.FromHtml("#FFE4E6")); // soft red
                    ws.Range(r, 1, r, 5).Style.Font.SetFontColor(XLColor.FromHtml("#991B1B"));
                }

                r++;
            }

            if (r - 1 >= lowStart + 1)
                ws.Range(lowStart, 1, r - 1, 5).CreateTable().Theme = XLTableTheme.TableStyleLight9;

            // final layout polish
            ws.Columns(1, 6).AdjustToContents();
            ws.SheetView.FreezeRows(3);

            wb.SaveAs(filePath);

            try
            {
                Process.Start(new ProcessStartInfo("explorer.exe", $"/select,\"{filePath}\"")
                {
                    UseShellExecute = true
                });
            }
            catch { }
        }

        private static int SectionTitle(IXLWorksheet ws, int r, string title, XLColor accent, int cols)
        {
            ws.Range(r, 1, r, cols).Merge().Value = title;
            ws.Range(r, 1, r, cols).Style
                .Font.SetBold().Font.SetFontSize(12).Font.SetFontColor(XLColor.White)
                .Fill.SetBackgroundColor(accent)
                .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            ws.Row(r).Height = 22;
            return r + 1;
        }

        private static int KeyValueSection(
            IXLWorksheet ws,
            int r,
            string title,
            string leftHeader,
            Dictionary<string, double> data,
            XLColor accent,
            XLColor border
        )
        {
            r = SectionTitle(ws, r, title, accent, 6);

            ws.Cell(r, 1).Value = leftHeader;
            ws.Cell(r, 2).Value = "Count";
            StyleHeader(ws.Range(r, 1, r, 2), XLColor.FromHtml("#F5F3FF"), border);
            r++;

            var rows = (data ?? new Dictionary<string, double>()).ToList();
            foreach (var kv in rows)
            {
                ws.Cell(r, 1).Value = kv.Key;
                ws.Cell(r, 2).Value = kv.Value;
                ws.Cell(r, 2).Style.NumberFormat.Format = "0";
                StyleRow(ws.Range(r, 1, r, 2), XLColor.FromHtml("#F8F9FC"), border, zebra: (r % 2 == 0));
                r++;
            }

            r++;
            return r;
        }

        private static void StyleHeader(IXLRange rng, XLColor fill, XLColor border)
        {
            rng.Style.Font.SetBold();
            rng.Style.Fill.SetBackgroundColor(fill);
            rng.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            rng.Style.Border.OutsideBorderColor = border;
            rng.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            rng.Style.Border.InsideBorderColor = border;
        }

        private static void StyleRow(IXLRange rng, XLColor baseFill, XLColor border, bool zebra)
        {
            rng.Style.Fill.SetBackgroundColor(zebra ? XLColor.White : baseFill);
            rng.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            rng.Style.Border.OutsideBorderColor = border;
            rng.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            rng.Style.Border.InsideBorderColor = border;
        }
    }
}