using Coffe.Service.Common;

namespace CoffeApp
{
    public class DrinkTypeWrapper
    {
        public string Value { get; set; }

        public DrinkType? getCoffeType()
        {
            if (Value == "Chocolat")
                return DrinkType.Chocolat;
            if (Value == "Coffe")
                return DrinkType.Coffe;
            if (Value == "The")
                return DrinkType.The;
            return null;
        }

    }
}
