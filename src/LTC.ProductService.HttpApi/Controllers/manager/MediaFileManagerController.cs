using System.Threading.Tasks;
using LTC.ProductService.Media;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace LTC.ProductService.Controllers.Manager
{
    /// <summary>
    /// Cloudinary-backed image upload endpoint for the manager dashboard. Mirrors the
    /// admin variant: returns the secure URL only; nothing is persisted server-side.
    /// </summary>
    [Route(ProductServiceSettingNames.DefaultRoute + "/manager/media-files")]
    [Authorize(Roles = "Manager,manager")]
    public class MediaFileManagerController : AbpControllerBase
    {
        private readonly IProductMediaUploader _uploader;

        public MediaFileManagerController(IProductMediaUploader uploader)
        {
            _uploader = uploader;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadAsync([FromForm] IFormFile file)
        {
            var result = await _uploader.UploadImageAsync(file);
            return Ok(result);
        }
    }
}
