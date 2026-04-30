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
        Task<PagedResultDto<ComboOutputDto>> GetComboListAsync(PaginationInputDto input);
        Task<ComboDetailOutputDto> GetComboAsync(Guid id);
        Task<ComboOutputDto> CreateComboAsync(CreateComboInputDto input);
        Task<ComboOutputDto> UpdateComboAsync(Guid id, UpdateComboInputDto input);
        Task DeleteComboAsync(Guid id);
        
        // Items
        Task<ComboItemOutputDto> AddItemAsync(Guid id, CreateComboItemInputDto input);
        Task DeleteItemAsync(Guid id, Guid comboItemId);
    }
}
