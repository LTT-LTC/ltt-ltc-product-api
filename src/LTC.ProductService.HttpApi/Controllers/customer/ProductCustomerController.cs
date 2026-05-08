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
    public class ProductCustomerController : AbpControllerBase
    {
        private readonly IProductAppService _appService;

        public ProductCustomerController(IProductAppService appService)
        {
            _appService = appService;
        }

        [AllowAnonymous]
        [HttpGet("product-all")]
        public Task<PagedResultDto<ProductOutputDto>> GetProductListAsync([FromQuery] GetProductListInputDto input)
        {
            return _appService.GetProductListAsync(input);
        }

        [AllowAnonymous]
        [HttpGet("product/{id}")]
        public Task<ProductOutputDto> GetProductAsync(Guid id)
        {
            return _appService.GetProductAsync(id);
        }
    }
}
