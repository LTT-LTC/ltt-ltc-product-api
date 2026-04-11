using Volo.Abp.Modularity;

namespace LTC.ProductService;

[DependsOn(
    typeof(ProductServiceApplicationModule),
    typeof(ProductServiceDomainTestModule)
)]
public class ProductServiceApplicationTestModule : AbpModule
{

}
