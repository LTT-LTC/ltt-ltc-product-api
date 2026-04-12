using System;
using Volo.Abp.Domain.Entities;

namespace LTC.ProductService.Entities
{
    public class ComboItem : Entity<Guid>
    {
        public Guid ComboId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        
        public Combo Combo { get; set; }
        public Product Product { get; set; }
    }
}
