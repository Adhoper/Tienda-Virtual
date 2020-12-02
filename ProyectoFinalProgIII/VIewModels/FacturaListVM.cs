using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalProgIII.VIewModels
{
    public class FacturaListVM
    {
        public Guid FacturacionId { get; set; }

        public string TipoFactura { get; set; }

        public string Cantidad { get; set; }

        public string Itbis { get; set; }

        public string NombreCliente { get; set; }

        public string NombreUsuario { get; set; }

        public string NombreProducto { get; set; }
        public string NombreServicio { get; set; }
    }
}
