using System;
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
    public class CategoryController : AbpControllerBase
    {
        private readonly ICategoryAppService _appService;

        public CategoryController(ICategoryAppService appService)
        {
            _appService = appService;
        }

        [HttpGet("category-all")]
        public async Task<PagedResultDto<CategoryOutputDto>> GetAllAsync([FromQuery] PaginationInputDto input)
        {
            /// <summary>
            /// Get all product categories.
            /// </summary>
            return await _appService.GetAllAsync(input);
        }

        [HttpPost("category")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<CategoryOutputDto> CreateAsync([FromBody] CreateCategoryInputDto input)
        {
            /// <summary>
            /// Create a new category (Admin/Manager only).
            /// </summary>
            return await _appService.CreateAsync(input);
        }

        [HttpPut("category/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<CategoryOutputDto> UpdateAsync(Guid id, [FromBody] UpdateCategoryInputDto input)
        {
            /// <summary>
            /// Update a category (Admin/Manager only).
            /// </summary>
            return await _appService.UpdateAsync(id, input);
        }

        [HttpDelete("category/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task DeleteAsync(Guid id)
        {
            /// <summary>
            /// Delete a category (Admin/Manager only).
            /// </summary>
            await _appService.DeleteAsync(id);
        }
    }
}
