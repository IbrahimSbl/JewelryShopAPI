using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JewelryShopManagementSystem.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20,ErrorMessage ="Exceeded max length which is 20 character")]
        public string? Name { get; set; }
        public string? CategoryPicture { get; set; }
        public List<JewelryItem>? JewelryItems { get; set; }
    }
}