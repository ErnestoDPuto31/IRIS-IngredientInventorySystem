using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IRIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixedschema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipeCosting",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "QuantityRequested",
                table: "Restocks",
                newName: "SuggestedRestockQuantity");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Restocks",
                newName: "DateRestocked");

            migrationBuilder.RenameColumn(
                name: "RequestItemId",
                table: "RequestItems",
                newName: "RequestDetailsId");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Requests",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PortionPerStudent",
                table: "RequestItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "PortionPerStudent",
                table: "RequestItems");

            migrationBuilder.RenameColumn(
                name: "SuggestedRestockQuantity",
                table: "Restocks",
                newName: "QuantityRequested");

            migrationBuilder.RenameColumn(
                name: "DateRestocked",
                table: "Restocks",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "RequestDetailsId",
                table: "RequestItems",
                newName: "RequestItemId");

            migrationBuilder.AddColumn<decimal>(
                name: "RecipeCosting",
                table: "Requests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
