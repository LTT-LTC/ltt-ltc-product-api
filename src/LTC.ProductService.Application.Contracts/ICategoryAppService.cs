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
        Task<PagedResultDto<CategoryOutputDto>> GetAllAsync(PaginationInputDto input);
        Task<CategoryOutputDto> GetAsync(Guid id);
        Task<CategoryOutputDto> CreateAsync(CreateCategoryInputDto input);
        Task<CategoryOutputDto> UpdateAsync(Guid id, UpdateCategoryInputDto input);
        Task DeleteAsync(Guid id);
    }
}
