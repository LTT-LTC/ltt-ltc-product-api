using Microsoft.AspNetCore.Mvc;

namespace LTC.ProductService.Controllers.Admin
{
    [Route(ProductServiceSettingNames.DefaultRoute + "/admin/category")]
    public class CategoryAdminController : CategoryController
    {
        public CategoryAdminController(ICategoryAppService appService) : base(appService)
        {
        }
    }
}
