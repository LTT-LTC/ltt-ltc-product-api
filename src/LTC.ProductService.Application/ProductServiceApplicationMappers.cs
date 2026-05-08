using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using LTC.ProductService.Dtos.Output;
using LTC.ProductService.Entities;

namespace LTC.ProductService;

[Mapper]
public partial class ProductServiceApplicationMappers
{
    public partial CategoryOutputDto MapToCategoryOutputDto(ProductCategory source);

    public partial ProductOutputDto MapToProductOutputDto(Product source);

    [MapperIgnoreSource(nameof(Combo.ProductIds))]
    public partial ComboOutputDto MapToComboOutputDto(Combo source);
}
