using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;

namespace LTC.ProductService.Controllers
{
    public abstract class ComboController : AbpControllerBase
    {
        private readonly IComboAppService _appService;

        public ComboController(IComboAppService appService)
        {
            _appService = appService;
        }

        [HttpGet("combo-all")]
        public async Task<PagedResultDto<ComboOutputDto>> GetComboListAsync([FromQuery] PaginationInputDto input)
        {
            /// <summary>
            /// Get all combos.
            /// </summary>
            return await _appService.GetComboListAsync(input);
        }

        [HttpGet("combo/{id}")]
        public async Task<ComboDetailOutputDto> GetComboAsync(Guid id)
        {
            /// <summary>
            /// Get combo details by id.
            /// </summary>
            return await _appService.GetComboAsync(id);
        }

    }
}
