using Microsoft.Extensions.Localization;
using LTC.ProductService.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace LTC.ProductService;

[Dependency(ReplaceServices = true)]
public class ProductServiceBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<ProductServiceResource> _localizer;

    public ProductServiceBrandingProvider(IStringLocalizer<ProductServiceResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
