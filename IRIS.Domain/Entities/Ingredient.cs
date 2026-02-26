using System.ComponentModel.DataAnnotations;
using IRIS.Domain.Enums; 

namespace IRIS.Domain.Entities
{
    public class Ingredient
    {
        [Key]  public int IngredientId { get; set; }

        [Required, MaxLength(100)] public string? Name { get; set; }
        [Required] public Categories Category { get; set; }

        [Required] public Units Unit { get; set; }

        [Required] public decimal CurrentStock { get; set; }

        [Required] public decimal MinimumStock { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}