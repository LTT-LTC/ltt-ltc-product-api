using System;
using System.ComponentModel.DataAnnotations;

namespace LTC.ProductService.Dtos.Input
{
    public class CreateProductVariantInputDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        
        [Required]
        public decimal AdditionalPrice { get; set; }
        
        public bool IsActive { get; set; }
    }
}
