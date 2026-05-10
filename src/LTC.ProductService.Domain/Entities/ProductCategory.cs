using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace LTC.ProductService.Entities
{
    /// <summary>Maps to <c>ProductCategories</c> — schema-aligned; no ABP audit columns in DB.</summary>
    public class ProductCategory : Entity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
