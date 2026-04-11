using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace LTC.ProductService.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class ProductServiceDbContext : AbpDbContext<ProductServiceDbContext>
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    public ProductServiceDbContext(DbContextOptions<ProductServiceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(ProductServiceConsts.DbTablePrefix + "YourEntities", ProductServiceConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }
}
