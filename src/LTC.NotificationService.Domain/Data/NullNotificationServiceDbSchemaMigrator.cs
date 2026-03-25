using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace LTC.NotificationService.Data;

/* This is used if database provider does't define
 * INotificationServiceDbSchemaMigrator implementation.
 */
public class NullNotificationServiceDbSchemaMigrator : INotificationServiceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
