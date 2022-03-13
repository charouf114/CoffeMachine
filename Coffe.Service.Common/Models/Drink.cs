using System;
using System.ComponentModel.DataAnnotations;

namespace Coffe.Service.Common
{
    public class Drink
    {
        [Required]
        [Key]
        public string BudgetId { get; set; }

        public DrinkType DrinkType { get; set; }

        public int SucreCount { get; set; }

        public bool HasMug { get; set; }

       public DateTime? CreationDate { get; set; }

        public Drink (string budgetId, DrinkType drinkType, int sucreCount, bool hasMug)
        {
            this.BudgetId = budgetId;
            this.DrinkType = drinkType;
            this.SucreCount = sucreCount;
            this.HasMug = hasMug;
        }
        public Drink() { }

    }
}
