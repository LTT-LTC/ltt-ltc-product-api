using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;
using LTC.ProductService.Entities;
using LTC.ProductService.Media;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace LTC.ProductService
{
    public class ProductAppService : ProductServiceAppService, IProductAppService
    {
        private readonly IRepository<Product, Guid> _repository;
        private readonly IProductMediaUploader _mediaUploader;

        public ProductAppService(
            IRepository<Product, Guid> repository,
            IProductMediaUploader mediaUploader)
        {
            _repository = repository;
            _mediaUploader = mediaUploader;
        }

        public async Task<PagedResultDto<ProductOutputDto>> GetProductListAsync(GetProductListInputDto input)
        {
            var query = await _repository.GetQueryableAsync();

            if (input.CategoryId.HasValue)
            {
                query = query.Where(x => x.ProductCategoryId == input.CategoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(input.Keyword))
            {
                query = query.Where(x => x.Name.Contains(input.Keyword));
            }

            var totalCount = await AsyncExecuter.CountAsync(query);
            var items = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ProductOutputDto>(
                totalCount,
                items.Select(MapProduct).ToList()
            );
        }

        public async Task<ProductOutputDto> GetProductAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return MapProduct(entity);
        }

        public async Task<ProductOutputDto> CreateProductAsync(CreateProductInputDto input)
        {
            var imageUrl = await ResolveImageUrlAsync(input.ImageFile, input.ImageUrl);

            var entity = new Product
            {
                ProductCategoryId = input.ProductCategoryId,
                Name = input.Name,
                Description = input.Description,
                BasePrice = input.BasePrice,
                ImageUrl = imageUrl,
                IsActive = input.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.InsertAsync(entity);
            return MapProduct(entity);
        }

        public async Task<ProductOutputDto> UpdateProductAsync(Guid id, UpdateProductInputDto input)
        {
            var entity = await _repository.GetAsync(id);

            var imageUrl = await ResolveImageUrlAsync(input.ImageFile, input.ImageUrl ?? entity.ImageUrl);

            entity.ProductCategoryId = input.ProductCategoryId;
            entity.Name = input.Name;
            entity.Description = input.Description;
            entity.BasePrice = input.BasePrice;
            entity.ImageUrl = imageUrl;
            entity.IsActive = input.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(entity);
            return MapProduct(entity);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task DeleteBulkAsync(List<Guid> ids)
        {
            await _repository.DeleteManyAsync(ids);
        }

        private async Task<string?> ResolveImageUrlAsync(Microsoft.AspNetCore.Http.IFormFile? file, string? fallbackUrl)
        {
            if (file != null && file.Length > 0)
            {
                var uploaded = await _mediaUploader.UploadImageAsync(file);
                return uploaded?.Url ?? fallbackUrl;
            }

            return fallbackUrl;
        }

        private static ProductOutputDto MapProduct(Product source)
        {
            return new ProductOutputDto
            {
                Id = source.Id,
                TenantId = source.TenantId,
                ProductCategoryId = source.ProductCategoryId,
                Name = source.Name,
                Description = source.Description,
                BasePrice = source.BasePrice,
                ImageUrl = source.ImageUrl,
                IsActive = source.IsActive,
                CreatedAt = source.CreatedAt,
                UpdatedAt = source.UpdatedAt,
                CreationTime = source.CreationTime,
                CreatorId = source.CreatorId,
                LastModificationTime = source.LastModificationTime,
                LastModifierId = source.LastModifierId,
                IsDeleted = source.IsDeleted,
                DeleterId = source.DeleterId,
                DeletionTime = source.DeletionTime
            };
        }
    }
}
