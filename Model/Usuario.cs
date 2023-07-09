using Microsoft.AspNetCore.Identity;

namespace UsuariosApi.Model
{
    public class Usuario : IdentityUser
    {
        public DateTime DataNascimento { get; set; }
        public Usuario() : base()
        {}

    }
}
