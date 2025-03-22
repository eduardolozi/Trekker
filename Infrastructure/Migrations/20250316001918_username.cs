using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class username : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FamilyName",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "User",
                newName: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "User",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "FamilyName",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
