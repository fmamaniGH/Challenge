using Challenge.Ecommerce.Application.DTO;
using Challenge.Ecommerce.Application.Interface;
using Challenge.Ecommerce.Comun;
using Challenge.Ecommerce.Mapper;
using Challenge.Ecommerce.Services.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Ecommerce.Services.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
   
    public class UsuarioController : Controller
    {
        private readonly IUsuarioApplication _usuarioApplication;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="usuarioApplication"></param>
        /// <param name="appSettings"></param>
        public UsuarioController(IUsuarioApplication usuarioApplication, IOptions<AppSettings> appSettings)
        {
            _usuarioApplication = usuarioApplication;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UsuarioDto usuarioDto)
        {
            var response = await _usuarioApplication.Authenticate(usuarioDto.UserName, usuarioDto.Password);

            if (response.IsSuccess)
            {
                if (response.Data != null)
                {
                    response.Data.Token = BuildToken(response);
                    return Ok(response);
                }
                else
                    return NotFound(response.Message);
            }
            return BadRequest(response.Message);
        }

        /// <summary>
        /// Inserta Usuario
        /// </summary>
        /// <param name="usuarioDto"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                return BadRequest();
            }

            var usuario = usuarioDto.ToEntity();
            var response = _usuarioApplication.Insert(usuario);

            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest();

        }

        /// <summary>
        /// Actualiza Usuario
        /// </summary>
        /// <param name="usuarioDto"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public IActionResult Update([FromBody] UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                return BadRequest();
            }

            var usuario = usuarioDto.ToEntity();
            _usuarioApplication.Update(usuario);
            return Ok();
        }

        /// <summary>
        /// Elimina Usuario por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public IActionResult Delete([FromBody] int id)
        {
            if (id < 0 )
            {
                return BadRequest();
            }

            var response = _usuarioApplication.Delete(id);

            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest();
        }

        private string BuildToken(Response<UsuarioDto> usuarioDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuarioDto.Data.UsuarioId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
