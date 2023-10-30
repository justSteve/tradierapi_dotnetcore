using Microsoft.EntityFrameworkCore;
using Tradier.Client.Models.Account;  // Adjust this based on where your models are actually located
using Tradier.Interfaces;
using Tradier.Entities.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Tradier.Data
{
    public class TradierDbContext : DbContext, ITradierDbContext
    {

        public TradierDbContext(DbContextOptions<TradierDbContext> options)
        : base(options)
        {
        }

        //dotnet ef migrations add Ini -p ..\DataAccess\Tradier.Data.csproj -s cli.csproj
        //dotnet ef database update -c TradierDbContext
        //sqlcmd -S MSSQLLocalDB -E  -Q "DROP DATABASE OptionsTracker"


        public DbSet<Balances> Balances { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<SOrder> SOrders { get; set; }
        public DbSet<Strade> Strades { get; set; }
        public DbSet<StradeFly> StradeFly { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasKey(o => o.DatabaseId);  // Use DatabaseId as the primary key

            modelBuilder.Entity<Leg>()
                .HasKey(l => l.DatabaseId);  // Use DatabaseId as the primary key

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Legs)
                .WithOne()
                .HasForeignKey(l => l.DatabaseOrderId)  // Use DatabaseOrderId as the foreign key
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Strade>()
                .HasMany(s => s.Flies)
                .WithOne()
                 .HasForeignKey(f => f.Id);

            modelBuilder.Entity<StradeFly>()
                .HasMany(s => s.SOrders)
                .WithOne()
                .HasForeignKey(o => o.StradeFlyId)
                .IsRequired(false);  // Indicate that the foreign key is optional


            modelBuilder.Entity<SOrder>()
                    .HasMany(o => o.SLegs) // Assumes Legs is a collection in Order
                    .WithOne()
                    .HasForeignKey(l => l.orderId)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SLeg>()
                    .Property(p => p.Id)
                    .ValueGeneratedOnAdd();

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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    SetupWarnings(optionsBuilder);
        //}

        //public void SetupWarnings(DbContextOptionsBuilder optionsBuilder = null)
        //{
        //    optionsBuilder?.ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
        //}
    }
}
