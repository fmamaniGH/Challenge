using Challenge.Ecommerce.Domain.Entity;
using Challenge.Ecommerce.Infrastructure.Data;
using Challenge.Ecommerce.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Challenge.Ecommerce.Infrastructure.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationContext context) : base(context)
        {

        }

        public async Task<Usuario> Authenticate(string username, string password)
        {
            return await (from usuario in _context.Set<Usuario>().AsNoTracking()
                          where usuario.UserName == username && usuario.Password == password
                          select usuario).FirstOrDefaultAsync();

        }


    }
}