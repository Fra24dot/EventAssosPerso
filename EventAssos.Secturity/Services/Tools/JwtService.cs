using EventAssos.Core.Interfaces.Services.Tools;
using EventAssos.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventAssos.Security.Services.Tools
{
    public class JwtService(IConfiguration configuration) : IJwtService
    {
        public string GenerateToken(User user)
        {

            //On récupère la config JWT depuis appsettings.json ou User Secrets.
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"]!;

            //On calcule la date d'expiration du token (heure actuelle + X minutes).
            var expiration = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpirationMinutes"] ?? "30"));



            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("role", user.Role.ToString() ?? "User")
        };
            //On crée une clé cryptographique avec ta clé secrète,
            //puis on définit l'algorithme de signature (HMAC SHA256).
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Assemblage du token
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            //On convertit le token en string JWT qu'on retourne au client 
            return new JwtSecurityTokenHandler().WriteToken(token); 

        }
    }
}
