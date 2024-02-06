using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasaDeEmpeño.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Correo { get; set; }

        public string Password { get; set; }

        public string TipoUsuario { get; set; }

        public string ConfirmarPassword { get; set; }


    }
}