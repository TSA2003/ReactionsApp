using AutoMapper;
using ReactionsApp.Business.Dtos;
using ReactionsApp.Data.Entities;
using ReactionsApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsApp.Business.Services
{
    public class StartingLightsGameResultService : BaseService<StartingLightsGameResult, StartingLightsGameResultRepository>
    {
        public StartingLightsGameResultService(StartingLightsGameResultRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<IEnumerable<TDto>> GetAllAsync<TDto>()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(e =>
            {
                var dto = _mapper.Map<TDto>(e);
                (dto as StartingLightsGameResultDto).Username = e.Player.Username;
                (dto as StartingLightsGameResultDto).ResultTime = e.CreatedAt;

                return dto;
            }).OrderBy(d => (d as StartingLightsGameResultDto).ReactionTime);
        }
    }
}
