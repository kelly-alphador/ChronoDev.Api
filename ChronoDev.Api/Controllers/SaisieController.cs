using ChronoDev.Application.DTO;
using ChronoDev.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChronoDev.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class SaisieController : Controller
    {
        private readonly SaisieTempsService _saisieTempsService;
        public SaisieController(SaisieTempsService saisieTempsService)
        {
            _saisieTempsService = saisieTempsService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response=await _saisieTempsService.GetAll();
            return Ok(response);
        }
        [HttpGet("TotalHeure")]
        public async Task<IActionResult> TotalHeurDeveloppeur()
        {
            var response = await _saisieTempsService.TotalHeureDeveloppeur();
            return Ok(response);
        }
        [HttpGet("getByDeveloppeur")]
        public async Task<IActionResult> GetSaisiTempsByDeveloppeur([FromQuery] string username)
        {
            var response = await _saisieTempsService.GetByDeveloppeur(username);

            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var result = await _saisieTempsService.Delete(id);

            if (result.Success)
                return Ok(result);

            return NotFound(result.Message);
        }
        [HttpPost]
        public async Task<ActionResult> AddSaisie([FromBody] SaisieAddDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _saisieTempsService.AddSaisieTemps(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
