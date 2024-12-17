using ReactionsApp.Business.Dtos;
using ReactionsApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsApp.Business.Services.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        Task<TDto> GetByIdAsync<TDto>(Guid id) where TDto : BaseDto;
        Task<IEnumerable<TDto>> GetAllAsync<TDto>() where TDto : BaseDto;
        Task<IEnumerable<TDto>> FindAsync<TDto>(Expression<Func<TDto, bool>> predicate) where TDto : BaseDto;
        Task AddAsync<TDto>(TDto dto) where TDto : BaseDto;
        Task UpdateAsync<TDto>(TDto dto) where TDto : BaseDto;
        Task DeleteAsync(Guid id);
    }
}
