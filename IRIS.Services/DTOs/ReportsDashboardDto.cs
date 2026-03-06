using IRIS.Domain.Entities;
using System.Collections.Generic;

namespace IRIS.Services.DTOs
{
    public class ReportsDashboardDto
    {
        public int TotalIngredients { get; set; }
        public int TotalRequests { get; set; }
        public int TotalTransactions { get; set; }
        public double ApprovalRate { get; set; }

        public Dictionary<string, double> InventoryStats { get; set; } = new Dictionary<string, double>();
        public Dictionary<string, double> RequestStats { get; set; } = new Dictionary<string, double>();
        public Dictionary<string, double> CategoryStats { get; set; } = new Dictionary<string, double>();

        public List<LowStockItem> LowStockIngredients { get; set; } = new List<LowStockItem>();
        public List<TopIngredientItem> TopUsedIngredients { get; set; } = new List<TopIngredientItem>();
    }
}