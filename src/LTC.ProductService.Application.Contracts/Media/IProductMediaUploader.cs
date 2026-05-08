using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LTC.ProductService.Media
{
    /// <summary>
    /// Lightweight Cloudinary-backed image uploader used by product/combo create/update
    /// flows and the dedicated media-files upload controllers. Returns the secure URL
    /// of the uploaded asset; nothing is persisted to the product database.
    /// </summary>
    public interface IProductMediaUploader
    {
        /// <summary>
        /// Uploads <paramref name="file"/> to Cloudinary under the product folder and
        /// returns its secure URL. Returns <c>null</c> when <paramref name="file"/> is
        /// null or empty.
        /// </summary>
        Task<MediaUploadOutputDto?> UploadImageAsync(IFormFile? file);
    }

    public class MediaUploadOutputDto
    {
        public string Url { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
    }
}
