using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Model;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : Controller
    {
        private UsuarioService _usuarioService;
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("Cadastro")]
        public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto usuarioDto) 
        {
            await _usuarioService.Cadastro(usuarioDto);
            return Ok("Usuário Cadastrado!");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUsuarioDto loginDto)
        {
           var token = await _usuarioService.Login(loginDto);
           return Ok(token);
        }


    }
}
