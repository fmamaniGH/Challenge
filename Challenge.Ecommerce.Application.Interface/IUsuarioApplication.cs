using Challenge.Ecommerce.Application.DTO;
using Challenge.Ecommerce.Comun;
using Challenge.Ecommerce.Domain.Entity;
using System.Threading.Tasks;

namespace Challenge.Ecommerce.Application.Interface
{
    public interface IUsuarioApplication: IRepository<Usuario>
    {
        Task<Response<UsuarioDto>> Authenticate(string username, string password);
    }
}
