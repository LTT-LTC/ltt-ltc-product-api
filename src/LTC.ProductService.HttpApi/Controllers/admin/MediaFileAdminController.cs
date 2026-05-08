using System.Threading.Tasks;
using LTC.ProductService.Media;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace LTC.ProductService.Controllers.Admin
{
    /// <summary>
    /// Cloudinary-backed image upload endpoint for the admin dashboard. The product
    /// service does not persist uploaded files in its own database - the secure URL is
    /// returned for the caller to attach onto a product/combo create or update payload.
    /// </summary>
    [Route(ProductServiceSettingNames.DefaultRoute + "/admin/media-files")]
    [Authorize(Roles = "Admin,admin")]
    public class MediaFileAdminController : AbpControllerBase
    {
        private readonly IProductMediaUploader _uploader;

        public MediaFileAdminController(IProductMediaUploader uploader)
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
