using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelryShopManagementSystem.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Birthdate { get; set; }
        public List<Purchase>? Purchases { get; set; }
    }
}
