using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Infrastructure.Persistence.Migrations
{
    public partial class RemovePriceCurrencyFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceCurrency",
                table: "SaleTransactions");

            migrationBuilder.DropColumn(
                name: "ProductPriceCurrency",
                table: "SaleTransactionProducts");

            migrationBuilder.DropColumn(
                name: "PriceCurrency",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PriceCurrency",
                table: "SaleTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductPriceCurrency",
                table: "SaleTransactionProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PriceCurrency",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}