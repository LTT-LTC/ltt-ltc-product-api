using Volo.Abp.Modularity;

namespace LTC.ProductService;

[DependsOn(
    typeof(ProductServiceDomainModule),
    typeof(ProductServiceTestBaseModule)
)]
public class ProductServiceDomainTestModule : AbpModule
{

}
