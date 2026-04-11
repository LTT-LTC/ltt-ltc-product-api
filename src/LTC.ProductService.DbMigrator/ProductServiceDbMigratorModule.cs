using LTC.ProductService.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace LTC.ProductService.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ProductServiceEntityFrameworkCoreModule),
    typeof(ProductServiceApplicationContractsModule)
    )]
public class ProductServiceDbMigratorModule : AbpModule
{
}
