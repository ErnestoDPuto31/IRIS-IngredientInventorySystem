using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRIS.Domain.Entities
{
    public class RequestItem
    {
        [Key]
        public int RequestItemId { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public int IngredientId { get; set; }

        [Required]
        public decimal RequestedQty { get; set; }

        [Required]
        public decimal AllowedQty { get; set; }

        // NAVIGATION PROPERTIES

        // ingredient id
        [ForeignKey("IngredientId")]
        public virtual Ingredient Ingredient { get; set; }

        // requestid
        [ForeignKey("RequestId")]
        public virtual Request Request { get; set; }
    }
}