using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

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
        public string? Description { get; set; }

        [Required]
        public decimal BasePrice { get; set; }

        [Required]
        public decimal SellPrice { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// Optional file uploaded as multipart/form-data. When supplied, it is uploaded
        /// to Cloudinary and the resulting URL replaces <see cref="ImageUrl"/>.
        /// </summary>
        public IFormFile? ImageFile { get; set; }

        /// <summary>
        /// Already-hosted image URL (used when client wants to keep the existing image
        /// on update or when uploading was performed via the dedicated upload endpoint).
        /// </summary>
        public string? ImageUrl { get; set; }
    }
}
