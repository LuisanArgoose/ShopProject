using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProject.EFDB.Migrations
{
    /// <inheritdoc />
    public partial class Token_logins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "TokenLogins",
                newName: "Token_Logins");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Token_Logins",
                newName: "TokenLogins");
        }
    }
}
