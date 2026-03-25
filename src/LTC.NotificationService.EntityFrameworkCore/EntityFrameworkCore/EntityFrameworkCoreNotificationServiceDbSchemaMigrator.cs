using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LTC.NotificationService.Data;
using Volo.Abp.DependencyInjection;

namespace LTC.NotificationService.EntityFrameworkCore;

public class EntityFrameworkCoreNotificationServiceDbSchemaMigrator
    : INotificationServiceDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreNotificationServiceDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the NotificationServiceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<NotificationServiceDbContext>()
            .Database
            .MigrateAsync();
    }
}
