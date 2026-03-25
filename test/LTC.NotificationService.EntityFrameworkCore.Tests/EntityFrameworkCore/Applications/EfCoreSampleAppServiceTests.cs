using LTC.NotificationService.Samples;
using Xunit;

namespace LTC.NotificationService.EntityFrameworkCore.Applications;

[Collection(NotificationServiceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<NotificationServiceEntityFrameworkCoreTestModule>
{

}
