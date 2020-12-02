using ProyectoFinalProgIII.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalProgIII.VIewModels
{
    public class FacturaVM
    {
        public Facturacion Facturacion { get; set; }
        public IEnumerable<Productos> Productos {get; set;} // lISTA DE pRODUCTOS
        public IEnumerable<Clientes> Clientes { get; set; } //LISTA DE CLIENTES
        public IEnumerable<Servicios> Servicios { get; set; } // LIsta de servicios

    }
}
