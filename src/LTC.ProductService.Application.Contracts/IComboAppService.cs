using System;
using System.Threading.Tasks;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LTC.ProductService
{
    public interface IComboAppService : IApplicationService
    {
        Task<PagedResultDto<ComboOutputDto>> GetAllAsync(PaginationInputDto input);
        Task<ComboDetailOutputDto> GetAsync(Guid id);
        Task<ComboOutputDto> CreateAsync(CreateComboInputDto input);
        Task<ComboOutputDto> UpdateAsync(Guid id, UpdateComboInputDto input);
        Task DeleteAsync(Guid id);
        
        // Items
        Task<ComboItemOutputDto> AddItemAsync(Guid id, CreateComboItemInputDto input);
        Task DeleteItemAsync(Guid id, Guid comboItemId);
    }
}
