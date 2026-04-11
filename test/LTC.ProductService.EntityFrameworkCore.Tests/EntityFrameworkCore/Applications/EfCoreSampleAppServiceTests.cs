using LTC.ProductService.Samples;
using Xunit;

namespace LTC.ProductService.EntityFrameworkCore.Applications;

[Collection(ProductServiceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ProductServiceEntityFrameworkCoreTestModule>
{

}
