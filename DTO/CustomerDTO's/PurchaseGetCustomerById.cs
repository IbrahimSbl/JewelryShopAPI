using JewelryShopAPI.DTO.CustomerDTO_s;

namespace JewelryShopAPI.DTO.CustomerDTO_s
{
    public class PurchaseGetCustomerById
    {
        public int id { get; set; }
        public DateTime purchaseDate { get; set; }
        public decimal totalAmount { get; set; }
        public List<JewelryItemPurchaseGetCustomerById>? joinTables { get; set; }
    }
}
