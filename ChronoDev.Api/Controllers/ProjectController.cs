using ChronoDev.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChronoDev.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
    }
}
