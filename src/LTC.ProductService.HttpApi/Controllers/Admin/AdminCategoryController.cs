using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;

namespace LTC.ProductService.Controllers.Admin
{
    [Route(ProductServiceSettingNames.DefaultRoute + "/administration/admin")]
    public class AdminCategoryController : AbpControllerBase
    {
        private readonly ICategoryAppService _appService;

        public AdminCategoryController(ICategoryAppService appService)
        {
            _appService = appService;
        }

        [HttpGet("category-all")]
        public async Task<PagedResultDto<CategoryOutputDto>> GetAllAsync([FromQuery] PaginationInputDto input)
        {
            return await _appService.GetAllAsync(input);
        }

        [HttpPost("category")]
        public async Task<CategoryOutputDto> CreateAsync([FromBody] CreateCategoryInputDto input)
        {
            return await _appService.CreateAsync(input);
        }

        [HttpPut("category/{id}")]
        public async Task<CategoryOutputDto> UpdateAsync(Guid id, [FromBody] UpdateCategoryInputDto input)
        {
            return await _appService.UpdateAsync(id, input);
        }

        [HttpDelete("category/{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _appService.DeleteAsync(id);
        }
    }
}
