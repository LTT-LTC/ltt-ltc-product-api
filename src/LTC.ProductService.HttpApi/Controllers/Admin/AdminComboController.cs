using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;

namespace LTC.ProductService.Controllers.Admin
{
    [Route(ProductServiceSettingNames.DefaultRoute + "/administration/admin")]
    public class AdminComboController : AbpControllerBase
    {
        private readonly IComboAppService _appService;

        public AdminComboController(IComboAppService appService)
        {
            _appService = appService;
        }

        [HttpGet("combo-all")]
        public async Task<PagedResultDto<ComboOutputDto>> GetAllAsync([FromQuery] PaginationInputDto input)
        {
            return await _appService.GetAllAsync(input);
        }

        [HttpGet("combo/{id}")]
        public async Task<ComboDetailOutputDto> GetAsync(Guid id)
        {
            return await _appService.GetAsync(id);
        }

        [HttpPost("combo")]
        public async Task<ComboOutputDto> CreateAsync([FromBody] CreateComboInputDto input)
        {
            return await _appService.CreateAsync(input);
        }

        [HttpPut("combo/{id}")]
        public async Task<ComboOutputDto> UpdateAsync(Guid id, [FromBody] UpdateComboInputDto input)
        {
            return await _appService.UpdateAsync(id, input);
        }

        [HttpDelete("combo/{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _appService.DeleteAsync(id);
        }

        [HttpPost("combo/{id}/item")]
        public async Task<ComboItemOutputDto> AddItemAsync(Guid id, [FromBody] CreateComboItemInputDto input)
        {
            return await _appService.AddItemAsync(id, input);
        }

        [HttpDelete("combo/{id}/item/{comboItemId}")]
        public async Task DeleteItemAsync(Guid id, Guid comboItemId)
        {
            await _appService.DeleteItemAsync(id, comboItemId);
        }
    }
}
