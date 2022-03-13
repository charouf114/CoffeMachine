using Microsoft.AspNetCore.Mvc;
using System;
using Coffe.Service.Common;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CoffeServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoffeController : ControllerBase
    {
        private IApplicationDbContext _context;
        public CoffeController(IApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<String> Get()
        {
            return Ok(" Welcome To The Jungle :)");
        }

        [HttpPost]
        [Route("LastCoffe")]
        public async Task<ActionResult<Drink>> GetLatestCoffe([FromBody] Drink drink)
        {
            var latestDrink = await GetLatestDrink(drink);
            if (latestDrink == null)
            {
                return NotFound();
            }
            return Ok(latestDrink);

        }

        [HttpPost]
        [Route("AddCoffe")]
        public async Task<ActionResult<Drink>> AddCoffe([FromBody] Drink drink)
        {
            var latestDrink = await GetLatestDrink(drink);
            
            if (latestDrink == null)
            {
                drink.CreationDate = DateTime.UtcNow;
                _context.Drink.Add(drink);          
            }
            else
            {
                // For Now We Can Update the value Only 
                // We Don't need All the history :)
                latestDrink.CreationDate = DateTime.UtcNow;
                latestDrink.DrinkType = drink.DrinkType;
                latestDrink.SucreCount = drink.SucreCount;
                latestDrink.HasMug = drink.HasMug;
            }
            await _context.SaveChanges();
            return Ok(drink);
        }

        private async Task<Drink> GetLatestDrink(Drink drink)
        {
            return  await _context.Drink.Where(d => d.BudgetId == drink.BudgetId)
                            .OrderByDescending(d => d.CreationDate)
                            .FirstOrDefaultAsync();
        }
    }
}
