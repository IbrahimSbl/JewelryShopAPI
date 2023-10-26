using JewelryShopManagementSystem.Models;

namespace JewelryShopAPI.DTO.EmployeeDTO_s
{
    public class JewelryItemPurchaseGetEmployeeByIdDTO
    {
        public int JewelryItemsId { get; set; }
        public int PurchasesId { get; set; }
        public JewelryItemGetEmployeeByIdDTO JewelryItem { get; set; }
    }
}
