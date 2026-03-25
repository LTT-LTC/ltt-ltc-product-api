using System;
using System.Collections.Generic;
using System.Text;
using LTC.NotificationService.Localization;
using Volo.Abp.Application.Services;

namespace LTC.NotificationService;

/* Inherit your application services from this class.
 */
public abstract class NotificationServiceAppService : ApplicationService
{
    protected NotificationServiceAppService()
    {
        LocalizationResource = typeof(NotificationServiceResource);
    }
}
