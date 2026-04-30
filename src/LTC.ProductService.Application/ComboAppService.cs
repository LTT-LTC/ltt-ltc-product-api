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
    public class ComboAppService : ProductServiceAppService, IComboAppService
    {
        private readonly IRepository<Combo, Guid> _repository;
        private readonly IRepository<ComboItem, Guid> _itemRepository;

        public ComboAppService(
            IRepository<Combo, Guid> repository,
            IRepository<ComboItem, Guid> itemRepository)
        {
            _repository = repository;
            _itemRepository = itemRepository;
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

            return new PagedResultDto<ComboOutputDto>(
                totalCount,
                items.Select(item => ObjectMapper.Map<Combo, ComboOutputDto>(item)).ToList()
            );
        }

        public async Task<ComboDetailOutputDto> GetComboAsync(Guid id)
        {
            var query = await _repository.WithDetailsAsync(x => x.ComboItems);
            var entity = await AsyncExecuter.FirstOrDefaultAsync(query.Where(x => x.Id == id));
            
            if (entity == null)
            {
                throw new Volo.Abp.UserFriendlyException("Combo not found");
            }
            
            return ObjectMapper.Map<Combo, ComboDetailOutputDto>(entity);
        }

        public async Task<ComboOutputDto> CreateComboAsync(CreateComboInputDto input)
        {
            var entity = new Combo
            {
                Name = input.Name,
                Description = input.Description,
                TotalPrice = input.TotalPrice,
                IsActive = input.IsActive,
                ComboItems = input.ComboItems.Select(x => new ComboItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                }).ToList()
            };

            await _repository.InsertAsync(entity);
            return ObjectMapper.Map<Combo, ComboOutputDto>(entity);
        }

        public async Task<ComboOutputDto> UpdateComboAsync(Guid id, UpdateComboInputDto input)
        {
            var entity = await _repository.GetAsync(id);

            entity.Name = input.Name;
            entity.Description = input.Description;
            entity.TotalPrice = input.TotalPrice;
            entity.IsActive = input.IsActive;

            await _repository.UpdateAsync(entity);
            return ObjectMapper.Map<Combo, ComboOutputDto>(entity);
        }

        public async Task DeleteComboAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<ComboItemOutputDto> AddItemAsync(Guid id, CreateComboItemInputDto input)
        {
            var item = new ComboItem
            {
                ComboId = id,
                ProductId = input.ProductId,
                Quantity = input.Quantity
            };

            await _itemRepository.InsertAsync(item);
            return ObjectMapper.Map<ComboItem, ComboItemOutputDto>(item);
        }

        public async Task DeleteItemAsync(Guid id, Guid comboItemId)
        {
            var item = await _itemRepository.GetAsync(comboItemId);
            if (item.ComboId == id)
            {
                await _itemRepository.DeleteAsync(comboItemId);
            }
        }
    }
}
