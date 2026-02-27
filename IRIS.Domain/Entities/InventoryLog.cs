using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRIS.Domain.Entities
{
    public class InventoryLog
    {
        [Key]
        public int InventoryLogId { get; set; }

        public int? IngredientId { get; set; }

        [Required, MaxLength(100)]
        public string IngredientName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string ActionType { get; set; }

        [Required]
        public decimal QuantityChanged { get; set; }

        [Required]
        public decimal PreviousStock { get; set; }

        [Required]
        public decimal NewStock { get; set; }

        [Required]
        public int PerformedByUserId { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.Now;

        [ForeignKey("IngredientId")]
        public virtual Ingredient? Ingredient { get; set; }

        [ForeignKey("PerformedByUserId")]
        public virtual User? User { get; set; }
    }
}