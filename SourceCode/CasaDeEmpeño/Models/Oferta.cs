using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasaDeEmpeño.Models
{
    public class Oferta
    {
        public int IdOferta { get; set; }
        public string NombrePersonaOferta { get; set; }
        public string NumeroCelular { get; set; }
        public float MontoOferta { get; set; }
        public int IdVenta { get; set; }

    }
}