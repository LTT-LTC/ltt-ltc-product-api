using System;
using System.ComponentModel.DataAnnotations;

namespace LTC.ProductService.Dtos.Input
{
    public class CreateProductInputDto
    {
        [Required]
        public Guid ProductCategoryId { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [Required]
        public decimal BasePrice { get; set; }
        
        public string ImageUrl { get; set; }
        
        public bool IsActive { get; set; }
        
        public string ProductType { get; set; }
    }
}
