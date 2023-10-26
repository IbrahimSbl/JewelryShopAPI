using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewelryShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class JewelryItemUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryJewelryItem");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "JewelryItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JewelryItem_CategoryId",
                table: "JewelryItem",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_JewelryItem_Category_CategoryId",
                table: "JewelryItem",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JewelryItem_Category_CategoryId",
                table: "JewelryItem");

            migrationBuilder.DropIndex(
                name: "IX_JewelryItem_CategoryId",
                table: "JewelryItem");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "JewelryItem");

            migrationBuilder.CreateTable(
                name: "CategoryJewelryItem",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    JewelryItemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryJewelryItem", x => new { x.CategoriesId, x.JewelryItemsId });
                    table.ForeignKey(
                        name: "FK_CategoryJewelryItem_Category_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryJewelryItem_JewelryItem_JewelryItemsId",
                        column: x => x.JewelryItemsId,
                        principalTable: "JewelryItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryJewelryItem_JewelryItemsId",
                table: "CategoryJewelryItem",
                column: "JewelryItemsId");
        }
    }
}
