using AutoMapper;
using BCrypt.Net;
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
    public class UserService : BaseService<User, UserRepository>
    {
        public UserService(UserRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task AddAsync<UserDto>(UserDto dto)
        {
            var entity = _mapper.Map<User>(dto);
            entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(entity.PasswordHash);
            await _repository.AddAsync(entity);
        }

        public override async Task UpdateAsync<UserDto>(UserDto dto)
        {
            var entity = _mapper.Map<User>(dto);
            entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(entity.PasswordHash);
            await _repository.UpdateAsync(entity);
        }
    }
}
