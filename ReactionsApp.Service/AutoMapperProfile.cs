﻿using AutoMapper;
using ReactionsApp.Business.Dtos;
using ReactionsApp.Business.Dtos.Auth;
using ReactionsApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsApp.Business
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<StartingLightsGameResult, StartingLightsGameResultDto>().ReverseMap();
            CreateMap<RandomPointsGameResult, RandomPointsGameResultDto>().ReverseMap();
        }
    }
}
