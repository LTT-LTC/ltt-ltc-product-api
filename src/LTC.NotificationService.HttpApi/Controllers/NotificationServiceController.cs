using LTC.NotificationService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace LTC.NotificationService.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class NotificationServiceController : AbpControllerBase
{
    protected NotificationServiceController()
    {
        LocalizationResource = typeof(NotificationServiceResource);
    }
}
