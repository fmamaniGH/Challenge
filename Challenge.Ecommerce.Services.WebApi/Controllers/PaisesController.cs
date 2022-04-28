using Challenge.Ecommerce.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Challenge.Ecommerce.Services.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaisesController : Controller
    {
        private readonly IPaisApplication _paisApplication;

        /// <summary>
        /// Constructor de Paises
        /// </summary>
        /// <param name="paisApplication"></param>
        public PaisesController(IPaisApplication paisApplication)
        {
            _paisApplication = paisApplication;
        }

        /// <summary>
        /// Obtiene el pais
        /// </summary>
        /// <param name="pais"></param>
        /// <returns></returns>
        [HttpGet("{pais}")]
        public async Task<IActionResult> Get(string pais)
        {       
            if (string.IsNullOrEmpty(pais))
                return BadRequest();

            if (pais == "BR" || pais == "CO")
            {
                return StatusCode(401, "Error 401 unauthorized");
            }
           
            var response = await _paisApplication.GetPais(pais);
            if (response.IsSuccess)
            {
                if (response.Data != null)
                {                    
                    return Ok(response);
                }
                else
                    return NotFound(response.Message);
            }
            return NotFound(response.Message);

        }
    }
}
