using Microsoft.AspNetCore.Mvc;

namespace LTC.ProductService.Controllers.Manager
{
    [Route(ProductServiceSettingNames.DefaultRoute + "/manager/category")]
    public class CategoryManagerController : CategoryController
    {
        public CategoryManagerController(ICategoryAppService appService) : base(appService)
        {
        }
    }
}
