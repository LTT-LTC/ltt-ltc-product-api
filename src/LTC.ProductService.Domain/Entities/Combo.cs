using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace LTC.ProductService.Entities
{
    /// <summary>Maps to <c>Combos</c> — schema-aligned; no ABP audit columns in DB.</summary>
    public class Combo : Entity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        /// <summary>
        /// JSON array of <c>{"productId":"...","quantity":N}</c> entries describing
        /// the products bundled in this combo.
        /// </summary>
        public string ProductIds { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
