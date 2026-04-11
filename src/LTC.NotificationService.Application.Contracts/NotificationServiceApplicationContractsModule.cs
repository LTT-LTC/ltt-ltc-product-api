using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;

namespace LTC.NotificationService;

[DependsOn(
    typeof(NotificationServiceDomainSharedModule),
    typeof(AbpObjectExtendingModule)
)]
public class NotificationServiceApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        NotificationServiceDtoExtensions.Configure();
    }
}
