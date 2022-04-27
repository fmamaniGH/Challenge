using Challenge.Ecommerce.Domain.Entity;
using Challenge.Ecommerce.Domain.Interface;
using Challenge.Ecommerce.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Ecommerce.Domain.Core
{
    public class UsuarioDomain : IUsuarioDomain
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsuarioDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Usuario> Authenticate(string username, string password)
        {
            return _unitOfWork.Usuario.Authenticate(username, password);
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
            throw new NotImplementedException();
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
            return _unitOfWork.Usuario.GetById(id);
        }

        public Task<IEnumerable<Usuario>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task Insert(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Usuario entity)
        {
            throw new NotImplementedException();
        }
    }
}
