using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using LTC.ProductService.Media;
using Microsoft.AspNetCore.Http;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace LTC.ProductService.Media
{
    public class ProductMediaUploader : IProductMediaUploader, ITransientDependency
    {
        private const string ParentFolder = "LTC/products";
        private const long MaxFileSize = 20L * 1024 * 1024;

        private readonly Cloudinary _cloudinary;
        private readonly ICurrentTenant _currentTenant;

        public ProductMediaUploader(Cloudinary cloudinary, ICurrentTenant currentTenant)
        {
            _cloudinary = cloudinary;
            _currentTenant = currentTenant;
        }

        public async Task<MediaUploadOutputDto?> UploadImageAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            if (file.Length > MaxFileSize)
            {
                throw new UserFriendlyException("Uploaded file exceeds the 20MB limit.");
            }

            var tenantId = _currentTenant.Id ?? Guid.Empty;
            var folder = $"{ParentFolder}/{tenantId}";
            var publicId = $"{tenantId}_{Guid.CreateVersion7()}";
            var fileName = string.IsNullOrWhiteSpace(file.FileName) ? "image" : Path.GetFileName(file.FileName);

            await using var stream = file.OpenReadStream();
            var description = new FileDescription(fileName, stream);

            var uploadParams = new ImageUploadParams
            {
                File = description,
                Folder = folder,
                PublicId = publicId,
                Overwrite = true,
                Tags = $"tenant-{tenantId},product-image"
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            if (result.StatusCode != HttpStatusCode.OK || result.SecureUrl == null)
            {
                throw new UserFriendlyException("Failed to upload image to Cloudinary.");
            }

            return new MediaUploadOutputDto
            {
                Url = result.SecureUrl.ToString(),
                PublicId = result.PublicId
            };
        }
    }
}
