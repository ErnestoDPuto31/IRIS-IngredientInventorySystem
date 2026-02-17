using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRIS.Domain.Entities
{
    public class RequestDetails
    {
        [Key] public int RequestDetailsId { get; set; }
        public int RequestId { get; set; }
        public int IngredientId { get; set; }
        [Required]
        public decimal PortionPerStudent { get; set; }
        [Required]
        public decimal RequestedQty { get; set; }
        [Required]
        public decimal AllowedQty { get; set; }

        [ForeignKey("IngredientId")] public virtual Ingredient? Ingredient { get; set; }
        [ForeignKey("RequestId")] public virtual Request? Request { get; set; }

        [NotMapped]
        public bool IsOverLimit => RequestedQty > AllowedQty;
    }
}