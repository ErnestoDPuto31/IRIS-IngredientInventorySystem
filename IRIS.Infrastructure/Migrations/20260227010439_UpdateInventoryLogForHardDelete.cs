using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IRIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInventoryLogForHardDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryLogs_Ingredients_IngredientId",
                table: "InventoryLogs");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientId",
                table: "InventoryLogs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "IngredientName",
                table: "InventoryLogs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryLogs_Ingredients_IngredientId",
                table: "InventoryLogs",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryLogs_Ingredients_IngredientId",
                table: "InventoryLogs");

            migrationBuilder.DropColumn(
                name: "IngredientName",
                table: "InventoryLogs");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientId",
                table: "InventoryLogs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryLogs_Ingredients_IngredientId",
                table: "InventoryLogs",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
