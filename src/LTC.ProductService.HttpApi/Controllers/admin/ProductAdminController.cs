using Microsoft.AspNetCore.Mvc;

namespace LTC.ProductService.Controllers.Admin
{
    [Route(ProductServiceSettingNames.DefaultRoute + "/admin/product")]
    public class ProductAdminController : ProductController
    {
        public ProductAdminController(IProductAppService appService) : base(appService)
        {
        }
    }
}
