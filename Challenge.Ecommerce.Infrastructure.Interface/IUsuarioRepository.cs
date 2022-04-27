using Challenge.Ecommerce.Comun;
using Challenge.Ecommerce.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Ecommerce.Infrastructure.Interface
{
    public interface IUsuarioRepository: IRepository<Usuario>
    {
        Task<Usuario> Authenticate(string username, string password);

    }

}
