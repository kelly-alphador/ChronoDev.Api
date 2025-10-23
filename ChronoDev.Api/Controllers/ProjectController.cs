using ChronoDev.Application.DTO;
using ChronoDev.Application.Services;
using ChronoDev.Domaine.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChronoDev.Api.Controllers
{
    [ApiController]
    [Route("Projet/[controller]")]
    public class ProjectController : Controller
    {
        private readonly ProjetService _projetService;
        public ProjectController(ProjetService projetService)
        {
            _projetService = projetService;
        }

        [HttpGet]
        public async  Task<IActionResult> GetAllProject()
        {
            var response = await _projetService.GetAllProject(); 
            return Ok(response);
        }
        [HttpGet("Search")]
        public async Task<ActionResult> GetByName([FromQuery] string name)
        {
            var response = await _projetService.GetProjectByName(name);

            // Retourner le code HTTP approprié selon le succès
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult> AddProject([FromBody] ProjectAddDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _projetService.AddProject(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var result = await _projetService.Remove(id);

            if (result.Success)
                return Ok(result);

            return NotFound(result.Message); 
        }
        [HttpGet("count")]
        public async Task<ActionResult<ApiResponse>> GetTotalCount()
        {
            var result = await _projetService.GetTotalProject();

            if (result.Success)
                return Ok(result);

            return StatusCode(500, result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] ProjectUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("L'ID de la route ne correspond pas à l'ID du projet");
            }

            var result = await _projetService.UpdateProject(dto);

            if (result.Success)
                return Ok(result);

            return NotFound(result); 
        }
    }
}
