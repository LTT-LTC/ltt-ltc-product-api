using System;
using System.Collections.Generic;

namespace LTC.ProductService.Dtos.Output
{
    public class ComboDetailOutputDto : ComboOutputDto
    {
        public List<ComboItemOutputDto> ComboItems { get; set; } = new();
    }
}
