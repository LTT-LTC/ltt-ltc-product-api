using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace LTC.ProductService.Dtos.Input
{
    public class CreateComboInputDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public decimal TotalPrice { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// Optional uploaded image. When provided, it is uploaded to Cloudinary and
        /// the resulting URL replaces <see cref="ImageUrl"/>.
        /// </summary>
        public IFormFile? ImageFile { get; set; }

        public string? ImageUrl { get; set; }

        /// <summary>
        /// Products bundled in this combo. Persisted as JSON in <c>Combos.ProductIds</c>.
        /// </summary>
        public List<ComboProductLineDto> Products { get; set; } = new();
    }
}
