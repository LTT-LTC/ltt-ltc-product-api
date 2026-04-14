using System;
using System.Collections.Generic;

namespace LTC.ProductService.Dtos.Output
{
    public class ProductDetailOutputDto : ProductOutputDto
    {
        public List<ProductVariantOutputDto> ProductVariants { get; set; } = new();
    }
}
