using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProject.EFDB.Migrations
{
    /// <inheritdoc />
    public partial class correctPlan1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoneyTurnover",
                table: "ShopPlans");

            migrationBuilder.RenameColumn(
                name: "TradeTurnover",
                table: "ShopPlans",
                newName: "PurchasesCount");

            migrationBuilder.AddColumn<decimal>(
                name: "AllProfit",
                table: "ShopPlans",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ClearProfit",
                table: "ShopPlans",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllProfit",
                table: "ShopPlans");

            migrationBuilder.DropColumn(
                name: "ClearProfit",
                table: "ShopPlans");

            migrationBuilder.RenameColumn(
                name: "PurchasesCount",
                table: "ShopPlans",
                newName: "TradeTurnover");

            migrationBuilder.AddColumn<int>(
                name: "MoneyTurnover",
                table: "ShopPlans",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
