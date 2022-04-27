using Challenge.Ecommerce.Comun;
using Challenge.Ecommerce.Domain.Entity;
using System.Threading.Tasks;

namespace Challenge.Ecommerce.Domain.Interface
{
    public interface IUsuarioDomain :IRepository<Usuario>
    {
        Task<Usuario> Authenticate(string username, string password);
    }
}
