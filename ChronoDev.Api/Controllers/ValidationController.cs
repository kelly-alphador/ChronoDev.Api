using ChronoDev.Application.DTO;
using ChronoDev.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChronoDev.Api.Controllers
{
    [Route("api/V1/[controller]")]
    public class ValidationController : Controller
    {
        private readonly ValidationService _validationService;
        public ValidationController(ValidationService validationService) 
        {
            _validationService = validationService;
        }
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] ValadiationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _validationService.Add(dto);
            return result.Success ? Ok(result) : BadRequest(result);

        }
    }
}
