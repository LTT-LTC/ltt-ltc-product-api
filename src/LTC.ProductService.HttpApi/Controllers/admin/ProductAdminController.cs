using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;

namespace LTC.ProductService.Controllers.Admin
{
    [Route(ProductServiceSettingNames.DefaultRoute + "/admin/product")]
    [Authorize(Roles = "Admin,admin")]
    public class ProductAdminController : ProductController
    {
        private readonly IProductAppService _appService;

        public ProductAdminController(IProductAppService appService) : base(appService)
        {
            _appService = appService;
        }

        [HttpPost("product")]
        [Consumes("multipart/form-data")]
        public Task<ProductOutputDto> CreateProductAsync([FromForm] CreateProductInputDto input) => _appService.CreateProductAsync(input);

        [HttpPut("product/{id}")]
        [Consumes("multipart/form-data")]
        public Task<ProductOutputDto> UpdateProductAsync(Guid id, [FromForm] UpdateProductInputDto input) => _appService.UpdateProductAsync(id, input);

        [HttpDelete("product/{id}")]
        public Task DeleteProductAsync(Guid id) => _appService.DeleteProductAsync(id);

        [HttpDelete("product")]
        public Task DeleteBulkAsync([FromBody] List<Guid> ids) => _appService.DeleteBulkAsync(ids);
    }
}
