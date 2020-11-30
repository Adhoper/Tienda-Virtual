using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinalProgIII.Data;

namespace ProyectoFinalProgIII.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext bd;

        public UsuariosController(ApplicationDbContext context)
        {
            bd = context;
        }

        public IActionResult Index(Guid id)
        {

            var facturacion = from fact in bd.Facturacion 
                               where fact.UsuarioId == id
                               select fact;

            return View(facturacion);
        }
    }
}