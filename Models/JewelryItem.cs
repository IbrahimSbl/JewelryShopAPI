using System.ComponentModel.DataAnnotations.Schema;
using JewelryShopAPI.Models;

namespace JewelryShopManagementSystem.Models
{
    public class JewelryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public double Discount { get; set; }
        public string Metal { get; set; }
        public string Gemstones { get; set; }
        public double Weight { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int StockQuantity { get; set; }
		public string? JewelryPicture { get; set; }
		public Category? Category { get; set; }
        public List<JewelryItemPurchase>? JoinTables { get; set; }
    }

}
