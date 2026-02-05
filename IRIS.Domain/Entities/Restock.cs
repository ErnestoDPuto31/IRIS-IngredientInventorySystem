using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IRIS.Domain.Enums;

namespace IRIS.Domain.Entities
{
    public class Restock
    {
        [Key]
        public int RestockId { get; set; }

        [Required]
        public int IngredientId { get; set; }

        // This links this class to the Ingredient class
        [ForeignKey("IngredientId")]
        public virtual Ingredient Ingredient { get; set; }

        [Required, MaxLength(100)]
        public string IngredientName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Category { get; set; } = string.Empty;

        [Required]
        public decimal CurrentStock { get; set; }

        [Required]
        public decimal MinimumThreshold { get; set; }

        [Required]
        public decimal SuggestedRestockQuantity { get; set; }

        [Required]
        public StockStatus Status { get; set; }
    }
}