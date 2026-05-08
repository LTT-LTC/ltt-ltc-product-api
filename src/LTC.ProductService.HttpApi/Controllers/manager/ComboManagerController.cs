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
        [Consumes("multipart/form-data")]
        public Task<ComboOutputDto> CreateComboAsync([FromForm] CreateComboInputDto input) => _appService.CreateComboAsync(input);

        [HttpPut("combo/{id}")]
        [Consumes("multipart/form-data")]
        public Task<ComboOutputDto> UpdateComboAsync(Guid id, [FromForm] UpdateComboInputDto input) => _appService.UpdateComboAsync(id, input);

        [HttpDelete("combo/{id}")]
        public Task DeleteComboAsync(Guid id) => _appService.DeleteComboAsync(id);
    }
}
