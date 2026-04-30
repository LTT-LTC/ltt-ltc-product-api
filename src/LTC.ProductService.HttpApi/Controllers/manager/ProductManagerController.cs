using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;

namespace LTC.ProductService.Controllers.Manager
{
    [Route(ProductServiceSettingNames.DefaultRoute + "/manager/product")]
    [Authorize(Roles = "Manager,manager")]
    public class ProductManagerController : ProductController
    {
        private readonly IProductAppService _appService;

        public ProductManagerController(IProductAppService appService) : base(appService)
        {
            _appService = appService;
        }

        [HttpPost("product")]
        public Task<ProductOutputDto> CreateProductAsync([FromBody] CreateProductInputDto input) => _appService.CreateProductAsync(input);

        [HttpPut("product/{id}")]
        public Task<ProductOutputDto> UpdateProductAsync(Guid id, [FromBody] UpdateProductInputDto input) => _appService.UpdateProductAsync(id, input);

        [HttpDelete("product/{id}")]
        public Task DeleteProductAsync(Guid id) => _appService.DeleteProductAsync(id);

        [HttpDelete("product")]
        public Task DeleteBulkAsync([FromBody] List<Guid> ids) => _appService.DeleteBulkAsync(ids);

        [HttpPost("product/{id}/variant")]
        public Task<ProductVariantOutputDto> CreateVariantAsync(Guid id, [FromBody] CreateProductVariantInputDto input) =>
            _appService.CreateVariantAsync(id, input);

        [HttpPut("product/{id}/variant/{variantId}")]
        public Task<ProductVariantOutputDto> UpdateVariantAsync(Guid id, Guid variantId, [FromBody] UpdateProductVariantInputDto input) =>
            _appService.UpdateVariantAsync(id, variantId, input);

        [HttpDelete("product/{id}/variant/{variantId}")]
        public Task DeleteVariantAsync(Guid id, Guid variantId) => _appService.DeleteVariantAsync(id, variantId);
    }
}
