using ChronoDev.Application.DTO;
using ChronoDev.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ChronoDev.Api.Controllers
{
    [ApiController]
    [Route("api/V1/[controller]")]
    public class TacheController : Controller
    {
        private readonly TacheService _tacheService;
        public TacheController(TacheService tacheService)
        {
            _tacheService = tacheService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTaches()
        {
            var response=await _tacheService.GetAllTache();
            if (response.Success) 
            {
                return Ok(response);
            }
            else { return BadRequest(response); }
        }
        [HttpGet("projet/{projetId}")]
        public async Task<IActionResult> GetTachesByProjet([FromQuery] int projectId)
        {
            var response=await _tacheService.GetAllTacheByProject(projectId);
            if (response.Success)
            {
                return Ok(response);
            }
            else { return BadRequest(response); }
        }
        [HttpPost]
        public async Task<IActionResult> CreateTache([FromBody] TacheAddDto tacheAddDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tacheService.AddTache(tacheAddDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTache(int id)
        {
            var result = await _tacheService.Delete(id);

            if (result.Success)
                return Ok(result);

            return NotFound(result.Message);
        }
    }
}
