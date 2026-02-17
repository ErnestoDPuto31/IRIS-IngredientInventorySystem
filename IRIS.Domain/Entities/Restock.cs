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

        [ForeignKey("IngredientId")]
        public virtual Ingredient Ingredient { get; set; }

        [Required]
        public StockStatus Status { get; set; } = StockStatus.Empty;

        [Required]
        public DateTime DateRestocked { get; set; } = DateTime.Now;
        [Required]
        public decimal SuggestedRestockQuantity { get; set; }
    }
}