using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IRIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OverhauledRequestModelsFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Approvals");

            migrationBuilder.RenameColumn(
                name: "EncodedBy",
                table: "Requests",
                newName: "EncodedById");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Approvals",
                newName: "ActionDate");

            migrationBuilder.RenameColumn(
                name: "ApprovedByUserId",
                table: "Approvals",
                newName: "ApproverId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Requests",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfUse",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "RecipeCosting",
                table: "Requests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "Approvals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActionType",
                table: "Approvals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_EncodedById",
                table: "Requests",
                column: "EncodedById");

            migrationBuilder.CreateIndex(
                name: "IX_Approvals_ApproverId",
                table: "Approvals",
                column: "ApproverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Approvals_Users_ApproverId",
                table: "Approvals",
                column: "ApproverId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_EncodedById",
                table: "Requests",
                column: "EncodedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Approvals_Users_ApproverId",
                table: "Approvals");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_EncodedById",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_EncodedById",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Approvals_ApproverId",
                table: "Approvals");

            migrationBuilder.DropColumn(
                name: "DateOfUse",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RecipeCosting",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ActionType",
                table: "Approvals");

            migrationBuilder.RenameColumn(
                name: "EncodedById",
                table: "Requests",
                newName: "EncodedBy");

            migrationBuilder.RenameColumn(
                name: "ApproverId",
                table: "Approvals",
                newName: "ApprovedByUserId");

            migrationBuilder.RenameColumn(
                name: "ActionDate",
                table: "Approvals",
                newName: "Timestamp");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Requests",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "Approvals",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Approvals",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
