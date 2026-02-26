using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IRIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedInterfacesAndDependencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "NewStock",
                table: "InventoryLogs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PreviousStock",
                table: "InventoryLogs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLogs_IngredientId",
                table: "InventoryLogs",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLogs_PerformedByUserId",
                table: "InventoryLogs",
                column: "PerformedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryLogs_Ingredients_IngredientId",
                table: "InventoryLogs",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryLogs_Users_PerformedByUserId",
                table: "InventoryLogs",
                column: "PerformedByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryLogs_Ingredients_IngredientId",
                table: "InventoryLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryLogs_Users_PerformedByUserId",
                table: "InventoryLogs");

            migrationBuilder.DropIndex(
                name: "IX_InventoryLogs_IngredientId",
                table: "InventoryLogs");

            migrationBuilder.DropIndex(
                name: "IX_InventoryLogs_PerformedByUserId",
                table: "InventoryLogs");

            migrationBuilder.DropColumn(
                name: "NewStock",
                table: "InventoryLogs");

            migrationBuilder.DropColumn(
                name: "PreviousStock",
                table: "InventoryLogs");
        }
    }
}
