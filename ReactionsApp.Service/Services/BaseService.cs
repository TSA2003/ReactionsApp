using AutoMapper;
using ReactionsApp.Business.Dtos;
using ReactionsApp.Business.Services.Interfaces;
using ReactionsApp.Data.Entities;
using ReactionsApp.Data.Repositories;
using ReactionsApp.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsApp.Business.Services
{
    public abstract class BaseService<TEntity, TRepository> : IBaseService<TEntity> where TEntity : BaseEntity where TRepository : BaseRepository<TEntity>
    {
        protected readonly TRepository _repository;
        protected readonly IMapper _mapper;

        public BaseService(TRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public virtual async Task<TDto> GetByIdAsync<TDto>(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync<TDto>()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<TOutDto> AddAsync<TInDto, TOutDto>(TInDto inDto)
        {
            var entity = _mapper.Map<TEntity>(inDto);
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;

            entity = await _repository.AddAsync(entity);

            return _mapper.Map<TOutDto>(entity);
        }

        public virtual async Task<TOutDto> UpdateAsync<TInDto, TOutDto>(TInDto inDto)
        {
            var entity = _mapper.Map<TEntity>(inDto);
            entity.UpdatedAt = DateTime.Now;

            entity = await _repository.UpdateAsync(entity);

            return _mapper.Map<TOutDto>(entity);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
                await _repository.DeleteAsync(entity);
        }
    }
}
