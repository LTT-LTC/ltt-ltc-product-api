using Xunit;

namespace LTC.ProductService.EntityFrameworkCore;

[CollectionDefinition(ProductServiceTestConsts.CollectionDefinitionName)]
public class ProductServiceEntityFrameworkCoreCollection : ICollectionFixture<ProductServiceEntityFrameworkCoreFixture>
{

}
