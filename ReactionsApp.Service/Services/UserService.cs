using AutoMapper;
using BCrypt.Net;
using ReactionsApp.Business.Dtos;
using ReactionsApp.Business.Dtos.Auth;
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

        public async Task<UserDto> FindByCredentialsAsync(string username, string password)
        {
            var result = await _repository.FilterAsync(u => u.Username == username);
            var entity = result.FirstOrDefault();

            if (entity is null || !BCrypt.Net.BCrypt.Verify(password, entity.PasswordHash))
            {
                return null;
            }

            return _mapper.Map<UserDto>(entity);
        }

        public override async Task<TOutDto> AddAsync<TInDto, TOutDto>(TInDto inDto)
        {
            var entity = _mapper.Map<User>(inDto);
            entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword((inDto as RegisterDto).Password);
            entity = await _repository.AddAsync(entity);

            return _mapper.Map<TOutDto>(entity);
        }

        public override async Task<TOutDto> UpdateAsync<TInDto, TOutDto>(TInDto inDto)
        {
            var entity = _mapper.Map<User>(inDto);
            entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword((inDto as RegisterDto).Password);
            entity = await _repository.UpdateAsync(entity);

            return _mapper.Map<TOutDto>(entity);
        }
    }
}
