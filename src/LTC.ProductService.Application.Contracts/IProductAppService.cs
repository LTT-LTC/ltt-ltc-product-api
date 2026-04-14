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
        Task<PagedResultDto<ProductOutputDto>> GetAllAsync(GetProductListInputDto input);
        Task<ProductDetailOutputDto> GetAsync(Guid id);
        Task<ProductOutputDto> CreateAsync(CreateProductInputDto input);
        Task<ProductOutputDto> UpdateAsync(Guid id, UpdateProductInputDto input);
        Task DeleteAsync(Guid id);
        Task DeleteBulkAsync(List<Guid> ids);
        
        // Variants
        Task<ProductVariantOutputDto> CreateVariantAsync(Guid id, CreateProductVariantInputDto input);
        Task<ProductVariantOutputDto> UpdateVariantAsync(Guid id, Guid variantId, UpdateProductVariantInputDto input);
        Task DeleteVariantAsync(Guid id, Guid variantId);
    }
}
