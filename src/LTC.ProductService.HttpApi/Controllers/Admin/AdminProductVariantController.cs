using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;

namespace LTC.ProductService.Controllers.Admin
{
    [Route(ProductServiceSettingNames.DefaultRoute + "/administration/admin")]
    public class AdminProductVariantController : AbpControllerBase
    {
        private readonly IProductAppService _appService;

        public AdminProductVariantController(IProductAppService appService)
        {
            _appService = appService;
        }

        [HttpPost("product/{id}/variant")]
        public async Task<ProductVariantOutputDto> CreateVariantAsync(Guid id, [FromBody] CreateProductVariantInputDto input)
        {
            return await _appService.CreateVariantAsync(id, input);
        }

        [HttpPut("product/{id}/variant/{variantId}")]
        public async Task<ProductVariantOutputDto> UpdateVariantAsync(Guid id, Guid variantId, [FromBody] UpdateProductVariantInputDto input)
        {
            return await _appService.UpdateVariantAsync(id, variantId, input);
        }

        [HttpDelete("product/{id}/variant/{variantId}")]
        public async Task DeleteVariantAsync(Guid id, Guid variantId)
        {
            await _appService.DeleteVariantAsync(id, variantId);
        }
    }
}
