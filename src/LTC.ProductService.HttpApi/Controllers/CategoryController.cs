using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;

namespace LTC.ProductService.Controllers
{
    public abstract class CategoryController : AbpControllerBase
    {
        private readonly ICategoryAppService _appService;

        public CategoryController(ICategoryAppService appService)
        {
            _appService = appService;
        }

        [HttpGet("category-all")]
        public async Task<PagedResultDto<CategoryOutputDto>> GetCategoryListAsync([FromQuery] PaginationInputDto input)
        {
            /// <summary>
            /// Get all product categories.
            /// </summary>
            return await _appService.GetCategoryListAsync(input);
        }

    }
}
