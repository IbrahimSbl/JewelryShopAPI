using JewelryShopManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace JewelryShopAPI.DTO.EmployeeDTO_s
{
    public class CustomerGetEmployeeByIdDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
