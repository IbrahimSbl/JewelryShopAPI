using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewelryShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class JewelryItemPurchaseManyToManyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JewelryItemPurchase_Purchase_purchasesId",
                table: "JewelryItemPurchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JewelryItemPurchase",
                table: "JewelryItemPurchase");

            migrationBuilder.DropIndex(
                name: "IX_JewelryItemPurchase_purchasesId",
                table: "JewelryItemPurchase");

            migrationBuilder.RenameColumn(
                name: "purchasesId",
                table: "JewelryItemPurchase",
                newName: "PurchasesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JewelryItemPurchase",
                table: "JewelryItemPurchase",
                columns: new[] { "PurchasesId", "JewelryItemsId" });

            migrationBuilder.CreateIndex(
                name: "IX_JewelryItemPurchase_JewelryItemsId",
                table: "JewelryItemPurchase",
                column: "JewelryItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_JewelryItemPurchase_Purchase_PurchasesId",
                table: "JewelryItemPurchase",
                column: "PurchasesId",
                principalTable: "Purchase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JewelryItemPurchase_Purchase_PurchasesId",
                table: "JewelryItemPurchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JewelryItemPurchase",
                table: "JewelryItemPurchase");

            migrationBuilder.DropIndex(
                name: "IX_JewelryItemPurchase_JewelryItemsId",
                table: "JewelryItemPurchase");

            migrationBuilder.RenameColumn(
                name: "PurchasesId",
                table: "JewelryItemPurchase",
                newName: "purchasesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JewelryItemPurchase",
                table: "JewelryItemPurchase",
                columns: new[] { "JewelryItemsId", "purchasesId" });

            migrationBuilder.CreateIndex(
                name: "IX_JewelryItemPurchase_purchasesId",
                table: "JewelryItemPurchase",
                column: "purchasesId");

            migrationBuilder.AddForeignKey(
                name: "FK_JewelryItemPurchase_Purchase_purchasesId",
                table: "JewelryItemPurchase",
                column: "purchasesId",
                principalTable: "Purchase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
