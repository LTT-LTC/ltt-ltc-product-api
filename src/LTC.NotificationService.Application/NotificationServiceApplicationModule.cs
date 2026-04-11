using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace LTC.NotificationService;

[DependsOn(
    typeof(NotificationServiceDomainModule),
    typeof(NotificationServiceApplicationContractsModule)
    )]
public class NotificationServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<NotificationServiceApplicationModule>();
    }
}
