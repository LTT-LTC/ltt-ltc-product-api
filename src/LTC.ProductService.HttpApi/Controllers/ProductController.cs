using System;
using System.Collections.Generic;
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
    public class ProductController : AbpControllerBase
    {
        private readonly IProductAppService _appService;

        public ProductController(IProductAppService appService)
        {
            _appService = appService;
        }

        [HttpGet("product-all")]
        public async Task<PagedResultDto<ProductOutputDto>> GetAllAsync([FromQuery] GetProductListInputDto input) { return await _appService.GetAllAsync(input); }

        [HttpGet("product/{id}")]
        public async Task<ProductDetailOutputDto> GetAsync(Guid id) { return await _appService.GetAsync(id); }

        [HttpPost("product")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ProductOutputDto> CreateAsync([FromBody] CreateProductInputDto input) { return await _appService.CreateAsync(input); }

        [HttpPut("product/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ProductOutputDto> UpdateAsync(Guid id, [FromBody] UpdateProductInputDto input) { return await _appService.UpdateAsync(id, input); }

        [HttpDelete("product/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task DeleteAsync(Guid id) { await _appService.DeleteAsync(id); }

        [HttpDelete("product")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task DeleteBulkAsync([FromBody] List<Guid> ids) { await _appService.DeleteBulkAsync(ids); }

        #region Product Variants

        [HttpPost("product/{id}/variant")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ProductVariantOutputDto> CreateVariantAsync(Guid id, [FromBody] CreateProductVariantInputDto input)
        {
            /// <summary>
            /// Create a product variant (Admin/Manager only).
            /// </summary>
            return await _appService.CreateVariantAsync(id, input);
        }

        [HttpPut("product/{id}/variant/{variantId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ProductVariantOutputDto> UpdateVariantAsync(Guid id, Guid variantId, [FromBody] UpdateProductVariantInputDto input)
        {
            /// <summary>
            /// Update a product variant (Admin/Manager only).
            /// </summary>
            return await _appService.UpdateVariantAsync(id, variantId, input);
        }

        [HttpDelete("product/{id}/variant/{variantId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task DeleteVariantAsync(Guid id, Guid variantId)
        {
            /// <summary>
            /// Delete a product variant (Admin/Manager only).
            /// </summary>
            await _appService.DeleteVariantAsync(id, variantId);
        }

        #endregion
    }
}

