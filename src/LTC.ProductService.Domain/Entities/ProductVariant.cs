using System;
using Volo.Abp.Domain.Entities;

namespace LTC.ProductService.Entities
{
    public class ProductVariant : Entity<Guid>
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal AdditionalPrice { get; set; }
        public bool IsActive { get; set; }
        
        public Product Product { get; set; }
    }
}
