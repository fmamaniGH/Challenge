using Challenge.Ecommerce.Infrastructure.Data;
using Challenge.Ecommerce.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Ecommerce.Infrastructure.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private readonly IUsuarioRepository _usuarioRepository;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            _usuarioRepository = new UsuarioRepository(context);


        }       
        public IUsuarioRepository Usuario => _usuarioRepository ?? new UsuarioRepository(_context);

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();

        }
    }
}
