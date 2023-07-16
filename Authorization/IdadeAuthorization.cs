using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UsuariosApi.Authorization
{
    public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
        {
            var dataNascimentoClaim = context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth);
            if (dataNascimentoClaim is null)
            {
                // nega o acesso
                return Task.CompletedTask;
            }

            // Converte a string da claim para DateTime
            DateTime dataNascimento = Convert.ToDateTime(dataNascimentoClaim.Value);
            
            // Calcula a idade
            int idade = DateTime.Today.Year - dataNascimento.Year;
            if(dataNascimento > DateTime.Today.AddYears(-idade)) idade--;

            if(idade >= requirement.Idade)
                context.Succeed(requirement);
            
            return Task.CompletedTask;
        }
    }
}
