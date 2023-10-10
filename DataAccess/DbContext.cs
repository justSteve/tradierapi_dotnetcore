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

        modelBuilder.Entity<Balances>()
            .HasOne(b => b.Margin)
            .WithOne()
            .HasForeignKey<Balances>(b => b.MarginId);

        modelBuilder.Entity<Balances>()
            .HasOne(b => b.Cash)
            .WithOne()
            .HasForeignKey<Balances>(b => b.CashId);

        modelBuilder.Entity<Balances>()
            .HasOne(b => b.PatternDayTrader)
            .WithOne()
            .HasForeignKey<Balances>(b => b.PatternDayTraderId);

        modelBuilder.Entity<Balances>()
            .HasIndex(b => b.AccountNumber)
            .IsUnique();

        modelBuilder.Entity<Balances>()
            .HasKey(b => b.sId);  // Explicitly set the primary key

    }

}
