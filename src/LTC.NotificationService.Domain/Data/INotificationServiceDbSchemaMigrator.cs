using System.Threading.Tasks;

namespace LTC.NotificationService.Data;

public interface INotificationServiceDbSchemaMigrator
{
    Task MigrateAsync();
}
