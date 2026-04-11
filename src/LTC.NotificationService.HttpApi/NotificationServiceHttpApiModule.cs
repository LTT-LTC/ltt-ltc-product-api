using Localization.Resources.AbpUi;
using LTC.NotificationService.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace LTC.NotificationService;

[DependsOn(
    typeof(NotificationServiceApplicationContractsModule)
    )]
public class NotificationServiceHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<NotificationServiceResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
