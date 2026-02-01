    using System.ComponentModel.DataAnnotations;

namespace IRIS.Domain.Entities
{
    public class InventoryLog
    {
        [Key] public int InventoryLogId { get; set; }
        [Required] public int IngredientId { get; set; }

        [Required] public decimal QuantityChanged { get; set; }
        [Required, MaxLength(50)] public string? ActionType { get; set; }
        [Required] public int PerformedByUserId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
