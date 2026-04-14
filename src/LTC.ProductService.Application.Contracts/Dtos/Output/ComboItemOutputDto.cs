using System;
using Volo.Abp.Application.Dtos;

namespace LTC.ProductService.Dtos.Output
{
    public class ComboItemOutputDto : EntityDto<Guid>
    {
        public Guid ComboId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        
        public ProductOutputDto Product { get; set; }
    }
}
