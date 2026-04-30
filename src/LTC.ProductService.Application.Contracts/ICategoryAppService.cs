using System;
using System.Threading.Tasks;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LTC.ProductService
{
    public interface ICategoryAppService : IApplicationService
    {
        Task<PagedResultDto<CategoryOutputDto>> GetCategoryListAsync(PaginationInputDto input);
        Task<CategoryOutputDto> GetCategoryAsync(Guid id);
        Task<CategoryOutputDto> CreateCategoryAsync(CreateCategoryInputDto input);
        Task<CategoryOutputDto> UpdateCategoryAsync(Guid id, UpdateCategoryInputDto input);
        Task DeleteCategoryAsync(Guid id);
    }
}
