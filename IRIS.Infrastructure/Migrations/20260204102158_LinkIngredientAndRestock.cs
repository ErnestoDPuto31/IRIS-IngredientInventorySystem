using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IRIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LinkIngredientAndRestock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IngredientId",
                table: "Restocks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restocks_IngredientId",
                table: "Restocks",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restocks_Ingredients_IngredientId",
                table: "Restocks",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restocks_Ingredients_IngredientId",
                table: "Restocks");

            migrationBuilder.DropIndex(
                name: "IX_Restocks_IngredientId",
                table: "Restocks");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientId",
                table: "Restocks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
