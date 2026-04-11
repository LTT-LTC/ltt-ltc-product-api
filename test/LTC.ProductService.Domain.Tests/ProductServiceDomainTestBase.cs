using Volo.Abp.Modularity;

namespace LTC.ProductService;

/* Inherit from this class for your domain layer tests. */
public abstract class ProductServiceDomainTestBase<TStartupModule> : ProductServiceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
