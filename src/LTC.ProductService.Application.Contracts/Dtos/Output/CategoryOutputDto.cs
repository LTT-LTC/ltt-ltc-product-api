using System;
using Volo.Abp.Application.Dtos;

namespace LTC.ProductService.Dtos.Output
{
    public class CategoryOutputDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
