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

        public virtual async Task<TDto> GetByIdAsync<TDto>(Guid id) where TDto : BaseDto
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync<TDto>() where TDto : BaseDto
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<IEnumerable<TDto>> FindAsync<TDto>(Expression<Func<TDto, bool>> predicate) where TDto : BaseDto
        {
            var expression = _mapper.Map<Expression<Func<TEntity, bool>>>(predicate); 
            var entities = await _repository.FindAsync(expression);
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task AddAsync<TDto>(TDto dto) where TDto : BaseDto
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
        }

        public virtual async Task UpdateAsync<TDto>(TDto dto) where TDto : BaseDto
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
                await _repository.DeleteAsync(entity);
        }
    }
}
