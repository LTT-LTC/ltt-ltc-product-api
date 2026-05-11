using System;
using Volo.Abp.Application.Dtos;

namespace LTC.ProductService.Dtos.Output
{
    public class ProductOutputDto : EntityDto<Guid>
    {
        public Guid? TenantId { get; set; }
        public Guid ProductCategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public decimal SellPrice { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
