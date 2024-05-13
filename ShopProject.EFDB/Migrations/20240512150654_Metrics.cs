using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShopProject.EFDB.Migrations
{
    /// <inheritdoc />
    public partial class Metrics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopPlans_PlanAtributes_PlanAtributeId",
                table: "ShopPlans");

            migrationBuilder.DropTable(
                name: "PlanAtributes");

            migrationBuilder.RenameColumn(
                name: "PlanAtributeId",
                table: "ShopPlans",
                newName: "MetricId");

            migrationBuilder.RenameColumn(
                name: "AtributeValue",
                table: "ShopPlans",
                newName: "MetricValue");

            migrationBuilder.RenameIndex(
                name: "IX_ShopPlans_PlanAtributeId",
                table: "ShopPlans",
                newName: "IX_ShopPlans_MetricId");

            migrationBuilder.CreateTable(
                name: "Metrics",
                columns: table => new
                {
                    MetricId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MetricName = table.Column<string>(type: "text", nullable: false),
                    MetricViewName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrics", x => x.MetricId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ShopPlans_Metrics_MetricId",
                table: "ShopPlans",
                column: "MetricId",
                principalTable: "Metrics",
                principalColumn: "MetricId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopPlans_Metrics_MetricId",
                table: "ShopPlans");

            migrationBuilder.DropTable(
                name: "Metrics");

            migrationBuilder.RenameColumn(
                name: "MetricValue",
                table: "ShopPlans",
                newName: "AtributeValue");

            migrationBuilder.RenameColumn(
                name: "MetricId",
                table: "ShopPlans",
                newName: "PlanAtributeId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopPlans_MetricId",
                table: "ShopPlans",
                newName: "IX_ShopPlans_PlanAtributeId");

            migrationBuilder.CreateTable(
                name: "PlanAtributes",
                columns: table => new
                {
                    PlanAtributeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AtributeName = table.Column<string>(type: "text", nullable: false),
                    AtributeViewName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanAtributes", x => x.PlanAtributeId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ShopPlans_PlanAtributes_PlanAtributeId",
                table: "ShopPlans",
                column: "PlanAtributeId",
                principalTable: "PlanAtributes",
                principalColumn: "PlanAtributeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
