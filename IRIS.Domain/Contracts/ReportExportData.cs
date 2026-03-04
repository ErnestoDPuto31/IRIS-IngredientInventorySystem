using System;
using System.Collections.Generic;
using System.Text;
using IRIS.Domain.Entities;

namespace IRIS.Domain.Contracts
{
    public class ReportExportData
    {
        public int TotalIngredients { get; set; }
        public int TotalRequests { get; set; }
        public int TotalTransactions { get; set; }
        public double ApprovalRate { get; set; }

        public List<TopIngredientItem> TopIngredients { get; set; } = new();
        public List<LowStockItem> LowStock { get; set; } = new();
        public byte[]? InventoryChartPng { get; set; }
        public byte[]? RequestsChartPng { get; set; }
        public byte[]? CategoryChartPng { get; set; }
    }
}
