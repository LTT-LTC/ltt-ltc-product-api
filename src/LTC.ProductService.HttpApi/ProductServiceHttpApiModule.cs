using Localization.Resources.AbpUi;
using LTC.ProductService.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace LTC.ProductService;

[DependsOn(
    typeof(ProductServiceApplicationContractsModule)
    )]
public class ProductServiceHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<ProductServiceResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
