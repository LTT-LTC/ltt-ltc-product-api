using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;

namespace LTC.ProductService.Controllers.Manager
{
    [Route(ProductServiceSettingNames.DefaultRoute + "/manager/combo")]
    [Authorize(Roles = "Manager,manager")]
    public class ComboManagerController : ComboController
    {
        private readonly IComboAppService _appService;

        public ComboManagerController(IComboAppService appService) : base(appService)
        {
            _appService = appService;
        }

        [HttpPost("combo")]
        public Task<ComboOutputDto> CreateComboAsync([FromBody] CreateComboInputDto input) => _appService.CreateComboAsync(input);

        [HttpPut("combo/{id}")]
        public Task<ComboOutputDto> UpdateComboAsync(Guid id, [FromBody] UpdateComboInputDto input) => _appService.UpdateComboAsync(id, input);

        [HttpDelete("combo/{id}")]
        public Task DeleteComboAsync(Guid id) => _appService.DeleteComboAsync(id);

        [HttpPost("combo/{id}/item")]
        public Task<ComboItemOutputDto> AddItemAsync(Guid id, [FromBody] CreateComboItemInputDto input) => _appService.AddItemAsync(id, input);

        [HttpDelete("combo/{id}/item/{comboItemId}")]
        public Task DeleteItemAsync(Guid id, Guid comboItemId) => _appService.DeleteItemAsync(id, comboItemId);
    }
}
