using Coffe.Service.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CoffeServices
{
    public interface IApplicationDbContext
    {
        DbSet<Drink> Drink { get; set; }

        Task<int> SaveChanges();
    }
}