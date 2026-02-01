using System.ComponentModel.DataAnnotations;

namespace IRIS.Domain.Entities
{
    public class Ingredient
    {
        [Key] public int IngredientId { get; set; }
        [Required, MaxLength(100)] public string? Name { get; set; }
        [Required, MaxLength(50)] public string? Category { get; set; }
        [Required, MaxLength(20)] public string? Unit { get; set; }
        [Required] public decimal CurrentStock { get; set; }
        [Required] public decimal MinimumStock { get; set; }
        [Required] public DateTime UpdatedAt { get; set; } 
    }
}
