using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewelryShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class DicountJewelryItemMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "JewelryItem",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "JewelryItem");
        }
    }
}
