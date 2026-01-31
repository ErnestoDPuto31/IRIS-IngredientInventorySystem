using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IRIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsFirstLoginToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isFirstLogin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isFirstLogin",
                table: "Users");
        }
    }
}
