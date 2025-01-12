using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReactionsApp.Business.Dtos;
using ReactionsApp.Business.Dtos.Auth;
using ReactionsApp.Business.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReactionsApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserService _service;
        private readonly IConfiguration _configuration;
        
        public AuthController(UserService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Username and password are required");

            var userDto = await _service.FindByCredentialsAsync(registerDto.Username, registerDto.Password);

            if (userDto is not null)
                return BadRequest("User exists");

            userDto = await _service.AddAsync<UserDto>(new UserDto() 
            { 
                Username = registerDto.Username,
                Password = registerDto.Password
            });

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userDto.Username),
                new Claim(ClaimTypes.NameIdentifier, userDto.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),  // Token expiration
                signingCredentials: creds
            );

            return Ok(new
            {
                user = userDto,
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Username and password are required");

            var userDto = await _service.FindByCredentialsAsync(loginDto.Username, loginDto.Password);

            if (userDto is null)
                return Unauthorized("Invalid username or password");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userDto.Username),
                new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),  // Token expiration
                signingCredentials: creds
            );

            return Ok(new
            {
                user = userDto,
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
