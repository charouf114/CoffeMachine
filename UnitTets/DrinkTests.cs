using Coffe.Service.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class DrinkTests : BaseTests
    {
        [TestMethod]
        public void DrinkWorkflowTest()
        {
            // Scenario
            // 1- Add 10 Drink for 10 differents badges
            // 2- Get Latest Drink for each badge And Assert 
            // 3- Update the half And Assert
            // 4- Check the old  is Good

            // 1- Add 10 Drink for 10 differents badges
            for (int i=0; i< 10; ++i)
            {
                Drink drink = new Drink($"badget_{i}_{i}",
                                    (DrinkType)(i % 3 + 1),
                                    sucreCount: i,
                                    hasMug: i % 2 == 0);

                _ = CoffeController.AddCoffe(drink).Result;
            }

            // 2- Get Latest Drink for each badge And Assert 
            for (int i = 0; i < 10; ++i)
            {
                Drink drink = new Drink($"badget_{i}_{i}",
                                    DrinkType.Chocolat,
                                    sucreCount : 0,
                                    hasMug: false);

                var lastDrink = GetObjectResultContent<Drink>(CoffeController.GetLatestCoffe(drink).Result);

                Assert.AreEqual((DrinkType)(i % 3 + 1), lastDrink.DrinkType);
                Assert.AreEqual(i, lastDrink.SucreCount);
                Assert.AreEqual(i % 2 == 0, lastDrink.HasMug);
            }

            // 3- Update the half And Assert
            for (int i = 0; i < 5; ++i)
            {
                Drink drink = new Drink($"badget_{i}_{i}",
                                    DrinkType.Chocolat,
                                    sucreCount: 0,
                                    hasMug: false);

                _ = CoffeController.AddCoffe(drink).Result;

                var lastDrink = GetObjectResultContent<Drink>(CoffeController.GetLatestCoffe(drink).Result);
                Assert.AreEqual(DrinkType.Chocolat, lastDrink.DrinkType);
                Assert.AreEqual(0, lastDrink.SucreCount);
                Assert.IsFalse(lastDrink.HasMug);
            }

            // 4- Check the old  is Good
            for (int i = 5; i < 10; ++i)
            {
                Drink drink = new Drink($"badget_{i}_{i}",
                                    DrinkType.Chocolat,
                                    sucreCount: 0,
                                    hasMug: false);

                var lastDrink = GetObjectResultContent<Drink>(CoffeController.GetLatestCoffe(drink).Result);
                Assert.AreEqual((DrinkType)(i % 3 + 1), lastDrink.DrinkType);
                Assert.AreEqual(i, lastDrink.SucreCount);
                Assert.AreEqual(i % 2 == 0, lastDrink.HasMug);
            }
        }
    }
}
