using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasaDeEmpeño.Models
{
    public class Devolucion
    {
        public int IdDevolucion { get; set; }
        public string ComentarioDevolucion { get; set; }
        public string FechaDevolucion { get; set; }
        public int IdProducto { get; set; }

        public string Accion { get; set; }

        public Producto producto { get; set; }
    }
}