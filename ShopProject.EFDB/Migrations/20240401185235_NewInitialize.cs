using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShopProject.EFDB.Migrations
{
    /// <inheritdoc />
    public partial class NewInitialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Users_UserId",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Shops_UserId",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Shops");

            migrationBuilder.AddColumn<string>(
                name: "ShopName",
                table: "Shops",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Cashiers",
                columns: table => new
                {
                    CashierId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Cashiers_pkey", x => x.CashierId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    CostPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    SellPrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Products_pkey", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "ShopPlans",
                columns: table => new
                {
                    ShopPlanId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TradeTurnover = table.Column<int>(type: "integer", nullable: false),
                    MoneyTurnover = table.Column<int>(type: "integer", nullable: false),
                    AverageBill = table.Column<decimal>(type: "numeric", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ShopId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ShopPlans_pkey", x => x.ShopPlanId);
                    table.ForeignKey(
                        name: "FK_ShopPlans_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "ShopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopUser",
                columns: table => new
                {
                    ShopsShopId = table.Column<int>(type: "integer", nullable: false),
                    UsersUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopUser", x => new { x.ShopsShopId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_ShopUser_Shops_ShopsShopId",
                        column: x => x.ShopsShopId,
                        principalTable: "Shops",
                        principalColumn: "ShopId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkerPlans",
                columns: table => new
                {
                    WorkerPlanId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PurchasesCount = table.Column<int>(type: "integer", nullable: false),
                    ProductsCount = table.Column<int>(type: "integer", nullable: false),
                    AverageBill = table.Column<decimal>(type: "numeric", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ShopId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("WorkerPlans_pkey", x => x.WorkerPlanId);
                    table.ForeignKey(
                        name: "FK_WorkerPlans_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "ShopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CashierShop",
                columns: table => new
                {
                    CashiersCashierId = table.Column<int>(type: "integer", nullable: false),
                    ShopsShopId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashierShop", x => new { x.CashiersCashierId, x.ShopsShopId });
                    table.ForeignKey(
                        name: "FK_CashierShop_Cashiers_CashiersCashierId",
                        column: x => x.CashiersCashierId,
                        principalTable: "Cashiers",
                        principalColumn: "CashierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashierShop_Shops_ShopsShopId",
                        column: x => x.ShopsShopId,
                        principalTable: "Shops",
                        principalColumn: "ShopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CashierId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Purchases_pkey", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_Purchases_Cashiers_CashierId",
                        column: x => x.CashierId,
                        principalTable: "Cashiers",
                        principalColumn: "CashierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPlans",
                columns: table => new
                {
                    ProductPlanId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    ExpectedQuantity = table.Column<int>(type: "integer", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ShopId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProductPlans_pkey", x => x.ProductPlanId);
                    table.ForeignKey(
                        name: "FK_ProductPlans_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPlans_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "ShopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseProducts",
                columns: table => new
                {
                    PurchaseProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    PurchaseId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PurchaseProducts_pkey", x => x.PurchaseProductId);
                    table.ForeignKey(
                        name: "FK_PurchaseProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseProducts_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashierShop_ShopsShopId",
                table: "CashierShop",
                column: "ShopsShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPlans_ProductId",
                table: "ProductPlans",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPlans_ShopId",
                table: "ProductPlans",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseProducts_ProductId",
                table: "PurchaseProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseProducts_PurchaseId",
                table: "PurchaseProducts",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_CashierId",
                table: "Purchases",
                column: "CashierId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPlans_ShopId",
                table: "ShopPlans",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopUser_UsersUserId",
                table: "ShopUser",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerPlans_ShopId",
                table: "WorkerPlans",
                column: "ShopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashierShop");

            migrationBuilder.DropTable(
                name: "ProductPlans");

            migrationBuilder.DropTable(
                name: "PurchaseProducts");

            migrationBuilder.DropTable(
                name: "ShopPlans");

            migrationBuilder.DropTable(
                name: "ShopUser");

            migrationBuilder.DropTable(
                name: "WorkerPlans");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Cashiers");

            migrationBuilder.DropColumn(
                name: "ShopName",
                table: "Shops");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Shops",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shops_UserId",
                table: "Shops",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Users_UserId",
                table: "Shops",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
