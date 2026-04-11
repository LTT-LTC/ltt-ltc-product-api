using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;

namespace LTC.ProductService;

[DependsOn(
    typeof(ProductServiceDomainSharedModule),
    typeof(AbpObjectExtendingModule)
)]
public class ProductServiceApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        ProductServiceDtoExtensions.Configure();
    }
}
