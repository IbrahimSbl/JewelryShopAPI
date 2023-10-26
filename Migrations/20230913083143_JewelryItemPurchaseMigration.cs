using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewelryShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class JewelryItemPurchaseMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JewelryItem_Purchase_PurchaseId",
                table: "JewelryItem");

            migrationBuilder.DropIndex(
                name: "IX_JewelryItem_PurchaseId",
                table: "JewelryItem");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "JewelryItem");

            migrationBuilder.CreateTable(
                name: "JewelryItemPurchase",
                columns: table => new
                {
                    JewelryItemsId = table.Column<int>(type: "int", nullable: false),
                    purchasesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JewelryItemPurchase", x => new { x.JewelryItemsId, x.purchasesId });
                    table.ForeignKey(
                        name: "FK_JewelryItemPurchase_JewelryItem_JewelryItemsId",
                        column: x => x.JewelryItemsId,
                        principalTable: "JewelryItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JewelryItemPurchase_Purchase_purchasesId",
                        column: x => x.purchasesId,
                        principalTable: "Purchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JewelryItemPurchase_purchasesId",
                table: "JewelryItemPurchase",
                column: "purchasesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JewelryItemPurchase");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "JewelryItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JewelryItem_PurchaseId",
                table: "JewelryItem",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_JewelryItem_Purchase_PurchaseId",
                table: "JewelryItem",
                column: "PurchaseId",
                principalTable: "Purchase",
                principalColumn: "Id");
        }
    }
}
