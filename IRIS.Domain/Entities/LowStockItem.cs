using System;
using System.Collections.Generic;
using System.Text;

namespace IRIS.Domain.Entities
{
    public class LowStockItem
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public float Stock { get; set; }
        public float Min { get; set; }
        public string Unit { get; set; }

        public LowStockItem(string n, string c, float s, float m, string u)
        {
            Name = n; Category = c; Stock = s; Min = m; Unit = u;
        }
    }
}