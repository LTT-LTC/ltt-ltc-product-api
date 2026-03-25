using LTC.NotificationService.Samples;
using Xunit;

namespace LTC.NotificationService.EntityFrameworkCore.Domains;

[Collection(NotificationServiceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<NotificationServiceEntityFrameworkCoreTestModule>
{

}
