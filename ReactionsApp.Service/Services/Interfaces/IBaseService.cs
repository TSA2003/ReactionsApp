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
        Task<TDto> GetByIdAsync<TDto>(Guid id);
        Task<IEnumerable<TDto>> GetAllAsync<TDto>();
        Task<TDto> AddAsync<TDto>(TDto dto);
        Task<TDto> UpdateAsync<TDto>(TDto dto);
        Task DeleteAsync(Guid id);
    }
}
