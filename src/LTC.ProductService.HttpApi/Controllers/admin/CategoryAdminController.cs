using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;

namespace LTC.ProductService.Controllers.Admin
{
    [Route(ProductServiceSettingNames.DefaultRoute + "/admin/category")]
    [Authorize(Roles = "Admin,admin")]
    public class CategoryAdminController : CategoryController
    {
        private readonly ICategoryAppService _appService;

        public CategoryAdminController(ICategoryAppService appService) : base(appService)
        {
            _appService = appService;
        }

        [HttpPost("category")]
        public Task<CategoryOutputDto> CreateCategoryAsync([FromBody] CreateCategoryInputDto input) => _appService.CreateCategoryAsync(input);

        [HttpPut("category/{id}")]
        public Task<CategoryOutputDto> UpdateCategoryAsync(Guid id, [FromBody] UpdateCategoryInputDto input) => _appService.UpdateCategoryAsync(id, input);

        [HttpDelete("category/{id}")]
        public Task DeleteCategoryAsync(Guid id) => _appService.DeleteCategoryAsync(id);
    }
}
