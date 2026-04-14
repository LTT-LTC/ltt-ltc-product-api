using System;
using Volo.Abp.Application.Dtos;

namespace LTC.ProductService.Dtos.Output
{
    public class ProductVariantOutputDto : EntityDto<Guid>
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal AdditionalPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
