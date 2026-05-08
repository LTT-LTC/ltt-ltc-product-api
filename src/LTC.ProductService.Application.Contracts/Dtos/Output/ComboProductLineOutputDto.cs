using System;

namespace LTC.ProductService.Dtos.Output
{
    /// <summary>
    /// Resolved combo line: the bound product (if still found) plus quantity.
    /// </summary>
    public class ComboProductLineOutputDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public ProductOutputDto? Product { get; set; }
    }
}
