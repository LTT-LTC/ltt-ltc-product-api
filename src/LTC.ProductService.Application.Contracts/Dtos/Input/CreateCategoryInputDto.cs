using System.ComponentModel.DataAnnotations;

namespace LTC.ProductService.Dtos.Input
{
    public class CreateCategoryInputDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public bool IsActive { get; set; }
    }
}
