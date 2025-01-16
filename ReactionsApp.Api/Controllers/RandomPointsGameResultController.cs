using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactionsApp.Business.Dtos;
using ReactionsApp.Business.Services;

namespace ReactionsApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RandomPointsGameResultController : ControllerBase
    {
        private readonly RandomPointsGameResultService _service;

        public RandomPointsGameResultController(RandomPointsGameResultService service)
        {
            _service = service;
        }

        public async Task<IActionResult> GetAll()
        {
            var results = await _service.GetAllAsync<RandomPointsGameResultDto>();
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] RandomPointsGameResultDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong DTO format");
            }

            await _service.AddAsync<RandomPointsGameResultDto, RandomPointsGameResultDto>(dto);

            return Ok("Game Result Saved");
        }
    }
}
