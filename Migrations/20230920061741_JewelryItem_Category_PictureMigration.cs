using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewelryShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class JewelryItem_Category_PictureMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JewelryPicture",
                table: "JewelryItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryPicture",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JewelryPicture",
                table: "JewelryItem");

            migrationBuilder.DropColumn(
                name: "CategoryPicture",
                table: "Category");
        }
    }
}
