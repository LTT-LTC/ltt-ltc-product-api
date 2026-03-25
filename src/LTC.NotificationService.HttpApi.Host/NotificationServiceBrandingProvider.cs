using Microsoft.Extensions.Localization;
using LTC.NotificationService.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace LTC.NotificationService;

[Dependency(ReplaceServices = true)]
public class NotificationServiceBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<NotificationServiceResource> _localizer;

    public NotificationServiceBrandingProvider(IStringLocalizer<NotificationServiceResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
