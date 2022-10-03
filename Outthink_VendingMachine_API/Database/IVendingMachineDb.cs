using Microsoft.EntityFrameworkCore;
using Outthink_VendingMachine_API.Models;

namespace Outthink_VendingMachine_API.Database
{
    public interface IVendingMachineDb
    {
        DbSet<Product> Products { get; set; }
        DbSet<Coin> Coins { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}