using Coffe.Service.Common;
using CoffeServices;
using CoffeServices.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    public class BaseTests
    {
        private CoffeController _coffeController;

        public object coffeControllerLock = new object();

        public IApplicationDbContext mockDbcontext;

        public CoffeController CoffeController { 
            get
            {
                if(_coffeController == null)
                {
                    lock (coffeControllerLock)
                    {
                        if (_coffeController == null)
                        {
                            _coffeController = new CoffeController(mockDbcontext);
                        }
                    }
                }
                return _coffeController;
            }
        }

        public BaseTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "DrinkListDatabase")
           .Options;

            mockDbcontext = new ApplicationDbContext(options);
        }

        public static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}
