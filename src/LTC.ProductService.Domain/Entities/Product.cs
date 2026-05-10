using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace LTC.ProductService.Entities
{
    /// <summary>Maps to <c>Products</c> — schema-aligned; uses <see cref="CreatedAt"/>/<see cref="UpdatedAt"/> instead of ABP audit columns.</summary>
    public class Product : Entity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public Guid ProductCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ProductCategory ProductCategory { get; set; }
    }
}
