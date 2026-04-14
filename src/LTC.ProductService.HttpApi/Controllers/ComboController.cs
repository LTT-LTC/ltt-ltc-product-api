using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;

namespace LTC.ProductService.Controllers
{
    [Route(ProductServiceSettingNames.DefaultRoute)]
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
            return await _appService.GetAllAsync(input);
        }

        [HttpGet("combo/{id}")]
        public async Task<ComboDetailOutputDto> GetAsync(Guid id)
        {
            return await _appService.GetAsync(id);
        }
    }
}
