using AutoMapper;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Model;

namespace UsuariosApi.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
        }
    }
}
