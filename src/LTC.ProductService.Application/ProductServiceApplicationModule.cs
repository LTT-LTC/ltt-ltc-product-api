using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace LTC.ProductService;

[DependsOn(
    typeof(ProductServiceDomainModule),
    typeof(ProductServiceApplicationContractsModule)
    )]
public class ProductServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<ProductServiceApplicationModule>();
    }
}
