using ChronoDev.Application.DTO;
using ChronoDev.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChronoDev.Api.Controllers
{
    [ApiController]
    [Route("api/v1/saisies-temps")]
    public class SaisieController : Controller
    {
        private readonly SaisieTempsService _saisieTempsService;
        public SaisieController(SaisieTempsService saisieTempsService)
        {
            _saisieTempsService = saisieTempsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSaisiesTemps()
        {
            var response=await _saisieTempsService.GetAll();
            return Ok(response);
        }
        [HttpGet("total-heures")]
        public async Task<IActionResult> GetTotalHeuresDeveloppeurs()
        {
            var response = await _saisieTempsService.TotalHeureDeveloppeur();
            return Ok(response);
        }
        [HttpGet("developpeur")]
        public async Task<IActionResult> GetSaisiesTempsByDeveloppeur([FromQuery] string username)
        {
            var response = await _saisieTempsService.GetByDeveloppeur(username);

            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSaisieTemps(int id)
        {
            var result = await _saisieTempsService.Delete(id);

            if (result.Success)
                return Ok(result);

            return NotFound(result.Message);
        }
        [HttpPost]
        public async Task<ActionResult> CreateSaisieTemps([FromBody] SaisieAddDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _saisieTempsService.AddSaisieTemps(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
