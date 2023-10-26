using System.Text.Json.Serialization;
using JewelryShopAPI.DTO.EmployeeDTO_s;
using JewelryShopAPI.Models;
using JewelryShopManagementSystem.Models;

namespace JewelryShopAPI.DTO
{
    public class PurchaseGetEmployeeByIdDTO
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        // Foreign key property
        public int CustomerId { get; set; }
        // Foreign key property
        public int EmployeeId { get; set; }
        public List<JewelryItemPurchaseGetEmployeeByIdDTO>? JoinTables { get; set; }
        public CustomerGetEmployeeByIdDTO? Customer { get; set; }
    }
}
