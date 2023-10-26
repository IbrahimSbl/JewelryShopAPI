using JewelryShopManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace JewelryShopAPI.DTO
{
    public class EmployeeGetByIdDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public decimal Salary { get; set; }
        public List<PurchaseGetEmployeeByIdDTO>? Purchases { get; set; }

    }
}
