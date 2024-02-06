using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasaDeEmpeño.Models
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public float PrecioVenta { get; set; }
        public float PrecioEnQueSeVendi { get; set; }
        public int Vendido { get; set; }
        public int IdProducto { get; set; }

        public string Accion { get; set; }

        public Producto producto { get; set; }

    }
}