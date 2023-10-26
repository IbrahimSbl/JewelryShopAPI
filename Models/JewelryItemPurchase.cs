using System.Text.Json.Serialization;
using JewelryShopManagementSystem.Models;

namespace JewelryShopAPI.Models
{
    public class JewelryItemPurchase
    {
        public int JewelryItemsId { get; set; }
        public int PurchasesId { get; set; }
        [JsonIgnore]
        public JewelryItem JewelryItem { get; set; }
        [JsonIgnore]
        public Purchase Purchase { get; set; }
    }
}
