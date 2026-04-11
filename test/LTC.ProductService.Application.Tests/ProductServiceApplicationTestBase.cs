using Volo.Abp.Modularity;

namespace LTC.ProductService;

public abstract class ProductServiceApplicationTestBase<TStartupModule> : ProductServiceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
