using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.VirtualFileSystem;

namespace LTC.NotificationService;

[DependsOn(
    typeof(NotificationServiceApplicationContractsModule)
)]
public class NotificationServiceHttpApiClientModule : AbpModule
{
    public const string RemoteServiceName = "Default";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(NotificationServiceApplicationContractsModule).Assembly,
            RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<NotificationServiceHttpApiClientModule>();
        });
    }
}


