using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LTC.ProductService
{
    public interface IProductAppService : IApplicationService
    {
        Task<PagedResultDto<ProductOutputDto>> GetProductListAsync(GetProductListInputDto input);
        Task<ProductOutputDto> GetProductAsync(Guid id);
        Task<ProductOutputDto> CreateProductAsync(CreateProductInputDto input);
        Task<ProductOutputDto> UpdateProductAsync(Guid id, UpdateProductInputDto input);
        Task DeleteProductAsync(Guid id);
        Task DeleteBulkAsync(List<Guid> ids);
    }
}
