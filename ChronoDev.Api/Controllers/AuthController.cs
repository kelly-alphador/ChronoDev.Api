using ChronoDev.Application.DTO;
using ChronoDev.Domaine.Entities;
using ChronoDev.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChronoDev.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        //on utilise la configuration ici pour avoir la config dans appsetting
        private readonly IConfiguration _configuration; 
        //on utilise l'UserManager pour la gestion de l'user
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly JwtService _jwtService;
        //Injection de la configuration pour avoir key dans appsettings.json
        public AuthController(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            JwtService jwtService,
            RoleManager<IdentityRole<int>> roleManager
        )
        {
            _jwtService = jwtService;
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        /// <summary>
        /// Endpoint pour la connexion d'un utilisateur existant
        /// POST: api/auth/login
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            //  Vérifier si le modèle est valide
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });
            }

            // Rechercher l'utilisateur par email
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                return Unauthorized(new AuthResponseDto
                {
                    Success = false,
                    Errors = new List<string> { "Email ou mot de passe incorrect" }
                });
            }

            // Vérifier le mot de passe
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!isPasswordCorrect)
            {
                return Unauthorized(new AuthResponseDto
                {
                    Success = false,
                    Errors = new List<string> { "Email ou mot de passe incorrect" }
                });
            }

            // Récupérer les rôles de l'utilisateur
            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault(); // On prend le premier rôle

            //  Générer le token JWT
            var token = _jwtService.GenerateJwtToken(user);
            var expiresAt = _jwtService.GetTokenExpiration();

            //  Retourner la réponse avec toutes les informations
            return Ok(new AuthResponseDto
            {
                Success = true,
                Token = token,
                ExpiresAt = expiresAt,
                Email = user.Email,
                Nom = user.nom,
                Prenom = user.prenom,
                Role = userRole
            });
        }
        // <summary>
        /// Endpoint pour l'inscription d'un nouvel utilisateur avec Nom, Prénom et Rôle
        /// POST: api/auth/register
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            //verifie si l'user a entree une donnees valide
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Errors = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage).ToList()
                });
            }
            //verifie si l'email est existe
            var EmailExist=await _userManager.FindByEmailAsync(registerDTO.Email);
            if(EmailExist!=null)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Errors = new List<string> { "un utilisateur avec email existe deja" }
                });
            }
            //Verifie si le role existe
            var RoleExist=await _roleManager.RoleExistsAsync(registerDTO.Role);
            if(!RoleExist)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Errors = new List<string> { $"le role {registerDTO.Role} n'existe pas" }
                });
            }
            //Creer une nouvelUser
            var newUser = new ApplicationUser
            {
                nom = registerDTO.Nom,
                prenom = registerDTO.prenom,
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
            };
            //Creation de l'user avec identity
            // Identity va automatiquement hasher le mot de passe
            var result = await _userManager.CreateAsync(newUser, registerDTO.Password);

            //  Vérifier si la création a réussi
            if (!result.Succeeded)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                });
            }

            //  Assigner le rôle à l'utilisateur
            var roleResult = await _userManager.AddToRoleAsync(newUser, registerDTO.Role);

            if (!roleResult.Succeeded)
            {
                // Si l'assignation du rôle échoue, supprimer l'utilisateur créé
                await _userManager.DeleteAsync(newUser);

                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Errors = new List<string> { "Erreur lors de l'assignation du rôle" }
                });
            }

            //  Générer le token JWT pour le nouvel utilisateur
            var token = _jwtService.GenerateJwtToken(newUser);
            var expiresAt = _jwtService.GetTokenExpiration();

            // 9. Retourner la réponse avec toutes les informations
            return Ok(new AuthResponseDto
            {
                Success = true,
                Token = token,
                ExpiresAt = expiresAt,
                Email = newUser.Email,
                Nom = newUser.nom,
                Prenom = newUser.prenom,
                Role = registerDTO.Role
            });


        }

      
    }
}
