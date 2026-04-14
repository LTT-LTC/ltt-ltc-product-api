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
    public class ProductController : AbpControllerBase
    {
        private readonly IProductAppService _appService;

        public ProductController(IProductAppService appService)
        {
            _appService = appService;
        }

        [HttpGet("product-all")]
        public async Task<PagedResultDto<ProductOutputDto>> GetAllAsync([FromQuery] GetProductListInputDto input)
        {
            return await _appService.GetAllAsync(input);
        }

        [HttpGet("product/{id}")]
        public async Task<ProductDetailOutputDto> GetAsync(Guid id)
        {
            return await _appService.GetAsync(id);
        }
    }
}
