using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradier.Data.Migrations
{
    /// <inheritdoc />
    public partial class Ini : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cash",
                columns: table => new
                {
                    sId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CashAvailable = table.Column<float>(type: "real", nullable: false),
                    Sweep = table.Column<int>(type: "int", nullable: false),
                    UnsettledFunds = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cash", x => x.sId);
                });

            migrationBuilder.CreateTable(
                name: "Margin",
                columns: table => new
                {
                    sId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FedCall = table.Column<int>(type: "int", nullable: false),
                    MaintenanceCall = table.Column<int>(type: "int", nullable: false),
                    OptionBuyingPower = table.Column<float>(type: "real", nullable: false),
                    StockBuyingPower = table.Column<float>(type: "real", nullable: false),
                    StockShortValue = table.Column<int>(type: "int", nullable: false),
                    Sweep = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Margin", x => x.sId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    DatabaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Side = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    AvgFillPrice = table.Column<float>(type: "real", nullable: false),
                    ExecQuantity = table.Column<float>(type: "real", nullable: false),
                    LastFillPrice = table.Column<float>(type: "real", nullable: false),
                    LastFillQuantity = table.Column<float>(type: "real", nullable: false),
                    RemainingQuantity = table.Column<float>(type: "real", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumLegs = table.Column<int>(type: "int", nullable: false),
                    Strategy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.DatabaseId);
                });

            migrationBuilder.CreateTable(
                name: "PatternDayTrader",
                columns: table => new
                {
                    sId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FedCall = table.Column<int>(type: "int", nullable: false),
                    MaintenanceCall = table.Column<int>(type: "int", nullable: false),
                    OptionBuyingPower = table.Column<float>(type: "real", nullable: false),
                    StockBuyingPower = table.Column<float>(type: "real", nullable: false),
                    StockShortValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatternDayTrader", x => x.sId);
                });

            migrationBuilder.CreateTable(
                name: "Strades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TOSNotation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Strike = table.Column<int>(type: "int", nullable: false),
                    CallPut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QtyContractsOpen = table.Column<int>(type: "int", nullable: false),
                    QtyContractsClosed = table.Column<int>(type: "int", nullable: false),
                    PNLOpen = table.Column<float>(type: "real", nullable: false),
                    PNLClosed = table.Column<float>(type: "real", nullable: false),
                    MaxProfitYetToGain = table.Column<float>(type: "real", nullable: false),
                    Expry = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leg",
                columns: table => new
                {
                    DatabaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatabaseOrderId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Side = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    AvgFillPrice = table.Column<float>(type: "real", nullable: false),
                    ExecQuantity = table.Column<float>(type: "real", nullable: false),
                    LastFillPrice = table.Column<float>(type: "real", nullable: false),
                    LastFillQuantity = table.Column<float>(type: "real", nullable: false),
                    RemainingQuantity = table.Column<float>(type: "real", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leg", x => x.DatabaseId);
                    table.ForeignKey(
                        name: "FK_Leg_Orders_DatabaseOrderId",
                        column: x => x.DatabaseOrderId,
                        principalTable: "Orders",
                        principalColumn: "DatabaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Balances",
                columns: table => new
                {
                    sId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionShortValue = table.Column<float>(type: "real", nullable: false),
                    DateInserted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalEquity = table.Column<float>(type: "real", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosePL = table.Column<float>(type: "real", nullable: false),
                    CurrentRequirement = table.Column<float>(type: "real", nullable: false),
                    Equity = table.Column<int>(type: "int", nullable: false),
                    LongMarketValue = table.Column<float>(type: "real", nullable: false),
                    MarketValue = table.Column<float>(type: "real", nullable: false),
                    OpenPL = table.Column<float>(type: "real", nullable: false),
                    OptionLongValue = table.Column<float>(type: "real", nullable: false),
                    OptionRequirement = table.Column<float>(type: "real", nullable: false),
                    PendingOrdersCount = table.Column<int>(type: "int", nullable: false),
                    ShortMarketValue = table.Column<float>(type: "real", nullable: false),
                    StockLongValue = table.Column<float>(type: "real", nullable: false),
                    TotalCash = table.Column<float>(type: "real", nullable: false),
                    UnclearedFunds = table.Column<int>(type: "int", nullable: false),
                    PendingCash = table.Column<float>(type: "real", nullable: false),
                    MarginId = table.Column<int>(type: "int", nullable: false),
                    CashId = table.Column<int>(type: "int", nullable: false),
                    PatternDayTraderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balances", x => x.sId);
                    table.ForeignKey(
                        name: "FK_Balances_Cash_CashId",
                        column: x => x.CashId,
                        principalTable: "Cash",
                        principalColumn: "sId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Balances_Margin_MarginId",
                        column: x => x.MarginId,
                        principalTable: "Margin",
                        principalColumn: "sId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Balances_PatternDayTrader_PatternDayTraderId",
                        column: x => x.PatternDayTraderId,
                        principalTable: "PatternDayTrader",
                        principalColumn: "sId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StradeFly",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    StradeId = table.Column<int>(type: "int", nullable: false),
                    Strike = table.Column<int>(type: "int", nullable: false),
                    QtyContractsOpen = table.Column<int>(type: "int", nullable: false),
                    QtyContractsClosed = table.Column<int>(type: "int", nullable: false),
                    CostBasis = table.Column<float>(type: "real", nullable: false),
                    PNLOpen = table.Column<float>(type: "real", nullable: false),
                    PNLClosed = table.Column<float>(type: "real", nullable: false),
                    CallPut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expry = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StradeFly", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StradeFly_Strades_Id",
                        column: x => x.Id,
                        principalTable: "Strades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brokerId = table.Column<int>(type: "int", nullable: false),
                    StradeId = table.Column<int>(type: "int", nullable: true),
                    CreditDebit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumLegs = table.Column<int>(type: "int", nullable: false),
                    NumContracts = table.Column<int>(type: "int", nullable: false),
                    Strategy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpenClosed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    TOSString = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SOrders_StradeFly_StradeId",
                        column: x => x.StradeId,
                        principalTable: "StradeFly",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SLeg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brokerId = table.Column<int>(type: "int", nullable: false),
                    brokerOrderId = table.Column<int>(type: "int", nullable: false),
                    orderId = table.Column<int>(type: "int", nullable: false),
                    BuySell = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpenClose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityOrdered = table.Column<float>(type: "real", nullable: false),
                    QuantityExecuted = table.Column<float>(type: "real", nullable: false),
                    RatioThisLegToTotalQty = table.Column<float>(type: "real", nullable: false),
                    RatioThisLegToTotalPNL = table.Column<float>(type: "real", nullable: false),
                    CostBasis = table.Column<float>(type: "real", nullable: false),
                    ThisFillPrice = table.Column<float>(type: "real", nullable: false),
                    ThisFillQuantity = table.Column<float>(type: "real", nullable: false),
                    RemainingQuantity = table.Column<float>(type: "real", nullable: false),
                    OptionSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Underlying = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Strike = table.Column<int>(type: "int", nullable: false),
                    CallPut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expry = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLeg", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SLeg_SOrders_orderId",
                        column: x => x.orderId,
                        principalTable: "SOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Balances_AccountNumber",
                table: "Balances",
                column: "AccountNumber",
                unique: true,
                filter: "[AccountNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Balances_CashId",
                table: "Balances",
                column: "CashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Balances_MarginId",
                table: "Balances",
                column: "MarginId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Balances_PatternDayTraderId",
                table: "Balances",
                column: "PatternDayTraderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leg_DatabaseOrderId",
                table: "Leg",
                column: "DatabaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SLeg_orderId",
                table: "SLeg",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_SOrders_StradeId",
                table: "SOrders",
                column: "StradeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Balances");

            migrationBuilder.DropTable(
                name: "Leg");

            migrationBuilder.DropTable(
                name: "SLeg");

            migrationBuilder.DropTable(
                name: "Cash");

            migrationBuilder.DropTable(
                name: "Margin");

            migrationBuilder.DropTable(
                name: "PatternDayTrader");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "SOrders");

            migrationBuilder.DropTable(
                name: "StradeFly");

            migrationBuilder.DropTable(
                name: "Strades");
        }
    }
}
