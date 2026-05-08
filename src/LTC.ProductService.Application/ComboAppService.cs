using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
    public class ComboAppService : ProductServiceAppService, IComboAppService
    {
        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

        private readonly IRepository<Combo, Guid> _repository;
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IProductMediaUploader _mediaUploader;

        public ComboAppService(
            IRepository<Combo, Guid> repository,
            IRepository<Product, Guid> productRepository,
            IProductMediaUploader mediaUploader)
        {
            _repository = repository;
            _productRepository = productRepository;
            _mediaUploader = mediaUploader;
        }

        public async Task<PagedResultDto<ComboOutputDto>> GetComboListAsync(PaginationInputDto input)
        {
            var query = await _repository.GetQueryableAsync();

            if (!string.IsNullOrWhiteSpace(input.Keyword))
            {
                query = query.Where(x => x.Name.Contains(input.Keyword));
            }

            var totalCount = await AsyncExecuter.CountAsync(query);
            var items = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            var productMap = await GetProductMapAsync(items);
            return new PagedResultDto<ComboOutputDto>(
                totalCount,
                items.Select(item => MapCombo(item, productMap)).ToList()
            );
        }

        public async Task<ComboOutputDto> GetComboAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            var productMap = await GetProductMapAsync(new[] { entity });
            return MapCombo(entity, productMap);
        }

        public async Task<ComboOutputDto> CreateComboAsync(CreateComboInputDto input)
        {
            await ValidateProductLinesAsync(input.Products);
            var imageUrl = await ResolveImageUrlAsync(input.ImageFile, input.ImageUrl);

            var entity = new Combo
            {
                Name = input.Name,
                Description = input.Description,
                TotalPrice = input.TotalPrice,
                IsActive = input.IsActive,
                ImageUrl = imageUrl,
                ProductIds = SerializeLines(input.Products),
                CreatedAt = DateTime.UtcNow
            };

            await _repository.InsertAsync(entity);
            var productMap = await GetProductMapAsync(new[] { entity });
            return MapCombo(entity, productMap);
        }

        public async Task<ComboOutputDto> UpdateComboAsync(Guid id, UpdateComboInputDto input)
        {
            await ValidateProductLinesAsync(input.Products);
            var entity = await _repository.GetAsync(id);

            var imageUrl = await ResolveImageUrlAsync(input.ImageFile, input.ImageUrl ?? entity.ImageUrl);

            entity.Name = input.Name;
            entity.Description = input.Description;
            entity.TotalPrice = input.TotalPrice;
            entity.IsActive = input.IsActive;
            entity.ImageUrl = imageUrl;
            entity.ProductIds = SerializeLines(input.Products);
            entity.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(entity);
            var productMap = await GetProductMapAsync(new[] { entity });
            return MapCombo(entity, productMap);
        }

        public async Task DeleteComboAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        private async Task ValidateProductLinesAsync(List<ComboProductLineDto>? lines)
        {
            if (lines == null || lines.Count == 0)
            {
                return;
            }

            var distinctIds = lines.Select(x => x.ProductId).Distinct().ToList();
            var query = await _productRepository.GetQueryableAsync();
            var existingIds = await AsyncExecuter.ToListAsync(
                query.Where(x => distinctIds.Contains(x.Id)).Select(x => x.Id)
            );

            var missing = distinctIds.Except(existingIds).ToList();
            if (missing.Count > 0)
            {
                throw new UserFriendlyException($"Referenced product(s) not found: {string.Join(",", missing)}");
            }
        }

        private async Task<Dictionary<Guid, Product>> GetProductMapAsync(IEnumerable<Combo> combos)
        {
            var productIds = combos
                .SelectMany(x => DeserializeLines(x.ProductIds))
                .Select(x => x.ProductId)
                .Distinct()
                .ToList();

            if (productIds.Count == 0)
            {
                return new Dictionary<Guid, Product>();
            }

            var query = await _productRepository.GetQueryableAsync();
            var products = await AsyncExecuter.ToListAsync(query.Where(x => productIds.Contains(x.Id)));
            return products.ToDictionary(x => x.Id);
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

        private static string? SerializeLines(List<ComboProductLineDto>? lines)
        {
            if (lines == null || lines.Count == 0)
            {
                return null;
            }

            var payload = lines
                .Where(x => x.Quantity > 0)
                .Select(x => new { productId = x.ProductId, quantity = x.Quantity })
                .ToList();

            return JsonSerializer.Serialize(payload, JsonOptions);
        }

        private static List<ComboProductLineDto> DeserializeLines(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<ComboProductLineDto>();
            }

            try
            {
                var raw = JsonSerializer.Deserialize<List<JsonComboLine>>(json, JsonOptions);
                if (raw == null)
                {
                    return new List<ComboProductLineDto>();
                }

                return raw
                    .Where(x => x.ProductId != Guid.Empty)
                    .Select(x => new ComboProductLineDto { ProductId = x.ProductId, Quantity = Math.Max(1, x.Quantity) })
                    .ToList();
            }
            catch (JsonException)
            {
                return new List<ComboProductLineDto>();
            }
        }

        private static ComboOutputDto MapCombo(Combo source, IReadOnlyDictionary<Guid, Product> productMap)
        {
            var lines = DeserializeLines(source.ProductIds)
                .Select(line =>
                {
                    productMap.TryGetValue(line.ProductId, out var product);
                    return new ComboProductLineOutputDto
                    {
                        ProductId = line.ProductId,
                        Quantity = line.Quantity,
                        Product = product == null ? null : new ProductOutputDto
                        {
                            Id = product.Id,
                            TenantId = product.TenantId,
                            ProductCategoryId = product.ProductCategoryId,
                            Name = product.Name,
                            Description = product.Description,
                            BasePrice = product.BasePrice,
                            ImageUrl = product.ImageUrl,
                            IsActive = product.IsActive,
                            CreatedAt = product.CreatedAt,
                            UpdatedAt = product.UpdatedAt
                        }
                    };
                })
                .ToList();

            return new ComboOutputDto
            {
                Id = source.Id,
                TenantId = source.TenantId,
                Name = source.Name,
                Description = source.Description,
                ImageUrl = source.ImageUrl,
                TotalPrice = source.TotalPrice,
                IsActive = source.IsActive,
                CreatedAt = source.CreatedAt,
                UpdatedAt = source.UpdatedAt,
                CreationTime = source.CreationTime,
                CreatorId = source.CreatorId,
                LastModificationTime = source.LastModificationTime,
                LastModifierId = source.LastModifierId,
                IsDeleted = source.IsDeleted,
                DeleterId = source.DeleterId,
                DeletionTime = source.DeletionTime,
                Products = lines
            };
        }

        private sealed class JsonComboLine
        {
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
