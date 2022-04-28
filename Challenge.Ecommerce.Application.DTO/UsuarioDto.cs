using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Ecommerce.Application.DTO
{
    public class UsuarioDto
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

    }
}
