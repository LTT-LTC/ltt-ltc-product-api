using System;

namespace LTC.ProductService.Dtos.Input
{
    public class GetProductListInputDto : PaginationInputDto
    {
        public Guid? CategoryId { get; set; }
    }
}
