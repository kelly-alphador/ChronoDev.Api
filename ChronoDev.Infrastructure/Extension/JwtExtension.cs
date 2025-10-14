using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ChronoDev.Infrastructure.Extension
{
    public static class JwtExtension
    {
        //Simaintsy mampiasa configuration io tsika afahana manana accees @ appsetting.json
        public static IServiceCollection AddJwtAuthentification(this IServiceCollection services,IConfiguration configuration) 
        {
            //maka any ilay key @ jwt ao anatiny appsetting.json
            var key = configuration["Jwt:key"];
            //hafahantsika micongigurer hoe schema jwt no ampiasaina
            services.AddAuthentication(options =>
            {
                //schema par defaut jwt
                //rehefa mampiasa Authorize de miverfier hoe manana jwt ve
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //cela veut dire que lorsque le token n'est pas valider et token expirer redirige vers 401
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //qui est l'emeteur
                    ValidateIssuer = true,
                    //qui est le destin
                    ValidateAudience = true,
                    //duree de vie du token
                    ValidateLifetime = true,
                    //verfier le signature
                    ValidateIssuerSigningKey = true,
                    //la cle utilise pour signer et valider le token
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });
            return services;
        }
    }
}
