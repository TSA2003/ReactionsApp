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
    public class StartingLightsGameResultService : BaseService<StartingLightsGameResult, StartingLightsGameResultRepository, StartingLightsGameResultDto>
    {
        public StartingLightsGameResultService(StartingLightsGameResultRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
