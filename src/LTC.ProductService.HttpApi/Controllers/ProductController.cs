using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;

namespace LTC.ProductService.Controllers
{
    public abstract class ProductController : AbpControllerBase
    {
        private readonly IProductAppService _appService;

        public ProductController(IProductAppService appService)
        {
            _appService = appService;
        }

        [HttpGet("product-all")]
        public async Task<PagedResultDto<ProductOutputDto>> GetProductListAsync([FromQuery] GetProductListInputDto input) { return await _appService.GetProductListAsync(input); }

        [HttpGet("product/{id}")]
        public async Task<ProductDetailOutputDto> GetProductAsync(Guid id) { return await _appService.GetProductAsync(id); }

    }
}

