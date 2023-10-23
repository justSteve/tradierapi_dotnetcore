using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tradier.Entities.Models;

namespace Tradier.Interfaces
{
    public interface ITradierDbContext
    {
        public DbSet<Strade> Strades { get; set; } 
        public DbSet<Balances> Balances { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<SOrder> SOrders { get; set; }
        public DbSet<StradeFly> StradeFly { get; set; }
        // Add other DbSet properties for other entities as needed

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        // Optionally, if you use more DbContext methods, add them here
    }
}

