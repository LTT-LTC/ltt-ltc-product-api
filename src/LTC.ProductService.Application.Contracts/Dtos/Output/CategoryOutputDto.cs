using System;
using Volo.Abp.Application.Dtos;

namespace LTC.ProductService.Dtos.Output
{
    public class CategoryOutputDto : EntityDto<Guid>
    {
        public Guid? TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
