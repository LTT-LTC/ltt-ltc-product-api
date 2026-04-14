using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;

namespace LTC.ProductService.Controllers.Admin
{
    [Route(ProductServiceSettingNames.DefaultRoute + "/administration/admin")]
    public class AdminProductController : AbpControllerBase
    {
        private readonly IProductAppService _appService;

        public AdminProductController(IProductAppService appService)
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

        [HttpPost("product")]
        public async Task<ProductOutputDto> CreateAsync([FromBody] CreateProductInputDto input)
        {
            return await _appService.CreateAsync(input);
        }

        [HttpPut("product/{id}")]
        public async Task<ProductOutputDto> UpdateAsync(Guid id, [FromBody] UpdateProductInputDto input)
        {
            return await _appService.UpdateAsync(id, input);
        }

        [HttpDelete("product/{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _appService.DeleteAsync(id);
        }

        [HttpDelete("product")]
        public async Task DeleteBulkAsync([FromBody] List<Guid> ids)
        {
            await _appService.DeleteBulkAsync(ids);
        }
    }
}
