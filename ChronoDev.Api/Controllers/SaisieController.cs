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
        [HttpGet("total-heures-par-semaine")]
        public async Task<IActionResult> GetTotalHeuresDeveloppeursParSemaine()
        {
            var response = await _saisieTempsService.TotalHeureDeveloppeurParSemaine();
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
        [HttpGet("heures-semaine")]
        public async Task<IActionResult> GetHeuresSemaine(DateTime debut, DateTime fin)
        {
            var dto = await _saisieTempsService.GetHeuresParSemaineAsync(debut, fin);
            return Ok(dto);
        }
        [HttpGet("heures-par-mois")]
        public async Task<IActionResult> GetHeuresParMois(int annee, int mois)
        {
            var result = await _saisieTempsService.GetHeuresParMoisAsync(annee, mois);
            return Ok(result);
        }
        [HttpGet("heures-par-mois-By-User")]
        public async Task<IActionResult> GetHeuresParMoisByUser(int utilisateurId,int annee, int mois)
        {
            var result = await _saisieTempsService.GetHeuresParMoisAsyncByUser(utilisateurId,annee, mois);
            return Ok(result);
        }
        [HttpGet("heures-semaine-by-user")]
        public async Task<IActionResult> GetDashboardUtilisateur(int utilisateurId, DateTime debut, DateTime fin)
        {
            var result = await _saisieTempsService.GetHeuresParSemaineParUtilisateurAsync(utilisateurId, debut, fin);
            return Ok(result);
        }
        [HttpGet("utilisateur/{utilisateurId}")]
        public async Task<IActionResult> GetSaisies(int utilisateurId,[FromQuery] DateTime debut,[FromQuery] DateTime fin)
        {
            var result = await _saisieTempsService.GetSaisiesDeLaSemaineAsync(utilisateurId, debut, fin);
            return Ok(result);
        }
    }
}
