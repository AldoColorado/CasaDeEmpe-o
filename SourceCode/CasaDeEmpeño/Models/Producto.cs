using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasaDeEmpeño.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string FechaDeIngreso { get; set; }
        public string EstadoProducto { get; set; }
        public float ValorCalculado { get; set; }
        public int IdTipoProducto { get; set; }

        public string Accion { get; set; }


        public TipoProducto tipoProducto { get; set; }
    }
}