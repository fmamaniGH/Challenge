using AutoMapper;
using Challenge.Ecommerce.Application.DTO;
using Challenge.Ecommerce.Domain.Entity;

namespace Challenge.Ecommerce.Mapper
{
    public static class UsuarioMapper
    {
        static UsuarioMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<UsuarioMappingsProfile>()).CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static UsuarioDto ToModel(this Usuario usuario)
        {
            return Mapper.Map<UsuarioDto>(usuario);
        }

        public static Usuario ToEntity(this UsuarioDto usuarioDto)
        {
            return Mapper.Map<Usuario>(usuarioDto);
        }

    }
}
