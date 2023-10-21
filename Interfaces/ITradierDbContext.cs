using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tradier.Entities;

namespace Tradier.Interfaces
{
    public interface ITradierDbContext
    {
        DbSet<Strade> Strades { get; set; } // Assuming you have a Strade entity

        // Add other DbSet properties for other entities as needed

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        // Optionally, if you use more DbContext methods, add them here
    }
}

