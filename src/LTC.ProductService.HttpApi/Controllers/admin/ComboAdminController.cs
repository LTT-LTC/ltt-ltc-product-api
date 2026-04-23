using Microsoft.AspNetCore.Mvc;

namespace LTC.ProductService.Controllers.Admin
{
    [Route(ProductServiceSettingNames.DefaultRoute + "/admin/combo")]
    public class ComboAdminController : ComboController
    {
        public ComboAdminController(IComboAppService appService) : base(appService)
        {
        }
    }
}
