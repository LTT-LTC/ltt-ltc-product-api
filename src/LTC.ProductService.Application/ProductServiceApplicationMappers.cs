using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using LTC.ProductService.Dtos.Output;
using LTC.ProductService.Entities;

namespace LTC.ProductService;

[Mapper]
public partial class ProductServiceApplicationMappers
{
    public partial ProductCategory MapToCategoryOutputDto(ProductCategory source);
    public partial CategoryOutputDto MapToCategoryOutputDto1(ProductCategory source);
    
    public partial ProductOutputDto MapToProductOutputDto(Product source);
    public partial ProductDetailOutputDto MapToProductDetailOutputDto(Product source);
    
    public partial ProductVariantOutputDto MapToProductVariantOutputDto(ProductVariant source);
    
    public partial ComboOutputDto MapToComboOutputDto(Combo source);
    public partial ComboDetailOutputDto MapToComboDetailOutputDto(Combo source);
    
    public partial ComboItemOutputDto MapToComboItemOutputDto(ComboItem source);
}
