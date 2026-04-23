using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace LTC.ProductService.Controllers.Manager
{
    /// <summary>
    /// Base controller for Manager operations in ProductService.
    /// Managers have read access and limited write access per entity policy.
    /// </summary>
    [RemoteService]
    [Area("manager")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public abstract class ManagerProductControllerBase : AbpControllerBase
    {
    }
}
