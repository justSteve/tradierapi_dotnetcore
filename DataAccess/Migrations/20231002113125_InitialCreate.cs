using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    PendingCash = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balances", x => x.AccountNumber);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Balances");
        }
    }
}
