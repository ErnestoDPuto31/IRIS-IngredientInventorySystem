using System;
using System.IO;
using IRIS.Domain.Contracts;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace IRIS.Domain.Helpers
{
    public class QuestReportPdfExporter : IReportPdfExporter
    {
        public void Export(ReportExportData data, string filePath)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var doc = new ReportsDocument(data);
            doc.GeneratePdf(filePath);
        }

        private sealed class ReportsDocument : IDocument
        {
            private readonly ReportExportData _d;
            public ReportsDocument(ReportExportData d) => _d = d;

            public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

            public void Compose(IDocumentContainer container)
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(24);
                    page.DefaultTextStyle(x => x.FontFamily("Segoe UI").FontSize(10));

                    page.Header().Text("reports and analytics").FontSize(18).SemiBold();
                    page.Content().Column(col =>
                    {
                        col.Spacing(12);

                        // summary
                        col.Item().Row(r =>
                        {
                            r.Spacing(10);
                            r.RelativeItem().Element(c => Stat(c, "total ingredients", _d.TotalIngredients.ToString()));
                            r.RelativeItem().Element(c => Stat(c, "total requests", _d.TotalRequests.ToString()));
                            r.RelativeItem().Element(c => Stat(c, "approval rate", $"{_d.ApprovalRate:0.0}%"));
                            r.RelativeItem().Element(c => Stat(c, "transactions", _d.TotalTransactions.ToString()));
                        });

                        // charts
                        col.Item().Element(c => Chart(c, "inventory", _d.InventoryChartPng));
                        col.Item().Element(c => Chart(c, "requests", _d.RequestsChartPng));
                        col.Item().Element(c => Chart(c, "category", _d.CategoryChartPng));

                        // top ingredients
                        col.Item().Element(c => TopIngredientsTable(c));

                        // low stock
                        col.Item().Element(c => LowStockTable(c));
                    });
                });
            }

            private void Stat(IContainer c, string title, string value)
            {
                c.Border(1).BorderColor(Colors.Grey.Lighten2).Padding(10).CornerRadius(10).Column(col =>
                {
                    col.Item().Text(title).FontColor(Colors.Grey.Darken1);
                    col.Item().Text(value).FontSize(16).SemiBold();
                });
            }

            private void Chart(IContainer c, string title, byte[]? png)
            {
                c.Border(1).BorderColor(Colors.Grey.Lighten2).Padding(10).CornerRadius(10).Column(col =>
                {
                    col.Item().Text(title).SemiBold();
                    if (png == null || png.Length == 0)
                        col.Item().Text("no chart").FontColor(Colors.Grey.Medium);
                    else
                        col.Item().Height(200).Image(png).FitArea();
                });
            }

            private void TopIngredientsTable(IContainer c)
            {
                c.Border(1).BorderColor(Colors.Grey.Lighten2).Padding(10).CornerRadius(10).Column(col =>
                {
                    col.Item().Text("top 5 most used ingredients").SemiBold();
                    col.Item().PaddingTop(6).Table(t =>
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
                            h.Cell().Text("name").SemiBold();
                            h.Cell().Text("category").SemiBold();
                            h.Cell().Text("total used").SemiBold();
                            h.Cell().Text("unit").SemiBold();
                        });

                        foreach (var x in _d.TopIngredients)
                        {
                            t.Cell().Text(x.Name);
                            t.Cell().Text(x.Category);
                            t.Cell().Text(x.TotalUsed.ToString());
                            t.Cell().Text(x.Unit);
                        }
                    });
                });
            }

            private void LowStockTable(IContainer c)
            {
                c.Border(1).BorderColor(Colors.Grey.Lighten2).Padding(10).CornerRadius(10).Column(col =>
                {
                    col.Item().Text("low stock alerts").SemiBold();
                    col.Item().PaddingTop(6).Table(t =>
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
                            h.Cell().Text("name").SemiBold();
                            h.Cell().Text("category").SemiBold();
                            h.Cell().Text("stock").SemiBold();
                            h.Cell().Text("min").SemiBold();
                            h.Cell().Text("unit").SemiBold();
                        });

                        foreach (var x in _d.LowStock)
                        {
                            t.Cell().Text(x.Name);
                            t.Cell().Text(x.Category);
                            t.Cell().Text(x.Stock.ToString("0.##"));
                            t.Cell().Text(x.Min.ToString("0.##"));
                            t.Cell().Text(x.Unit);
                        }
                    });
                });
            }
        }
    }
}