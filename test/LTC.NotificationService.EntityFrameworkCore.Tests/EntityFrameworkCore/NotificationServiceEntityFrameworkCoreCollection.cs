using Xunit;

namespace LTC.NotificationService.EntityFrameworkCore;

[CollectionDefinition(NotificationServiceTestConsts.CollectionDefinitionName)]
public class NotificationServiceEntityFrameworkCoreCollection : ICollectionFixture<NotificationServiceEntityFrameworkCoreFixture>
{

}
