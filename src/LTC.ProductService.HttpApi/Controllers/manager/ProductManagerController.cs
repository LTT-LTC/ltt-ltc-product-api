using Microsoft.AspNetCore.Mvc;

namespace LTC.ProductService.Controllers.Manager
{
    [Route(ProductServiceSettingNames.DefaultRoute + "/manager/product")]
    public class ProductManagerController : ProductController
    {
        public ProductManagerController(IProductAppService appService) : base(appService)
        {
        }
    }
}
