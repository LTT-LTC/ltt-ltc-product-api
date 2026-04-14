using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LTC.ProductService.Dtos.Input
{
    public class CreateComboInputDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public decimal TotalPrice { get; set; }
        
        public bool IsActive { get; set; }

        public List<CreateComboItemInputDto> ComboItems { get; set; } = new();
    }
}
