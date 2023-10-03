﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(TradierDbContext))]
    [Migration("20231002202957_AddBalance")]
    partial class AddBalance
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Balances", b =>
                {
                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CashId")
                        .HasColumnType("int");

                    b.Property<float>("ClosePL")
                        .HasColumnType("real");

                    b.Property<float>("CurrentRequirement")
                        .HasColumnType("real");

                    b.Property<int>("Equity")
                        .HasColumnType("int");

                    b.Property<float>("LongMarketValue")
                        .HasColumnType("real");

                    b.Property<int>("MarginId")
                        .HasColumnType("int");

                    b.Property<float>("MarketValue")
                        .HasColumnType("real");

                    b.Property<float>("OpenPL")
                        .HasColumnType("real");

                    b.Property<float>("OptionLongValue")
                        .HasColumnType("real");

                    b.Property<float>("OptionRequirement")
                        .HasColumnType("real");

                    b.Property<float>("OptionShortValue")
                        .HasColumnType("real");

                    b.Property<int>("PatternDayTraderId")
                        .HasColumnType("int");

                    b.Property<float>("PendingCash")
                        .HasColumnType("real");

                    b.Property<int>("PendingOrdersCount")
                        .HasColumnType("int");

                    b.Property<float>("ShortMarketValue")
                        .HasColumnType("real");

                    b.Property<float>("StockLongValue")
                        .HasColumnType("real");

                    b.Property<float>("TotalCash")
                        .HasColumnType("real");

                    b.Property<float>("TotalEquity")
                        .HasColumnType("real");

                    b.Property<int>("UnclearedFunds")
                        .HasColumnType("int");

                    b.HasKey("AccountNumber");

                    b.HasIndex("CashId")
                        .IsUnique();

                    b.HasIndex("MarginId")
                        .IsUnique();

                    b.HasIndex("PatternDayTraderId")
                        .IsUnique();

                    b.ToTable("Balances");
                });

            modelBuilder.Entity("Cash", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("CashAvailable")
                        .HasColumnType("real");

                    b.Property<int>("Sweep")
                        .HasColumnType("int");

                    b.Property<float>("UnsettledFunds")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Cash");
                });

            modelBuilder.Entity("Margin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FedCall")
                        .HasColumnType("int");

                    b.Property<int>("MaintenanceCall")
                        .HasColumnType("int");

                    b.Property<float>("OptionBuyingPower")
                        .HasColumnType("real");

                    b.Property<float>("StockBuyingPower")
                        .HasColumnType("real");

                    b.Property<int>("StockShortValue")
                        .HasColumnType("int");

                    b.Property<int>("Sweep")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Margin");
                });

            modelBuilder.Entity("PatternDayTrader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FedCall")
                        .HasColumnType("int");

                    b.Property<int>("MaintenanceCall")
                        .HasColumnType("int");

                    b.Property<float>("OptionBuyingPower")
                        .HasColumnType("real");

                    b.Property<float>("StockBuyingPower")
                        .HasColumnType("real");

                    b.Property<int>("StockShortValue")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PatternDayTrader");
                });

            modelBuilder.Entity("Balances", b =>
                {
                    b.HasOne("Cash", "Cash")
                        .WithOne()
                        .HasForeignKey("Balances", "CashId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Margin", "Margin")
                        .WithOne()
                        .HasForeignKey("Balances", "MarginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PatternDayTrader", "PatternDayTrader")
                        .WithOne()
                        .HasForeignKey("Balances", "PatternDayTraderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cash");

                    b.Navigation("Margin");

                    b.Navigation("PatternDayTrader");
                });
#pragma warning restore 612, 618
        }
    }
}
