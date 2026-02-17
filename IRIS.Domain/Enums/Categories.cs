using System.ComponentModel.DataAnnotations;

namespace IRIS.Domain.Enums
{
    public enum Categories
    {
        Produce,
        Protein,
        [Display(Name ="Dairy & Eggs")] DairyAndEggs,
        [Display(Name = "Pantry & Staples")] PantryAndStaples,
        [Display(Name = "Spices & Seasonings")]SpicesAndSeasonings,
        [Display(Name = "Condiments & Oils")] CondimentsAndOils,
        [Display(Name = "Grains & Legumes")] GrainsALegumes,
        [Display(Name = "Bakery & Sweets")] BakeryAndSweets,
        Beverages,
        [Display(Name = "Frozen & Prepared")] FrozenAndPrepared,
    }
}
