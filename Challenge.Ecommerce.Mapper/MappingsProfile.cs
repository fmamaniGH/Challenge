using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Challenge.Ecommerce.Application.DTO;
using Challenge.Ecommerce.Domain.Entity;

namespace Challenge.Ecommerce.Mapper
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
        }
    }
}