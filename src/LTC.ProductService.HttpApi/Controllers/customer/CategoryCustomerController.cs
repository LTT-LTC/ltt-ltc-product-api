using System.Threading.Tasks;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace LTC.ProductService.Controllers.Customer
{
    [Route(ProductServiceSettingNames.DefaultRoute)]
    public class CategoryCustomerController : AbpControllerBase
    {
        private readonly ICategoryAppService _appService;

        public CategoryCustomerController(ICategoryAppService appService)
        {
            _appService = appService;
        }

        [AllowAnonymous]
        [HttpGet("category-all")]
        public Task<PagedResultDto<CategoryOutputDto>> GetCategoryListAsync([FromQuery] PaginationInputDto input)
        {
            return _appService.GetCategoryListAsync(input);
        }
    }
}
