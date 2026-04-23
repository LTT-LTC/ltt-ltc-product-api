using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace LTC.ProductService.Controllers.Customer
{
    [RemoteService]
    [Area("customer")]
    [ApiController]
    [Authorize]
    public abstract class CustomerProductControllerBase : AbpControllerBase
    {
    }
}
