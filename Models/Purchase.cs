using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using JewelryShopAPI.Models;

namespace JewelryShopManagementSystem.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        // Foreign key property
        public int CustomerId { get; set; }
        // Foreign key property
        public int EmployeeId { get; set; }
        public List<JewelryItemPurchase>? JoinTables { get; set; }
        [JsonIgnore]
        public Customer? Customer { get; set; }
        [JsonIgnore]
        public Employee? Employee { get; set; }
    }
}