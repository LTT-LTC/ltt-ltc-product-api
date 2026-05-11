using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace LTC.ProductService.Dtos.Output
{
    public class ComboOutputDto : EntityDto<Guid>
    {
        public Guid? TenantId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<ComboProductLineOutputDto> Products { get; set; } = new();
    }
}
