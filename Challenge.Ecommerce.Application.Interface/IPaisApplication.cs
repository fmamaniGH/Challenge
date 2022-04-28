using Challenge.Ecommerce.Application.DTO;
using Challenge.Ecommerce.Comun;
using System.Threading.Tasks;

namespace Challenge.Ecommerce.Application.Interface
{
    public interface IPaisApplication 
    {
        Task<Response<PaisDto>> GetPais(string pais);
    }
}
