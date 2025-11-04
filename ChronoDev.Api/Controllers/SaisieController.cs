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
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var result = await _saisieTempsService.Delete(id);

            if (result.Success)
                return Ok(result);

            return NotFound(result.Message);
        }
    }
}
