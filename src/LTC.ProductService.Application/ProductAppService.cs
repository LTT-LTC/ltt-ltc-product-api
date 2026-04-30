using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;
using LTC.ProductService.Entities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LTC.ProductService
{
    public class ProductAppService : ProductServiceAppService, IProductAppService
    {
        private readonly IRepository<Product, Guid> _repository;
        private readonly IRepository<ProductVariant, Guid> _variantRepository;

        public ProductAppService(
            IRepository<Product, Guid> repository,
            IRepository<ProductVariant, Guid> variantRepository)
        {
            _repository = repository;
            _variantRepository = variantRepository;
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

        public async Task<ProductDetailOutputDto> GetProductAsync(Guid id)
        {
            var query = await _repository.WithDetailsAsync(x => x.ProductVariants);
            var entity = await AsyncExecuter.FirstOrDefaultAsync(query.Where(x => x.Id == id));
            
            if (entity == null)
            {
                throw new Volo.Abp.UserFriendlyException("Product not found");
            }
            
            return MapProductDetail(entity);
        }

        public async Task<ProductOutputDto> CreateProductAsync(CreateProductInputDto input)
        {
            var entity = new Product
            {
                ProductCategoryId = input.ProductCategoryId,
                Name = input.Name,
                Description = input.Description,
                BasePrice = input.BasePrice,
                ImageUrl = input.ImageUrl,
                IsActive = input.IsActive,
                ProductType = input.ProductType
            };

            await _repository.InsertAsync(entity);
            return MapProduct(entity);
        }

        public async Task<ProductOutputDto> UpdateProductAsync(Guid id, UpdateProductInputDto input)
        {
            var entity = await _repository.GetAsync(id);

            entity.ProductCategoryId = input.ProductCategoryId;
            entity.Name = input.Name;
            entity.Description = input.Description;
            entity.BasePrice = input.BasePrice;
            entity.ImageUrl = input.ImageUrl;
            entity.IsActive = input.IsActive;
            entity.ProductType = input.ProductType;

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

        public async Task<ProductVariantOutputDto> CreateVariantAsync(Guid id, CreateProductVariantInputDto input)
        {
            var variant = new ProductVariant
            {
                ProductId = id,
                Name = input.Name,
                AdditionalPrice = input.AdditionalPrice,
                IsActive = input.IsActive
            };

            await _variantRepository.InsertAsync(variant);
            return MapVariant(variant);
        }

        public async Task<ProductVariantOutputDto> UpdateVariantAsync(Guid id, Guid variantId, UpdateProductVariantInputDto input)
        {
            var variant = await _variantRepository.GetAsync(variantId);
            
            if (variant.ProductId != id)
            {
                throw new Volo.Abp.UserFriendlyException("Variant doesn't belong to product");
            }

            variant.Name = input.Name;
            variant.AdditionalPrice = input.AdditionalPrice;
            variant.IsActive = input.IsActive;

            await _variantRepository.UpdateAsync(variant);
            return MapVariant(variant);
        }

        public async Task DeleteVariantAsync(Guid id, Guid variantId)
        {
            var variant = await _variantRepository.GetAsync(variantId);
            if (variant.ProductId == id)
            {
                await _variantRepository.DeleteAsync(variantId);
            }
        }

        private static ProductOutputDto MapProduct(Product source)
        {
            return new ProductOutputDto
            {
                Id = source.Id,
                ProductCategoryId = source.ProductCategoryId,
                Name = source.Name,
                Description = source.Description,
                BasePrice = source.BasePrice,
                ImageUrl = source.ImageUrl,
                IsActive = source.IsActive,
                ProductType = source.ProductType,
                CreationTime = source.CreationTime,
                CreatorId = source.CreatorId,
                LastModificationTime = source.LastModificationTime,
                LastModifierId = source.LastModifierId,
                IsDeleted = source.IsDeleted,
                DeleterId = source.DeleterId,
                DeletionTime = source.DeletionTime
            };
        }

        private static ProductVariantOutputDto MapVariant(ProductVariant source)
        {
            return new ProductVariantOutputDto
            {
                Id = source.Id,
                ProductId = source.ProductId,
                Name = source.Name,
                AdditionalPrice = source.AdditionalPrice,
                IsActive = source.IsActive
            };
        }

        private static ProductDetailOutputDto MapProductDetail(Product source)
        {
            return new ProductDetailOutputDto
            {
                Id = source.Id,
                ProductCategoryId = source.ProductCategoryId,
                Name = source.Name,
                Description = source.Description,
                BasePrice = source.BasePrice,
                ImageUrl = source.ImageUrl,
                IsActive = source.IsActive,
                ProductType = source.ProductType,
                CreationTime = source.CreationTime,
                CreatorId = source.CreatorId,
                LastModificationTime = source.LastModificationTime,
                LastModifierId = source.LastModifierId,
                IsDeleted = source.IsDeleted,
                DeleterId = source.DeleterId,
                DeletionTime = source.DeletionTime,
                ProductVariants = source.ProductVariants?.Select(MapVariant).ToList() ?? new List<ProductVariantOutputDto>()
            };
        }
    }
}
