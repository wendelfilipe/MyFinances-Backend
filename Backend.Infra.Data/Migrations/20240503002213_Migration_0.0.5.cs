using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migration_005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_wallet_user_UserId",
                schema: "product",
                table: "wallet");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "product",
                table: "wallet",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_wallet_UserId",
                schema: "product",
                table: "wallet",
                newName: "IX_wallet_user_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deleted_at",
                schema: "product",
                table: "wallet",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_wallet_user_user_id",
                schema: "product",
                table: "wallet",
                column: "user_id",
                principalSchema: "security",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_wallet_user_user_id",
                schema: "product",
                table: "wallet");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "product",
                table: "wallet",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_wallet_user_id",
                schema: "product",
                table: "wallet",
                newName: "IX_wallet_UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deleted_at",
                schema: "product",
                table: "wallet",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_wallet_user_UserId",
                schema: "product",
                table: "wallet",
                column: "UserId",
                principalSchema: "security",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
