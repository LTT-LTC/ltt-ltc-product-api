using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using LTC.ProductService.Entities;

namespace LTC.ProductService.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class ProductServiceDbContext : AbpDbContext<ProductServiceDbContext>
{
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }
    public DbSet<Combo> Combos { get; set; }
    public DbSet<ComboItem> ComboItems { get; set; }

    public ProductServiceDbContext(DbContextOptions<ProductServiceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ProductCategory>(b =>
        {
            b.ToTable("ProductCategories");
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(256);
        });

        builder.Entity<Product>(b =>
        {
            b.ToTable("Products");
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(256);
            b.Property(x => x.BasePrice).HasColumnType("decimal(18,2)");
            b.HasOne(x => x.ProductCategory).WithMany().HasForeignKey(x => x.ProductCategoryId);
        });

        builder.Entity<ProductVariant>(b =>
        {
            b.ToTable("ProductVariants");
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(256);
            b.Property(x => x.AdditionalPrice).HasColumnType("decimal(18,2)");
            b.HasOne(x => x.Product).WithMany(x => x.ProductVariants).HasForeignKey(x => x.ProductId);
        });

        builder.Entity<Combo>(b =>
        {
            b.ToTable("Combos");
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(256);
            b.Property(x => x.TotalPrice).HasColumnType("decimal(18,2)");
        });

        builder.Entity<ComboItem>(b =>
        {
            b.ToTable("ComboItems");
            b.ConfigureByConvention();
            b.HasOne(x => x.Combo).WithMany(x => x.ComboItems).HasForeignKey(x => x.ComboId);
            b.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
        });
    }
}
