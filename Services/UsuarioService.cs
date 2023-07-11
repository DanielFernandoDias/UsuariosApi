using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Controllers;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Model;

namespace UsuariosApi.Services
{
    public class UsuarioService
    {
        private readonly IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;

        public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        public async Task Cadastro(CreateUsuarioDto usuarioDto)
        {
            Usuario user = _mapper.Map<Usuario>(usuarioDto);

            IdentityResult result = await _userManager.CreateAsync(user, usuarioDto.Password);

            if (!result.Succeeded)
            {
                throw new ApplicationException("Falha ao cadastrar usuario");
            }
        }

        public async Task Login(LoginUsuarioDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false);
            if (!result.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado");
            }

        }
    }
}
