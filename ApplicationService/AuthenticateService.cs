using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService
{
    public class AuthenticateService
    {
        private AlunoRepository Repository { get; set; }

        private IConfiguration Configuration { get; set; }

        public AuthenticateService(AlunoRepository repository, IConfiguration configuration)
        {
            this.Repository = repository;
            this.Configuration = configuration;
        }

        public string AuthenticateUser(string email, string password)
        {
            var aluno = this.Repository.GetAlunoByEmail(email);

            if (aluno == null)
                return null;

            if (password != "123456")
                return null;

            return CreateToken(aluno);
        }

        private string CreateToken(Aluno aluno)
        {
            var key = Encoding.UTF8.GetBytes(this.Configuration["Token:Secret"]);

            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, aluno.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, aluno.Nome));
            claims.Add(new Claim(ClaimTypes.Email, aluno.Email));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Audience = "ALUNO-API",
                Issuer = "ALUNO-API"
            };

            var securityToken = tokenHandler.CreateToken(tokenDescription);

            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
