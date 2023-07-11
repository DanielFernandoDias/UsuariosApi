using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosApi.Model;

namespace UsuariosApi.Services
{
    public class TokenService
    {
        public string GeneratingToken(Usuario usuario)
        {
            // gerando meu payload para o token
            Claim[] claims = new Claim[]
            {
                new Claim("Username", usuario.UserName),
                new Claim("Id", usuario.Id),
                new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString())
            };

            // gerando minha chave
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sdkgv5t90g54uyghoaEOARGMJRE0443JRMFIOg90trgk4trse"));

            // gerando minha credencial
            var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            //gerando meu token
            var token = new JwtSecurityToken
                (
                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: signingCredentials
                );

            // retornando meu token em string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}