using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelryShopManagementSystem.Models
{
    public class Employee
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
        public List<Purchase>? Purchases{ get; set; }

    }
}
