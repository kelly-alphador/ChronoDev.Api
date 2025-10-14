using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ChronoDev.Domaine.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ChronoDev.Infrastructure.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Génère un token JWT pour un utilisateur
        /// </summary>
        /// <param name="user">L'utilisateur pour lequel générer le token</param>
        /// <returns>Le token JWT sous forme de string</returns>
        public string GenerateJwtToken(ApplicationUser user)
        {
            // 1. Créer le handler qui va générer le token
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            // 2. Récupérer la clé secrète depuis appsettings.json et la convertir en bytes
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            // 3. Définir les claims (informations) qui seront dans le token
            // Les claims sont des paires clé-valeur qui contiennent des infos sur l'utilisateur
            var claims = new List<Claim>
            {
                // L'ID de l'utilisateur (utile pour identifier l'utilisateur dans les requêtes)
                new Claim("Id", user.Id.ToString()),
                
                // Le "Subject" du token - généralement l'email
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                
                // L'email de l'utilisateur
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                
                // Le JTI (JWT ID) - un identifiant unique pour ce token
                // Utile pour le refresh token ou pour révoquer des tokens spécifiques
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                
                // La date de création du token
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
            };

            // 4. Récupérer la durée d'expiration depuis la configuration
            var expirationHours = _configuration.GetValue<int>("Jwt:ExpirationInHours");

            // 5. Créer le descripteur du token avec toutes ses propriétés
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Les informations de l'utilisateur (claims)
                Subject = new ClaimsIdentity(claims),

                // Date d'expiration : maintenant + 1 heure (selon votre config)
                // Important : utiliser UtcNow pour éviter les problèmes de fuseau horaire
                Expires = DateTime.UtcNow.AddHours(expirationHours),

                // L'émetteur du token (votre application)
                Issuer = _configuration["Jwt:Issuer"],

                // L'audience (qui peut utiliser ce token)
                Audience = _configuration["Jwt:Audience"],

                // Les informations de signature pour sécuriser le token
                // On utilise HMAC SHA512 pour l'algorithme de cryptage
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature
                )
            };

            // 6. Créer le token à partir du descripteur
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            // 7. Convertir le token en string (format JWT standard)
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        /// <summary>
        /// Récupère la date d'expiration du token
        /// </summary>
        public DateTime GetTokenExpiration()
        {
            var expirationHours = _configuration.GetValue<int>("Jwt:ExpirationInHours");
            return DateTime.UtcNow.AddHours(expirationHours);
        }
    }
}
