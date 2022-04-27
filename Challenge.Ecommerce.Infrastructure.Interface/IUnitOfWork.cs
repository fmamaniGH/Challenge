using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Ecommerce.Infrastructure.Interface
{
    public interface IUnitOfWork
    {
        IUsuarioRepository Usuario { get; }
        Task SaveChanges();
    }
}
