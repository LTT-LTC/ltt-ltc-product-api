using System;
using System.Threading.Tasks;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace LTC.ProductService.Controllers.Customer
{
    [Route(ProductServiceSettingNames.DefaultRoute)]
    public class ComboCustomerController : AbpControllerBase
    {
        private readonly IComboAppService _appService;

        public ComboCustomerController(IComboAppService appService)
        {
            _appService = appService;
        }

        [AllowAnonymous]
        [HttpGet("combo-all")]
        public Task<PagedResultDto<ComboOutputDto>> GetComboListAsync([FromQuery] PaginationInputDto input)
        {
            return _appService.GetComboListAsync(input);
        }

        [AllowAnonymous]
        [HttpGet("combo/{id}")]
        public Task<ComboOutputDto> GetComboAsync(Guid id)
        {
            return _appService.GetComboAsync(id);
        }
    }
}
