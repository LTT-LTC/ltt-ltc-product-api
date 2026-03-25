using Volo.Abp.Modularity;

namespace LTC.NotificationService;

/* Inherit from this class for your domain layer tests. */
public abstract class NotificationServiceDomainTestBase<TStartupModule> : NotificationServiceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
