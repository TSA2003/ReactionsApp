using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReactionsApp.Business.Dtos.Auth;
using ReactionsApp.Business.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReactionsApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _service;
        private readonly IConfiguration _configuration;
        
        public AuthController(UserService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Username and password are required");
            }

            //_service.FindAsync(u => u.Username == login.Username && u.Password == login.Password);

            //if (model.Username == "admin" && model.Password == "password")
            //{
            //    var claims = new[]
            //    {
            //        new Claim(ClaimTypes.Name, model.Username),
            //        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            //    };

            //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //    var token = new JwtSecurityToken(
            //        issuer: _configuration["Jwt:Issuer"],
            //        audience: _configuration["Jwt:Audience"],
            //        claims: claims,
            //        expires: DateTime.Now.AddMinutes(30),  // Token expiration
            //        signingCredentials: creds
            //    );

            //    return Ok(new
            //    {
            //        token = new JwtSecurityTokenHandler().WriteToken(token)
            //    });
            //}

            return Unauthorized("Invalid username or password");
        }
    }
}
