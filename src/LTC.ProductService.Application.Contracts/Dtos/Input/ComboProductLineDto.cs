using System;
using System.ComponentModel.DataAnnotations;

namespace LTC.ProductService.Dtos.Input
{
    /// <summary>
    /// One product line in a combo: the referenced product and the quantity bundled.
    /// Persisted as a JSON entry in <c>Combos.ProductIds</c>.
    /// </summary>
    public class ComboProductLineDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;
    }
}
