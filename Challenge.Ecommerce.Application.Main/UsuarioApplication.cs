using AutoMapper;
using Challenge.Ecommerce.Application.DTO;
using Challenge.Ecommerce.Application.Interface;
using Challenge.Ecommerce.Comun;
using Challenge.Ecommerce.Domain.Entity;
using Challenge.Ecommerce.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Challenge.Ecommerce.Application.Main
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly IUsuarioDomain _usuarioDomain;
        private readonly IMapper _mapper;

        public UsuarioApplication(IUsuarioDomain usuarioDomain, IMapper mapper)
        {
            _usuarioDomain = usuarioDomain;
            _mapper = mapper;
        }

        public async Task<Response<UsuarioDto>> Authenticate(string userName, string password)
        {
            var response = new Response<UsuarioDto>();
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                response.IsSuccess = false;
                response.Message = "Errores de validación";
                return response;
            }

            try
            {               
                var usuario = await _usuarioDomain.Authenticate(userName,password);
                response.Data = _mapper.Map<UsuarioDto>(usuario);

                if (response.Data != null)
                {
                    if (usuario.Password == password)
                    {
                        response.IsSuccess = true;
                        response.Message = "Exito";
                        return response;
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Usuario no existe";
                    return response;
                }
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = false;
                response.Message = "Usuario no existe";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

     
        public Task<int> Count(Expression<Func<Usuario, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public void DeAttach(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(object id)
        {
            return _usuarioDomain.Delete(id);
        }

        public void DeleteEntity(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> Find(Expression<Func<Usuario, bool>> wherecondition = null, Func<IQueryable<Usuario>, IOrderedQueryable<Usuario>> orderby = null, string includeProps = null, int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task Insert(Usuario usuario)
        {
            return _usuarioDomain.Insert(usuario);
        }

        public void Update(Usuario usuario)
        {
            _usuarioDomain.Update(usuario);
        }
    }
}
