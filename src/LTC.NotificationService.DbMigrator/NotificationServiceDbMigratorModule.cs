using LTC.NotificationService.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace LTC.NotificationService.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(NotificationServiceEntityFrameworkCoreModule),
    typeof(NotificationServiceApplicationContractsModule)
    )]
public class NotificationServiceDbMigratorModule : AbpModule
{
}
