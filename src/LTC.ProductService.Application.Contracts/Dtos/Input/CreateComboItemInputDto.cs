using System;
using System.ComponentModel.DataAnnotations;

namespace LTC.ProductService.Dtos.Input
{
    public class CreateComboItemInputDto
    {
        [Required]
        public Guid ProductId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
