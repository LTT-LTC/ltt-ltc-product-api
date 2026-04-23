using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;

namespace LTC.ProductService.Controllers
{
    [Route(ProductServiceSettingNames.DefaultRoute)]
    [Obsolete("Use role-specific endpoints under /admin or /manager. This route remains for compatibility.")]
    public class ComboController : AbpControllerBase
    {
        private readonly IComboAppService _appService;

        public ComboController(IComboAppService appService)
        {
            _appService = appService;
        }

        [HttpGet("combo-all")]
        public async Task<PagedResultDto<ComboOutputDto>> GetAllAsync([FromQuery] PaginationInputDto input)
        {
            /// <summary>
            /// Get all combos.
            /// </summary>
            return await _appService.GetAllAsync(input);
        }

        [HttpGet("combo/{id}")]
        public async Task<ComboDetailOutputDto> GetAsync(Guid id)
        {
            /// <summary>
            /// Get combo details by id.
            /// </summary>
            return await _appService.GetAsync(id);
        }

        [HttpPost("combo")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ComboOutputDto> CreateAsync([FromBody] CreateComboInputDto input)
        {
            /// <summary>
            /// Create a new combo (Admin/Manager only).
            /// </summary>
            return await _appService.CreateAsync(input);
        }

        [HttpPut("combo/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ComboOutputDto> UpdateAsync(Guid id, [FromBody] UpdateComboInputDto input)
        {
            /// <summary>
            /// Update a combo (Admin/Manager only).
            /// </summary>
            return await _appService.UpdateAsync(id, input);
        }

        [HttpDelete("combo/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task DeleteAsync(Guid id)
        {
            /// <summary>
            /// Delete a combo (Admin/Manager only).
            /// </summary>
            await _appService.DeleteAsync(id);
        }

        [HttpPost("combo/{id}/item")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ComboItemOutputDto> AddItemAsync(Guid id, [FromBody] CreateComboItemInputDto input)
        {
            /// <summary>
            /// Add item to combo (Admin/Manager only).
            /// </summary>
            return await _appService.AddItemAsync(id, input);
        }

        [HttpDelete("combo/{id}/item/{comboItemId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task DeleteItemAsync(Guid id, Guid comboItemId)
        {
            /// <summary>
            /// Delete item from combo (Admin/Manager only).
            /// </summary>
            await _appService.DeleteItemAsync(id, comboItemId);
        }
    }
}
