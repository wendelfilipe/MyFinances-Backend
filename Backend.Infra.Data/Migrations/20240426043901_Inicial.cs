using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "product");

            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.CreateTable(
                name: "user",
                schema: "security",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    source_create = table.Column<int>(type: "integer", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "wallet",
                schema: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    source_create = table.Column<int>(type: "integer", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wallet", x => x.id);
                    table.ForeignKey(
                        name: "FK_wallet_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assets",
                schema: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    wallet_id = table.Column<int>(type: "integer", nullable: false),
                    cod_name = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    currentprice = table.Column<decimal>(name: "current-price", type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    buy_price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    source_type_assets = table.Column<int>(type: "integer", nullable: false),
                    average_price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    source_create = table.Column<int>(type: "integer", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assets", x => x.id);
                    table.ForeignKey(
                        name: "FK_assets_wallet_wallet_id",
                        column: x => x.wallet_id,
                        principalSchema: "product",
                        principalTable: "wallet",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JoinAssetsWallet",
                schema: "product",
                columns: table => new
                {
                    wallet_id = table.Column<int>(type: "integer", nullable: false),
                    assets_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinAssetsWallet", x => new { x.assets_id, x.wallet_id });
                    table.ForeignKey(
                        name: "FK_JoinAssetsWallet_wallet_wallet_id",
                        column: x => x.wallet_id,
                        principalSchema: "product",
                        principalTable: "wallet",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetsJoinAssetWallet",
                schema: "product",
                columns: table => new
                {
                    AssetsId = table.Column<int>(type: "integer", nullable: false),
                    JoinAssetWalletAssetId = table.Column<int>(type: "integer", nullable: false),
                    JoinAssetWalletWalletId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetsJoinAssetWallet", x => new { x.AssetsId, x.JoinAssetWalletAssetId, x.JoinAssetWalletWalletId });
                    table.ForeignKey(
                        name: "FK_AssetsJoinAssetWallet_JoinAssetsWallet_JoinAssetWalletAsset~",
                        columns: x => new { x.JoinAssetWalletAssetId, x.JoinAssetWalletWalletId },
                        principalSchema: "product",
                        principalTable: "JoinAssetsWallet",
                        principalColumns: new[] { "assets_id", "wallet_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetsJoinAssetWallet_assets_AssetsId",
                        column: x => x.AssetsId,
                        principalSchema: "product",
                        principalTable: "assets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_assets_wallet_id",
                schema: "product",
                table: "assets",
                column: "wallet_id");

            migrationBuilder.CreateIndex(
                name: "IX_AssetsJoinAssetWallet_JoinAssetWalletAssetId_JoinAssetWalle~",
                schema: "product",
                table: "AssetsJoinAssetWallet",
                columns: new[] { "JoinAssetWalletAssetId", "JoinAssetWalletWalletId" });

            migrationBuilder.CreateIndex(
                name: "IX_JoinAssetsWallet_wallet_id",
                schema: "product",
                table: "JoinAssetsWallet",
                column: "wallet_id");

            migrationBuilder.CreateIndex(
                name: "IX_wallet_UserId",
                schema: "product",
                table: "wallet",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetsJoinAssetWallet",
                schema: "product");

            migrationBuilder.DropTable(
                name: "JoinAssetsWallet",
                schema: "product");

            migrationBuilder.DropTable(
                name: "assets",
                schema: "product");

            migrationBuilder.DropTable(
                name: "wallet",
                schema: "product");

            migrationBuilder.DropTable(
                name: "user",
                schema: "security");
        }
    }
}
