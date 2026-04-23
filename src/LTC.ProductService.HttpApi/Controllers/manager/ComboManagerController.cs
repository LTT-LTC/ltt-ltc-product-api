using Microsoft.AspNetCore.Mvc;

namespace LTC.ProductService.Controllers.Manager
{
    [Route(ProductServiceSettingNames.DefaultRoute + "/manager/combo")]
    public class ComboManagerController : ComboController
    {
        public ComboManagerController(IComboAppService appService) : base(appService)
        {
        }
    }
}
