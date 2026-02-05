using System;
using System.Collections.Generic;
using System.Text;

namespace IRIS.Domain
{
    public class IngredientMock
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int CurrentStock { get; set; }
        public int MinimumStock { get; set; }
        public string Status { get; set; }
    }
}
