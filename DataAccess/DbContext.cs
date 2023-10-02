using Microsoft.EntityFrameworkCore;
using Tradier.Client.Models.Account;  // Adjust this based on where your models are actually located

public class TradierDbContext : DbContext
{

    public TradierDbContext(DbContextOptions<TradierDbContext> options)
    : base(options)
    {
    }

    public DbSet<Balances> Balances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define primary key for Balances
        modelBuilder.Entity<Balances>().HasKey(b => b.AccountNumber);

        // Ignore other classes if they should not be entities
        modelBuilder.Ignore<Margin>();
        modelBuilder.Ignore<Cash>();
        modelBuilder.Ignore<PatternDayTrader>();
        modelBuilder.Ignore<BalanceRootObject>();
    }
}
