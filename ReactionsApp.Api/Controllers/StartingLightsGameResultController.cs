using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactionsApp.Business.Dtos;
using ReactionsApp.Business.Dtos.Auth;
using ReactionsApp.Business.Services;
using System.Security.Claims;

namespace ReactionsApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StartingLightsGameResultController : ControllerBase
    {
        private readonly StartingLightsGameResultService _service;

        public StartingLightsGameResultController(StartingLightsGameResultService service)
        {
            _service = service;
        }

        public async Task<IActionResult> GetAll()
        {
            var results = _service.GetAllAsync<StartingLightsGameResultDto>();
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] StartingLightsGameResultDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong DTO format");
            }

            dto.PlayerId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _service.AddAsync(dto);

            return Ok("Game Result Saved");
        }

    }
}
