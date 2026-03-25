using Volo.Abp.Modularity;

namespace LTC.NotificationService;

[DependsOn(
    typeof(NotificationServiceDomainModule),
    typeof(NotificationServiceTestBaseModule)
)]
public class NotificationServiceDomainTestModule : AbpModule
{

}
