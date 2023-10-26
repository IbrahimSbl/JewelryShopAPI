using JewelryShopAPI.DTO.CustomerDTO_s;

namespace JewelryShopAPI.DTO.CustomerDTO_s
{
    public class JewelryItemPurchaseGetCustomerById
    {
        public int jewelryItemsId { get; set; }
        public int purchasesId { get; set; }
        public JewelryItemGetCustomerById jewelryItem { get; set; }
    }
}
