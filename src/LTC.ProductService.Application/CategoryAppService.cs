using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTC.ProductService.Dtos.Input;
using LTC.ProductService.Dtos.Output;
using LTC.ProductService.Entities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace LTC.ProductService
{
    public class CategoryAppService : ProductServiceAppService, ICategoryAppService
    {
        private readonly IRepository<ProductCategory, Guid> _repository;

        public CategoryAppService(IRepository<ProductCategory, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResultDto<CategoryOutputDto>> GetAllAsync(PaginationInputDto input)
        {
            var query = await _repository.GetQueryableAsync();

            if (!string.IsNullOrWhiteSpace(input.Keyword))
            {
                query = query.Where(x => x.Name.Contains(input.Keyword));
            }

            var totalCount = await AsyncExecuter.CountAsync(query);
            var items = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<CategoryOutputDto>(
                totalCount,
                ObjectMapper.Map<List<ProductCategory>, List<CategoryOutputDto>>(items)
            );
        }

        public async Task<CategoryOutputDto> GetAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return ObjectMapper.Map<ProductCategory, CategoryOutputDto>(entity);
        }

        public async Task<CategoryOutputDto> CreateAsync(CreateCategoryInputDto input)
        {
            var entity = new ProductCategory 
            {
                Name = input.Name,
                Description = input.Description,
                IsActive = input.IsActive
            };
            
            await _repository.InsertAsync(entity);
            return ObjectMapper.Map<ProductCategory, CategoryOutputDto>(entity);
        }

        public async Task<CategoryOutputDto> UpdateAsync(Guid id, UpdateCategoryInputDto input)
        {
            var entity = await _repository.GetAsync(id);
            
            entity.Name = input.Name;
            entity.Description = input.Description;
            entity.IsActive = input.IsActive;
            
            await _repository.UpdateAsync(entity);
            return ObjectMapper.Map<ProductCategory, CategoryOutputDto>(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
