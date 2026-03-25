using Volo.Abp.Modularity;

namespace LTC.NotificationService;

public abstract class NotificationServiceApplicationTestBase<TStartupModule> : NotificationServiceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
