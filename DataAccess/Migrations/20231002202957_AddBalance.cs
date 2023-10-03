using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cash",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CashAvailable = table.Column<float>(type: "real", nullable: false),
                    Sweep = table.Column<int>(type: "int", nullable: false),
                    UnsettledFunds = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cash", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Margin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_Margin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatternDayTrader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FedCall = table.Column<int>(type: "int", nullable: false),
                    MaintenanceCall = table.Column<int>(type: "int", nullable: false),
                    OptionBuyingPower = table.Column<float>(type: "real", nullable: false),
                    StockBuyingPower = table.Column<float>(type: "real", nullable: false),
                    StockShortValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatternDayTrader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Balances",
                columns: table => new
                {
                    AccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OptionShortValue = table.Column<float>(type: "real", nullable: false),
                    TotalEquity = table.Column<float>(type: "real", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Balances", x => x.AccountNumber);
                    table.ForeignKey(
                        name: "FK_Balances_Cash_CashId",
                        column: x => x.CashId,
                        principalTable: "Cash",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Balances_Margin_MarginId",
                        column: x => x.MarginId,
                        principalTable: "Margin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Balances_PatternDayTrader_PatternDayTraderId",
                        column: x => x.PatternDayTraderId,
                        principalTable: "PatternDayTrader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Balances");

            migrationBuilder.DropTable(
                name: "Cash");

            migrationBuilder.DropTable(
                name: "Margin");

            migrationBuilder.DropTable(
                name: "PatternDayTrader");
        }
    }
}
