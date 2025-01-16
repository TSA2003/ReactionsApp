using AutoMapper;
using ReactionsApp.Business.Dtos;
using ReactionsApp.Business.Services.Interfaces;
using ReactionsApp.Data.Entities;
using ReactionsApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsApp.Business.Services
{
    public class RandomPointsGameResultService : BaseService<RandomPointsGameResult, RandomPointsGameResultRepository>
    {
        public RandomPointsGameResultService(RandomPointsGameResultRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<IEnumerable<TDto>> GetAllAsync<TDto>()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(e =>
            {
                var dto = _mapper.Map<TDto>(e);
                (dto as RandomPointsGameResultDto).Username = e.Player.Username;
                (dto as RandomPointsGameResultDto).ResultTime = e.CreatedAt;

                return dto;
            }).OrderByDescending(d => (d as RandomPointsGameResultDto).Score);
        }
    }
}
