using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Model;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : Controller
    {
        private readonly IMapper _mapper;
        private UserManager<Usuario> _userManager;
        public UsuarioController(IMapper mapper, UserManager<Usuario> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto usuarioDto) 
        {
            Usuario user = _mapper.Map<Usuario>(usuarioDto);

            IdentityResult result = await _userManager.CreateAsync(user, usuarioDto.Password);

            if(result.Succeeded) return Ok("Usuário Cadastrado!");

            throw new ApplicationException("Falha ao cadastrar usuario");
        }


    }
}
