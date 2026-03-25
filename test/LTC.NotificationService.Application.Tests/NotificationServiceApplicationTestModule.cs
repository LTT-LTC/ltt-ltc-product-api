using Volo.Abp.Modularity;

namespace LTC.NotificationService;

[DependsOn(
    typeof(NotificationServiceApplicationModule),
    typeof(NotificationServiceDomainTestModule)
)]
public class NotificationServiceApplicationTestModule : AbpModule
{

}
