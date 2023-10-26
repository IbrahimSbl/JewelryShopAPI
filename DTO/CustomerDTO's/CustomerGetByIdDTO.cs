using JewelryShopAPI.DTO.CustomerDTO_s;
using System.ComponentModel.DataAnnotations;

namespace JewelryShopAPI.DTO.CustomerDTO_s
{
    public class CustomerGetByIdDTO
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [EmailAddress]
        public string email { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime birthdate { get; set; }
        public List<PurchaseGetCustomerById>? purchases { get; set; }
    }
}
