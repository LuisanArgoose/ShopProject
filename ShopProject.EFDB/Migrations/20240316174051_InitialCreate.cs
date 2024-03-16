using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShopProject.EFDB.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Category_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Categories_pkey", x => x.Category_id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Region_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Region_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Regions_pkey", x => x.Region_id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Role_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Role_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Roles_pkey", x => x.Role_id);
                });

            migrationBuilder.CreateTable(
                name: "Salary_types",
                columns: table => new
                {
                    Salary_type_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Salary_type_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Salary_types_pkey", x => x.Salary_type_id);
                });

            migrationBuilder.CreateTable(
                name: "Shop_types",
                columns: table => new
                {
                    Shop_type_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Shop_type_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Shop_types_pkey", x => x.Shop_type_id);
                });

            migrationBuilder.CreateTable(
                name: "TestTable",
                columns: table => new
                {
                    TestId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TestText = table.Column<string>(type: "text", nullable: false),
                    TextToUpdate = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TestTable_pkey", x => x.TestId);
                });

            migrationBuilder.CreateTable(
                name: "Worker_types",
                columns: table => new
                {
                    Worker_type_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Worker_type_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Worker_types_pkey", x => x.Worker_type_id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Product_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category_id = table.Column<int>(type: "integer", nullable: false),
                    Product_name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Buy_cost = table.Column<decimal>(type: "money", nullable: false),
                    Sell_cost = table.Column<decimal>(type: "money", nullable: false),
                    Barcode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Products_pkey", x => x.Product_id);
                    table.ForeignKey(
                        name: "Products_Category_id_fkey",
                        column: x => x.Category_id,
                        principalTable: "Categories",
                        principalColumn: "Category_id");
                });

            migrationBuilder.CreateTable(
                name: "Region_plans",
                columns: table => new
                {
                    Region_plan_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Region_id = table.Column<int>(type: "integer", nullable: false),
                    Turnover = table.Column<int>(type: "integer", nullable: false),
                    Profit = table.Column<decimal>(type: "money", nullable: false),
                    Start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    End_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Region_plans_pkey", x => x.Region_plan_id);
                    table.ForeignKey(
                        name: "Region_plans_Region_id_fkey",
                        column: x => x.Region_id,
                        principalTable: "Regions",
                        principalColumn: "Region_id");
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Position_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Position_name = table.Column<string>(type: "text", nullable: false),
                    Salary_type_id = table.Column<int>(type: "integer", nullable: false),
                    Role_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Positions_pkey", x => x.Position_id);
                    table.ForeignKey(
                        name: "Positions_Role_id_fkey",
                        column: x => x.Role_id,
                        principalTable: "Roles",
                        principalColumn: "Role_id");
                    table.ForeignKey(
                        name: "Positions_Salary_type_id_fkey",
                        column: x => x.Salary_type_id,
                        principalTable: "Salary_types",
                        principalColumn: "Salary_type_id");
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Shop_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Addres = table.Column<string>(type: "text", nullable: false),
                    Shop_type_id = table.Column<int>(type: "integer", nullable: false),
                    Region_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Shops_pkey", x => x.Shop_id);
                    table.ForeignKey(
                        name: "Shops_Region_id_fkey",
                        column: x => x.Region_id,
                        principalTable: "Regions",
                        principalColumn: "Region_id");
                    table.ForeignKey(
                        name: "Shops_Shop_type_id_fkey",
                        column: x => x.Shop_type_id,
                        principalTable: "Shop_types",
                        principalColumn: "Shop_type_id");
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Worker_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fullname = table.Column<string>(type: "text", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Worker_type_id = table.Column<int>(type: "integer", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Workers_pkey", x => x.Worker_id);
                    table.ForeignKey(
                        name: "Workers_Worker_type_id_fkey",
                        column: x => x.Worker_type_id,
                        principalTable: "Worker_types",
                        principalColumn: "Worker_type_id");
                });

            migrationBuilder.CreateTable(
                name: "Products_in_storage",
                columns: table => new
                {
                    Product_in_storage_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Shop_id = table.Column<int>(type: "integer", nullable: false),
                    Product_id = table.Column<int>(type: "integer", nullable: false),
                    Product_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Products_in_storage_pkey", x => x.Product_in_storage_id);
                    table.ForeignKey(
                        name: "Products_in_storage_Product_id_fkey",
                        column: x => x.Product_id,
                        principalTable: "Products",
                        principalColumn: "Product_id");
                    table.ForeignKey(
                        name: "Products_in_storage_Shop_id_fkey",
                        column: x => x.Shop_id,
                        principalTable: "Shops",
                        principalColumn: "Shop_id");
                });

            migrationBuilder.CreateTable(
                name: "Shop_plans",
                columns: table => new
                {
                    Shop_plan_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Shop_id = table.Column<int>(type: "integer", nullable: false),
                    Turnovet = table.Column<int>(type: "integer", nullable: false),
                    Profit = table.Column<decimal>(type: "money", nullable: false),
                    Start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    End_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Shop_plans_pkey", x => x.Shop_plan_id);
                    table.ForeignKey(
                        name: "Shop_plans_Shop_id_fkey",
                        column: x => x.Shop_id,
                        principalTable: "Shops",
                        principalColumn: "Shop_id");
                });

            migrationBuilder.CreateTable(
                name: "Order_consignments",
                columns: table => new
                {
                    Order_consignment_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Worker_id = table.Column<int>(type: "integer", nullable: false),
                    Date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Accepting_orders_pkey", x => x.Order_consignment_id);
                    table.ForeignKey(
                        name: "Accepting_orders_Worker_id_fkey",
                        column: x => x.Worker_id,
                        principalTable: "Workers",
                        principalColumn: "Worker_id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Payment_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Shop_id = table.Column<int>(type: "integer", nullable: false),
                    Approvier_worker_id = table.Column<int>(type: "integer", nullable: true),
                    Recipient_worker_id = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Is_approved = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Payments_pkey", x => x.Payment_id);
                    table.ForeignKey(
                        name: "Payments_Approvier_worker_id_fkey",
                        column: x => x.Approvier_worker_id,
                        principalTable: "Workers",
                        principalColumn: "Worker_id");
                    table.ForeignKey(
                        name: "Payments_Recipient_worker_id_fkey",
                        column: x => x.Recipient_worker_id,
                        principalTable: "Workers",
                        principalColumn: "Worker_id");
                    table.ForeignKey(
                        name: "Payments_Shop_id_fkey",
                        column: x => x.Shop_id,
                        principalTable: "Shops",
                        principalColumn: "Shop_id");
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Purchase_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Worker_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Purchases_pkey", x => x.Purchase_id);
                    table.ForeignKey(
                        name: "Purchases_Worker_id_fkey",
                        column: x => x.Worker_id,
                        principalTable: "Workers",
                        principalColumn: "Worker_id");
                });

            migrationBuilder.CreateTable(
                name: "Shop_positions",
                columns: table => new
                {
                    Shop_position_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Shop_id = table.Column<int>(type: "integer", nullable: false),
                    Position_id = table.Column<int>(type: "integer", nullable: false),
                    Worker_id = table.Column<int>(type: "integer", nullable: true),
                    Salary = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Shop_positions_pkey", x => x.Shop_position_id);
                    table.ForeignKey(
                        name: "Shop_positions_Position_id_fkey",
                        column: x => x.Position_id,
                        principalTable: "Positions",
                        principalColumn: "Position_id");
                    table.ForeignKey(
                        name: "Shop_positions_Shop_id_fkey",
                        column: x => x.Shop_id,
                        principalTable: "Shops",
                        principalColumn: "Shop_id");
                    table.ForeignKey(
                        name: "Shop_positions_Worker_id_fkey",
                        column: x => x.Worker_id,
                        principalTable: "Workers",
                        principalColumn: "Worker_id");
                });

            migrationBuilder.CreateTable(
                name: "Product_consignments",
                columns: table => new
                {
                    Product_consignment_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Worker_id = table.Column<int>(type: "integer", nullable: false),
                    Date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Order_consignment_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Product_consignments_pkey", x => x.Product_consignment_id);
                    table.ForeignKey(
                        name: "Product_consignments_Order_consignment_id_fkey",
                        column: x => x.Order_consignment_id,
                        principalTable: "Order_consignments",
                        principalColumn: "Order_consignment_id");
                    table.ForeignKey(
                        name: "Product_consignments_Worker_id_fkey",
                        column: x => x.Worker_id,
                        principalTable: "Workers",
                        principalColumn: "Worker_id");
                });

            migrationBuilder.CreateTable(
                name: "Product_orders",
                columns: table => new
                {
                    Product_order_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Worker_id = table.Column<int>(type: "integer", nullable: false),
                    Order_consignment_id = table.Column<int>(type: "integer", nullable: false),
                    Is_approved = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Product_orders_pkey", x => x.Product_order_id);
                    table.ForeignKey(
                        name: "Product_orders_Consigment_id_fkey",
                        column: x => x.Order_consignment_id,
                        principalTable: "Order_consignments",
                        principalColumn: "Order_consignment_id");
                    table.ForeignKey(
                        name: "Product_orders_Worker_id_fkey",
                        column: x => x.Worker_id,
                        principalTable: "Workers",
                        principalColumn: "Worker_id");
                });

            migrationBuilder.CreateTable(
                name: "Purchase_product_in_storage",
                columns: table => new
                {
                    Purchase_id = table.Column<int>(type: "integer", nullable: false),
                    Products_in_storage_id = table.Column<int>(type: "integer", nullable: false),
                    Product_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Purchase_product_in_storage_pkey", x => new { x.Purchase_id, x.Products_in_storage_id });
                    table.ForeignKey(
                        name: "Purchase_product_in_storage_Products_in_storage_id_fkey",
                        column: x => x.Products_in_storage_id,
                        principalTable: "Products_in_storage",
                        principalColumn: "Product_in_storage_id");
                    table.ForeignKey(
                        name: "Purchase_product_in_storage_Purchase_id_fkey",
                        column: x => x.Purchase_id,
                        principalTable: "Purchases",
                        principalColumn: "Purchase_id");
                });

            migrationBuilder.CreateTable(
                name: "Refunds",
                columns: table => new
                {
                    Refund_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date_Time = table.Column<DateTimeOffset>(type: "time with time zone", nullable: false),
                    Worker_id = table.Column<int>(type: "integer", nullable: false),
                    Purchase_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Refunds_pkey", x => x.Refund_id);
                    table.ForeignKey(
                        name: "Refunds_Purchase_id_fkey",
                        column: x => x.Purchase_id,
                        principalTable: "Purchases",
                        principalColumn: "Purchase_id");
                    table.ForeignKey(
                        name: "Refunds_Worker_id_fkey",
                        column: x => x.Worker_id,
                        principalTable: "Workers",
                        principalColumn: "Worker_id");
                });

            migrationBuilder.CreateTable(
                name: "Product_consignment_product",
                columns: table => new
                {
                    Product_id = table.Column<int>(type: "integer", nullable: false),
                    Product_consignment_id = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Product_consignment_product_pkey", x => new { x.Product_id, x.Product_consignment_id });
                    table.ForeignKey(
                        name: "Product_consignment_product_Product_consignment_id_fkey",
                        column: x => x.Product_consignment_id,
                        principalTable: "Product_consignments",
                        principalColumn: "Product_consignment_id");
                    table.ForeignKey(
                        name: "Product_consignment_product_Product_id_fkey",
                        column: x => x.Product_id,
                        principalTable: "Products",
                        principalColumn: "Product_id");
                });

            migrationBuilder.CreateTable(
                name: "Product_order_product_in_storage",
                columns: table => new
                {
                    Product_order_id = table.Column<int>(type: "integer", nullable: false),
                    Product_in_storage_id = table.Column<int>(type: "integer", nullable: false),
                    Product_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Product_order_product_in_storage_pkey", x => new { x.Product_order_id, x.Product_in_storage_id });
                    table.ForeignKey(
                        name: "Product_order_product_in_storage_Product_in_storage_id_fkey",
                        column: x => x.Product_in_storage_id,
                        principalTable: "Products_in_storage",
                        principalColumn: "Product_in_storage_id");
                    table.ForeignKey(
                        name: "Product_order_product_in_storage_Product_order_id_fkey",
                        column: x => x.Product_order_id,
                        principalTable: "Product_orders",
                        principalColumn: "Product_order_id");
                });

            migrationBuilder.CreateTable(
                name: "Refund_product_in_storage",
                columns: table => new
                {
                    Refund_id = table.Column<int>(type: "integer", nullable: false),
                    Product_in_storage_id = table.Column<int>(type: "integer", nullable: false),
                    Product_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Refund_product_in_storage_pkey", x => new { x.Refund_id, x.Product_in_storage_id });
                    table.ForeignKey(
                        name: "Refund_product_in_storage_Product_in_storage_id_fkey",
                        column: x => x.Product_in_storage_id,
                        principalTable: "Products_in_storage",
                        principalColumn: "Product_in_storage_id");
                    table.ForeignKey(
                        name: "Refund_product_in_storage_Refund_id_fkey",
                        column: x => x.Refund_id,
                        principalTable: "Refunds",
                        principalColumn: "Refund_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_consignments_Worker_id",
                table: "Order_consignments",
                column: "Worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Approvier_worker_id",
                table: "Payments",
                column: "Approvier_worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Recipient_worker_id",
                table: "Payments",
                column: "Recipient_worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Shop_id",
                table: "Payments",
                column: "Shop_id");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_Role_id",
                table: "Positions",
                column: "Role_id");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_Salary_type_id",
                table: "Positions",
                column: "Salary_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_consignment_product_Product_consignment_id",
                table: "Product_consignment_product",
                column: "Product_consignment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_consignments_Order_consignment_id",
                table: "Product_consignments",
                column: "Order_consignment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_consignments_Worker_id",
                table: "Product_consignments",
                column: "Worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_order_product_in_storage_Product_in_storage_id",
                table: "Product_order_product_in_storage",
                column: "Product_in_storage_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_orders_Order_consignment_id",
                table: "Product_orders",
                column: "Order_consignment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_orders_Worker_id",
                table: "Product_orders",
                column: "Worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Category_id",
                table: "Products",
                column: "Category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_in_storage_Product_id",
                table: "Products_in_storage",
                column: "Product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_in_storage_Shop_id",
                table: "Products_in_storage",
                column: "Shop_id");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_product_in_storage_Products_in_storage_id",
                table: "Purchase_product_in_storage",
                column: "Products_in_storage_id");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_Worker_id",
                table: "Purchases",
                column: "Worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_product_in_storage_Product_in_storage_id",
                table: "Refund_product_in_storage",
                column: "Product_in_storage_id");

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_Purchase_id",
                table: "Refunds",
                column: "Purchase_id");

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_Worker_id",
                table: "Refunds",
                column: "Worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_Region_plans_Region_id",
                table: "Region_plans",
                column: "Region_id");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_plans_Shop_id",
                table: "Shop_plans",
                column: "Shop_id");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_positions_Position_id",
                table: "Shop_positions",
                column: "Position_id");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_positions_Shop_id",
                table: "Shop_positions",
                column: "Shop_id");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_positions_Worker_id",
                table: "Shop_positions",
                column: "Worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_Region_id",
                table: "Shops",
                column: "Region_id");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_Shop_type_id",
                table: "Shops",
                column: "Shop_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_Worker_type_id",
                table: "Workers",
                column: "Worker_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Product_consignment_product");

            migrationBuilder.DropTable(
                name: "Product_order_product_in_storage");

            migrationBuilder.DropTable(
                name: "Purchase_product_in_storage");

            migrationBuilder.DropTable(
                name: "Refund_product_in_storage");

            migrationBuilder.DropTable(
                name: "Region_plans");

            migrationBuilder.DropTable(
                name: "Shop_plans");

            migrationBuilder.DropTable(
                name: "Shop_positions");

            migrationBuilder.DropTable(
                name: "TestTable");

            migrationBuilder.DropTable(
                name: "Product_consignments");

            migrationBuilder.DropTable(
                name: "Product_orders");

            migrationBuilder.DropTable(
                name: "Products_in_storage");

            migrationBuilder.DropTable(
                name: "Refunds");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Order_consignments");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Salary_types");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Shop_types");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "Worker_types");
        }
    }
}
