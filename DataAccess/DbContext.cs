using Microsoft.EntityFrameworkCore;
using Tradier.Client.Models.Account;  // Adjust this based on where your models are actually located
//using Tradier.Client.Models.Account.OrdersFromPy;  // Adjust this based on where your models are actually located

public class TradierDbContext : DbContext
{

    public TradierDbContext(DbContextOptions<TradierDbContext> options)
    : base(options)
    {
    }

    public DbSet<Balances> Balances { get; set; }
    //public DbSet<TradierOrder> TradierOrder { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<Strade> Strades { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Strade>()
            .HasMany(s => s.Orders)
            .WithOne()
            .HasForeignKey(o => o.StradeId);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.Legs) // Assumes Legs is a collection in Order
            .WithOne()
            .HasForeignKey(l => l.OrderId)
            .OnDelete(DeleteBehavior.Restrict);  // <-- Add this line



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
