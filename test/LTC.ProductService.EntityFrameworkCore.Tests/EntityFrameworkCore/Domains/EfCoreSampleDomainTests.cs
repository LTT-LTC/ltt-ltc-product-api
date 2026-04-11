using LTC.ProductService.Samples;
using Xunit;

namespace LTC.ProductService.EntityFrameworkCore.Domains;

[Collection(ProductServiceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ProductServiceEntityFrameworkCoreTestModule>
{

}
