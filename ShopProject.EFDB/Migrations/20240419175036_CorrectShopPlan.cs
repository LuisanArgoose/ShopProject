using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShopProject.EFDB.Migrations
{
    /// <inheritdoc />
    public partial class CorrectShopPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllProfit",
                table: "ShopPlans");

            migrationBuilder.DropColumn(
                name: "AverageBill",
                table: "ShopPlans");

            migrationBuilder.DropColumn(
                name: "ClearProfit",
                table: "ShopPlans");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "ShopPlans");

            migrationBuilder.RenameColumn(
                name: "PurchasesCount",
                table: "ShopPlans",
                newName: "PlanAtributeId");

            migrationBuilder.AddColumn<int>(
                name: "AtributeValue",
                table: "ShopPlans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SetTime",
                table: "ShopPlans",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "PlanAtributes",
                columns: table => new
                {
                    PlanAtributeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AtributeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanAtributes", x => x.PlanAtributeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopPlans_PlanAtributeId",
                table: "ShopPlans",
                column: "PlanAtributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopPlans_PlanAtributes_PlanAtributeId",
                table: "ShopPlans",
                column: "PlanAtributeId",
                principalTable: "PlanAtributes",
                principalColumn: "PlanAtributeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopPlans_PlanAtributes_PlanAtributeId",
                table: "ShopPlans");

            migrationBuilder.DropTable(
                name: "PlanAtributes");

            migrationBuilder.DropIndex(
                name: "IX_ShopPlans_PlanAtributeId",
                table: "ShopPlans");

            migrationBuilder.DropColumn(
                name: "AtributeValue",
                table: "ShopPlans");

            migrationBuilder.DropColumn(
                name: "SetTime",
                table: "ShopPlans");

            migrationBuilder.RenameColumn(
                name: "PlanAtributeId",
                table: "ShopPlans",
                newName: "PurchasesCount");

            migrationBuilder.AddColumn<decimal>(
                name: "AllProfit",
                table: "ShopPlans",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageBill",
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

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "ShopPlans",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
