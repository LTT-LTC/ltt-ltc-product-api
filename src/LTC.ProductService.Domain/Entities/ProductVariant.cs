using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace LTC.ProductService.Entities
{
    public class ProductVariant : Entity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal AdditionalPrice { get; set; }
        public bool IsActive { get; set; }
        
        public Product Product { get; set; }
    }
}
