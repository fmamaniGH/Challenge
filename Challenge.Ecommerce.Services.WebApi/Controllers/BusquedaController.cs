using Challenge.Ecommerce.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Challenge.Ecommerce.Services.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BusquedaController : Controller
    {
        private readonly IBusquedaApplication _busquedaApplication;

        /// <summary>
        /// Constructor de Busquedas
        /// </summary>
        /// <param name="busquedaApplication"></param>
        public BusquedaController(IBusquedaApplication busquedaApplication)
        {
            _busquedaApplication = busquedaApplication;
        }

        /// <summary>
        /// Obtiene items
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpGet("{item}")]
        public async Task<IActionResult> Get(string item)
        {
            if (string.IsNullOrEmpty(item))
                return BadRequest();
  
            var response = await _busquedaApplication.GetItems(item);
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
