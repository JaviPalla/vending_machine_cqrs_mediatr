using Microsoft.EntityFrameworkCore;
using Outthink_VendingMachine_API.Models;

namespace Outthink_VendingMachine_API.Database
{
    public class VendingMachineDb : DbContext, IVendingMachineDb
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Coin> Coins { get; set; }

        public VendingMachineDb(DbContextOptions options) : base(options)
        {
            LoadProducts();
            LoadCoins();
            base.SaveChangesAsync();
        }

        public void LoadProducts()
        {
            // Database initial data
            Products.AddRange(new List<Product>
            {
                new Product { Id = 1, Name = "Tea", Price = 1.3, Quantity = 10 },
                new Product { Id = 2, Name = "Espresso", Price = 1.8, Quantity = 20 },
                new Product { Id = 3, Name = "Juice", Price = 1.8, Quantity = 20 },
                new Product { Id = 4, Name = "Chicken soup", Price = 1.8, Quantity = 15 },
            });
        }

        public void LoadCoins() { 
            Coins.AddRange(new List<Coin>
            {
                new Coin { Value = 0.1, Quantity = 100 },
                new Coin { Value = 0.2, Quantity = 100 },
                new Coin { Value = 0.5, Quantity = 100 },
                new Coin { Value = 1, Quantity = 100 }
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
